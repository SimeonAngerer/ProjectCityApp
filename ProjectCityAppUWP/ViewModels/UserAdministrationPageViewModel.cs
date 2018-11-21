using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;

namespace ProjectCityAppUWP.ViewModels
{
    public class UserAdministrationPageViewModel : ViewModelBase
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
        private string firstName;

        public string Firstname
        {
            get { return firstName; }
            set { firstName = value; RaisePropertyChanged(); }
        }

        private string lastName;

        public string Lastname
        {
            get { return lastName; }
            set { lastName = value; RaisePropertyChanged(); }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        private string passwordConfirm;

        public string PasswordConfirm
        {
            get { return passwordConfirm; }
            set { passwordConfirm = value; RaisePropertyChanged(); }
        }
        private DateTime birthday;

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; RaisePropertyChanged(); }
        }
        private Visibility visible;

        public Visibility Visible
        {
            get { return visible; }
            set { visible = value; RaisePropertyChanged(); }
        }
        private string companyName;

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; RaisePropertyChanged(); }
        }
        private string street;

        public string Street
        {
            get { return street; }
            set { street = value; RaisePropertyChanged(); }
        }
        private string postalcode;

        public string Postalcode
        {
            get { return postalcode; }
            set { postalcode = value; RaisePropertyChanged(); }
        }
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; RaisePropertyChanged(); }
        }
        private string facebook;

        public string Facebook
        {
            get { return facebook; }
            set { facebook = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SharedEvent> events;

        public ObservableCollection<SharedEvent> Events
        {
            get { return events; }
            set { events = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<SharedPromotion> promotions;

        public ObservableCollection<SharedPromotion> Promotions
        {
            get { return promotions; }
            set { promotions = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<Guid> CmdGoToEventAdministration { get; set; }
        public DelegateCommand<Guid> CmdGoToPromotionAdministration { get; set; }
        public DelegateCommand BtnLogout { get; set; }
        public UserAdministrationPageViewModel()
        {
            // Delegates
            BtnLogout = new DelegateCommand(Logout);

            GetData();
        }

        private async void GetData()
        {
            // Get the User
            string tempUserId = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"];
            HttpClient client = new HttpClient();
            string temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/User/" + tempUserId));
            SharedUser user = JsonConvert.DeserializeObject<SharedUser>(temp);
            UserName = user.UserName;
            Firstname = user.FirstName;
            Lastname = user.LastName;
            Birthday = user.DateOfBirth;
            

            if (user.Type == "Entrepreneur")
            {
                // Get the Company
                temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/Company/" + user.FK_CompanyID));
                SharedCompany company = JsonConvert.DeserializeObject<SharedCompany>(temp);
                CompanyName = company.Name;
                Street = company.Street;
                Postalcode = company.ZipCode;
                City = company.City;
                Facebook = company.Facebook;

                // Get the Events
                temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/Event?companyID=" + user.FK_CompanyID));
                Events = new ObservableCollection<SharedEvent>(JsonConvert.DeserializeObject<List<SharedEvent>>(temp));

                // Get the Promotions
                temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/Promotion?companyID=" + user.FK_CompanyID));
                Promotions = new ObservableCollection<SharedPromotion>(JsonConvert.DeserializeObject<List<SharedPromotion>>(temp));
            }
            else
            {
                Visible = Visibility.Collapsed;
            }
        }

        private void Logout()
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values.Clear();
            NavigationService.Navigate(typeof(Views.MainPage));
        }
    }
}
