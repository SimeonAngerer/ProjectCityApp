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
    public class CompaniesPageViewModel : ViewModelBase
    {
        public ObservableCollection<SharedCompany> Companies { get; set; }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            GetCompanies((Guid)parameter);
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GetCompanies(Guid categoryID)
        {
            if(Companies == null) { Companies = new ObservableCollection<SharedCompany>(); }
            Companies.Clear();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/company?categoryID=" + categoryID));
            var list = JsonConvert.DeserializeObject<List<SharedCompany>>(res);
            foreach (var item in list)
            {
                Companies.Add(item);
            }
            RaisePropertyChanged("Companies");
        }
    }
}
