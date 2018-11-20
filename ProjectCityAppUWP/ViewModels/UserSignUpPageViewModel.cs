using Newtonsoft.Json;
using ProjectCityAppUWP.Helpers;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectCityAppUWP.ViewModels
{
    public class UserSignUpPageViewModel : ViewModelBase
    {
        private SharedUser user = new SharedUser();

        public string UserName
        {
            get { return user.UserName; }
            set { user.UserName = value; RaisePropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return user.Password; }
            set { user.Password = value; RaisePropertyChanged(); }
        }

        public string PasswordConfirm
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        private bool entrepreneur;
        public bool Entrepreneur
        {
            get { return entrepreneur; }
            set
            {
                entrepreneur = value;
                user.Type = "entrepreneur";
                RaisePropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get { return user.DateOfBirth;}
            set {  user.DateOfBirth = value; RaisePropertyChanged(); }
        }

        public bool Customer
        {
            get { return !entrepreneur; }
            set
            {
                entrepreneur = !value;
                user.Type = "customer";
                RaisePropertyChanged();
            }
        }

        public DelegateCommand BtnSignUp { get; set; }

        public UserSignUpPageViewModel()
        {
            BtnSignUp = new DelegateCommand(SignUp);
        }

        private async void SignUp()
        {
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(user));
            var currentuser = await client.PostAsync(new Uri("http://localhost:51070/api/User/"),content);
            if (user != null)
            {
                NavigationService.Navigate(typeof(Views.UserSignIn));
                // TODO Inform Shell, currently it's a workaround
            }

        }
    }
}
