using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectCityAppUWP.ViewModels
{
    public class CategoriesPageViewModel : ViewModelBase
    {
        //public ObservableCollection<SharedCategory> Categories { get; set; }

        public CategoriesPageViewModel()
        {
            GetCategories();
        }

        public async void GetCategories()
        {
            //HttpClient client = new HttpClient();
            //string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Category"));

            //Categories = new ObservableCollection<SharedCategory>(JsonConvert.DeserializeObject<List<SharedCategory>>(res));
        }
    }
}
