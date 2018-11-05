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
    public class CompaniesViewModel : ViewModelBase
    {
        public ObservableCollection<SharedCompany> Companies { get; set; }

        public CompaniesViewModel()
        {
            Companies = new ObservableCollection<SharedCompany>();
        }

        public async void GetCompanies(Guid categoryID)
        {
            Companies.Clear();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/company?categoryID=" + categoryID));
            var list = JsonConvert.DeserializeObject<List<SharedCompany>>(res);
            foreach(var item in list)
            {
                Companies.Add(item);
            }
        }
    }
}
