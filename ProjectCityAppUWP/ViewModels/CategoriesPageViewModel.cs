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

namespace ProjectCityAppUWP.ViewModels
{
    public class CategoriesPageViewModel : ViewModelBase
    {
        public ObservableCollection<SharedCategory> Categories { get; set; }

        public CategoriesPageViewModel()
        {
            Categories = new ObservableCollection<SharedCategory>();
            GetCategories();
        }

        public async void GetCategories()
        {
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Category"));

            var tempList = JsonConvert.DeserializeObject<List<SharedCategory>>(res);
            foreach(var item in tempList)
            {
                Categories.Add(item);
            }
        }
    }
}
