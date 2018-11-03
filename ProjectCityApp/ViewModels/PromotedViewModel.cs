using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCityApp.ViewModels
{
    public class PromotedViewModel : ViewModelBase
    {
        public ObservableCollection<SharedCompany> Companies { get; set; }

        public PromotedViewModel()
        {
            GetCompanies();
        }

        private async void GetCompanies()
        {
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Company?searchDate=" + DateTime.Now.ToString("yyyy-MM-dd")));

            Companies = new ObservableCollection<SharedCompany>(JsonConvert.DeserializeObject<List<SharedCompany>>(res));
        }
    }
}
