using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;

namespace ProjectCityAppUWP.ViewModels
{
    public class CategoriesPageViewModel : ViewModelBase
    {
        public ObservableCollection<SharedCategory> Categories { get; set; }
        public DelegateCommand<Guid> CmdGoToCategory { get; set; }

        public CategoriesPageViewModel()
        {
            Categories = new ObservableCollection<SharedCategory>();
            CmdGoToCategory = new DelegateCommand<Guid>(GoToCategory);
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
            RaisePropertyChanged("Categories");
        }

        private void GoToCategory(Guid guid)
        {
            NavigationService.Navigate(typeof(Views.CompaniesPage), guid);
        }
    }
}
