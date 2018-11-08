using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using ProjectCityAppUWP.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace ProjectCityAppUWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<SharedCompany> Companies { get; set; }

        public MainPageViewModel()
        {
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            string currentUser = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"];
            if (!String.IsNullOrEmpty(currentUser))
            {
                GetCompanies(Guid.Parse(currentUser));
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GetCompanies(Guid userId)
        {
            if (Companies == null) { Companies = new ObservableCollection<SharedCompany>(); }
            Companies.Clear();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/company?userID=" + userId));
            var list = JsonConvert.DeserializeObject<List<SharedCompany>>(res);
            foreach (var item in list)
            {
                item.Command = new DelegateCommand<Guid>(GoToCompanyDetail);     // Workaround!!!
                Companies.Add(item);
            }
            RaisePropertyChanged("Companies");
        }

        private void GoToCompanyDetail(Guid guid)
        {
            NavigationService.Navigate(typeof(Views.CompanyDetailPage), guid);
        }
    }
}
