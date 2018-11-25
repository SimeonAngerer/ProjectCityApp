using Newtonsoft.Json;
using ProjectCityAppUWP.Helpers;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;

namespace ProjectCityAppUWP.ViewModels
{
    public class UserAdministrationPageViewModel : ViewModelBase
    {
        #region Properties
        public string Type { get; set; }
        public Guid PK_UserID { get; set; }
        public Guid FK_CompanyID { get; set; }

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
        private DateTimeOffset birthday;

        public DateTimeOffset Birthday
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
        #endregion Properties

        public DelegateCommand<Guid> CmdGoToEventAdministration { get; set; }
        public DelegateCommand<Guid> CmdGoToPromotionAdministration { get; set; }
        public DelegateCommand CmdCreateEvent { get; set; }
        public DelegateCommand CmdCreatePromotion { get; set; }
        public DelegateCommand BtnUpdate { get; set; }
        public DelegateCommand BtnLogout { get; set; }

        public UserAdministrationPageViewModel()
        {
            // Delegates
            CmdGoToEventAdministration = new DelegateCommand<Guid>(GoToEventAdministration);
            CmdGoToPromotionAdministration = new DelegateCommand<Guid>(GoToPromotionAdministration);
            CmdCreateEvent = new DelegateCommand(CreateEvent);
            CmdCreatePromotion = new DelegateCommand(CreatePromotion);
            BtnUpdate = new DelegateCommand(Update);
            BtnLogout = new DelegateCommand(Logout);

            GetData();
        }

        private void CreatePromotion()
        {
            NavigationService.Navigate(typeof(Views.PromotionAdministrationPage), Guid.Empty);
        }

        private void CreateEvent()
        {
            NavigationService.Navigate(typeof(Views.EventAdministrationPage), Guid.Empty);
        }

        private void GoToPromotionAdministration(Guid guid)
        {
            NavigationService.Navigate(typeof(Views.PromotionAdministrationPage), guid);
        }

        private void GoToEventAdministration(Guid guid)
        {
            NavigationService.Navigate(typeof(Views.EventAdministrationPage), guid);
        }

        private async void Update()
        {
            SharedUser tempUser = new SharedUser()
            {
                DateOfBirth = Birthday.DateTime,
                FirstName = Firstname,
                LastName = Lastname,
                Password = HashMethods.ComputeMD5(Password),
                UserName = UserName
            };
            HttpClient client = new HttpClient();
            var myContent = JsonConvert.SerializeObject(tempUser);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PutAsync(new Uri("http://localhost:51070/api/User/" + PK_UserID), byteContent);

            if (Type == "Entrepreneur")
            {
                SharedCompany tempCompany = new SharedCompany()
                {
                    City = City,
                    Facebook = Facebook,
                    Name = CompanyName,
                    Street = Street,
                    ZipCode = Postalcode
                };
                myContent = JsonConvert.SerializeObject(tempCompany);
                buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                result = await client.PutAsync(new Uri("http://localhost:51070/api/Company/" + FK_CompanyID), byteContent);
            }
            NavigationService.Navigate(typeof(Views.MainPage));
        }

        private async void GetData()
        {
            // Get the User
            string tempUserId = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"];
            HttpClient client = new HttpClient();
            string temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/User/" + tempUserId));
            SharedUser user = JsonConvert.DeserializeObject<SharedUser>(temp);
            PK_UserID = user.PK_UserID;
            UserName = user.UserName;
            Firstname = user.FirstName;
            Lastname = user.LastName;
            Birthday = user.DateOfBirth;
            Type = user.Type;
            

            if (user.Type == "Entrepreneur")
            {
                Visible = Visibility.Visible;

                // Get the Company
                temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/Company/" + user.FK_CompanyID));
                SharedCompany company = JsonConvert.DeserializeObject<SharedCompany>(temp);
                FK_CompanyID = user.FK_CompanyID;
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
