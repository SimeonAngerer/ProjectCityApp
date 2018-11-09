using Newtonsoft.Json;
using ProjectCityAppUWP.Helpers;
using ProjectCityAppUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectCityAppUWP.ViewModels
{
    public class UserSignInPageViewModel : ViewModelBase
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        public DelegateCommand BtnLogin { get; set; }

        public UserSignInPageViewModel()
        {
            BtnLogin = new DelegateCommand(Login);
        }

        private async void Login()
        {
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri($"http://localhost:51070/api/User?userName={UserName}&password={HashMethods.ComputeMD5(Password)}"));
            var user = JsonConvert.DeserializeObject<SharedUser>(res);
            if (user != null)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"] = user.PK_UserID.ToString();
                NavigationService.Navigate(typeof(Views.MainPage));
                // TODO Inform Shell, currently it's a workaround
            }

        }
    }
}
