using Newtonsoft.Json;
using ProjectCityAppUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        public ObservableCollection<SharedCompany> Companies { get; set; }
        public DelegateCommand BtnSearch { get; set; }
        private string searchString;

        public string SearchString
        {
            get { return searchString; }
            set { searchString = value; RaisePropertyChanged(); }
        }


        public SearchPageViewModel()
        {
            BtnSearch = new DelegateCommand(GetCompanies);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (mode == NavigationMode.Back)
            {
                // TODO
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GetCompanies()
        {
            if (Companies == null) { Companies = new ObservableCollection<SharedCompany>(); }
            Companies.Clear();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/company?searchString=" + SearchString));
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
