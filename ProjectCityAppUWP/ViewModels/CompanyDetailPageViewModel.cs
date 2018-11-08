using Newtonsoft.Json;
using ProjectCityAppUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class CompanyDetailPageViewModel : ViewModelBase
    {
        public SharedCompany Company { get; set; }
        public DelegateCommand BtnFacebook { get; set; }
        public DelegateCommand BtnLike { get; set; }

        public CompanyDetailPageViewModel()
        {
            BtnFacebook = new DelegateCommand(OpenFacebook);
            BtnLike = new DelegateCommand(LikeCompany);
        }

        private void LikeCompany()
        {
            //LaunchUriAsync(new Uri(Company.Facebook);
        }

        private void OpenFacebook()
        {
            throw new NotImplementedException();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            GetCompany((Guid)parameter);
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
        public async void GetCompany(Guid companyId)
        {
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/company/" + companyId));
            var tempCompany = JsonConvert.DeserializeObject<SharedCompany>(res);
            Company = tempCompany;
            RaisePropertyChanged("Company");
        }
    }
}
