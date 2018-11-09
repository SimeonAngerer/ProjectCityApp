using Newtonsoft.Json;
using ProjectCityAppUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private string currentUser;

        public string CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }

        private Visibility isLoggedIn;

        public Visibility IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; RaisePropertyChanged(); }
        }

        public DelegateCommand BtnUser { get; set; }

        public ShellViewModel()
        {
            UpdateView(null);
            Task.Factory.StartNew(UpdateViewTask);
        }

        private async void UpdateViewTask()
        {
            while(true)
            {
                await Task.Delay(1000);
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"] != null)
                {
                    var tempUserId = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"];
                    HttpClient client = new HttpClient();
                    string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/User/" + tempUserId));
                    var user = JsonConvert.DeserializeObject<SharedUser>(res);
                    UpdateView(user.FirstName + " " + user.LastName);
                }
            }
        }

        public void UpdateView(string currentUser)
        {
            if (!String.IsNullOrEmpty(currentUser))
            {
                CurrentUser = currentUser;
                IsLoggedIn = Visibility.Visible;
            }
            else
            {
                IsLoggedIn = Visibility.Collapsed;
            }
        }
    }
}
