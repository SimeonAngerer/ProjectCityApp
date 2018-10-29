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
    public class CategoriesViewModel : ViewModelBase
    {
        public string Test { get; set; }
        public ObservableCollection<SharedCategory> Categories { get; set; }

        public CategoriesViewModel()
        {
            GetCategories();
        }

        public async void GetCategories()
        {
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Category"));

            Categories = new ObservableCollection<SharedCategory>(JsonConvert.DeserializeObject<List<SharedCategory>>(res));
        }
    }
}
