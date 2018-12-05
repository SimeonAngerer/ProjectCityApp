using Newtonsoft.Json;
using SharedClasses;
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
        public DelegateCommand<Guid> CmdGoToCompanyDetail { get; set; }
        private string searchString;

        public string SearchString
        {
            get { return searchString; }
            set { searchString = value; GetCompanies(); RaisePropertyChanged(); }
        }


        public SearchPageViewModel()
        {
            CmdGoToCompanyDetail = new DelegateCommand<Guid>(GoToCompanyDetail);
            SearchString = String.Empty;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (!String.IsNullOrEmpty(SearchString)) { SearchString = String.Empty; }
            if (mode == NavigationMode.Back)
            {
                // TODO
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GetCompanies()
        {
            //Views.Busy.SetBusy(true, "Please wait...");
            if (Companies == null) { Companies = new ObservableCollection<SharedCompany>(); }
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/company?searchString=" + SearchString));
            var list = JsonConvert.DeserializeObject<List<SharedCompany>>(res);
            foreach (var item in list)
            {
                if (!Companies.Contains(item)) { Companies.Add(item); }
            }
            for (int i = Companies.Count - 1; i >= 0; i--)
            {
                if (!list.Contains(Companies[i])) { Companies.Remove(Companies[i]); }
            }
            RaisePropertyChanged("Companies");
            //Views.Busy.SetBusy(false);
        }

        private void GoToCompanyDetail(Guid guid)
        {
            NavigationService.Navigate(typeof(Views.CompanyDetailPage), guid);
        }
    }
}
