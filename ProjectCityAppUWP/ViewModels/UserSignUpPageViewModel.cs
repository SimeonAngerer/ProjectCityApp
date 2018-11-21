using Newtonsoft.Json;
using ProjectCityAppUWP.Helpers;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;

namespace ProjectCityAppUWP.ViewModels
{
    public class UserSignUpPageViewModel : ViewModelBase
    {
        private SharedUser user = new SharedUser();

        public string Firstname
        {
            get { return user.FirstName; }
            set { user.FirstName = value; RaisePropertyChanged(); }
        }

        public string Lastname
        {
            get { return user.LastName; }
            set { user.LastName = value; RaisePropertyChanged(); }
        }

        public string UserName
        {
            get { return user.UserName; }
            set { user.UserName = value; RaisePropertyChanged(); }
        }

        private Visibility visible;
        public Visibility Visible
        {
            get { return this.visible; }
            set
            {
                this.visible = value;
                RaisePropertyChanged();
            }
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
                changeVisible();
                RaisePropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get { return user.DateOfBirth; }
            set { user.DateOfBirth = value; RaisePropertyChanged(); }
        }

        public bool Customer
        {
            get { return !entrepreneur; }
            set
            {
                entrepreneur = !value;
                user.Type = "customer";
                changeVisible();
                RaisePropertyChanged();
            }
        }

        public DelegateCommand BtnSignUp { get; set; }

        public UserSignUpPageViewModel()
        {
            Customer = true;
            BtnSignUp = new DelegateCommand(SignUp);
        }

        private async void SignUp()
        {
            user.PK_UserID = Guid.NewGuid();
            user.Password = HashMethods.ComputeMD5(user.Password);
            HttpClient client = new HttpClient();
            var myContent = JsonConvert.SerializeObject(user);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            //HttpContent content = new StringContent(JsonConvert.SerializeObject(user));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var currentuser = await client.PostAsync(new Uri("http://localhost:51070/api/User/"), byteContent);
            if (user != null)
            {
                NavigationService.Navigate(typeof(Views.UserSignIn));
                // TODO Inform Shell, currently it's a workaround
            }

        }

        private void changeVisible()
        {
            if (this.entrepreneur)
            {
                this.Visible = Visibility.Visible;
            }
            else
            {
                this.Visible = Visibility.Collapsed;
            }
        }

        private void clearCompany()
        {
            this.user.FK_CompanyID = default(Guid); // this foreign key should optional, not required
        }
    }
}
