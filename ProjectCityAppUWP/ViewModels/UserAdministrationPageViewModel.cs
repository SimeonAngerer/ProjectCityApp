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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<SharedCategory> categories = new ObservableCollection<SharedCategory>();
        public ObservableCollection<SharedCategory> Categories { get { return categories; } set { categories = value; RaisePropertyChanged(); } }
        public async void GetCategories()
        {
            HttpClient client = new HttpClient();
            string res = "";
            res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Category"));
            categories = new ObservableCollection<SharedCategory>();
            foreach (var item in JsonConvert.DeserializeObject<List<SharedCategory>>(res))
            {
                categories.Add(item);
            }
            RaisePropertyChanged("Categories");
        }
        public int SelectedComboIndex { get; set; } = -1;

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

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
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

        private SolidColorBrush red = new SolidColorBrush() { Color = Colors.Red };
        private SolidColorBrush green = new SolidColorBrush() { Color = Colors.Green };
        private SolidColorBrush gray = new SolidColorBrush() { Color = Colors.DarkGray };
        #region Formcontrols
        public List<bool> Formcontrol { get; set; }

        private SolidColorBrush _formcontrol1 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl1
        {
            get { return _formcontrol1; }
            set { _formcontrol1 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol2 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl2
        {
            get { return _formcontrol2; }
            set { _formcontrol2 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol3 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl3
        {
            get { return _formcontrol3; }
            set { _formcontrol3 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol4 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl4
        {
            get { return _formcontrol4; }
            set { _formcontrol4 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol5 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl5
        {
            get { return _formcontrol5; }
            set { _formcontrol5 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol6 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl6
        {
            get { return _formcontrol6; }
            set { _formcontrol6 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol7 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl7
        {
            get { return _formcontrol7; }
            set { _formcontrol7 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol8 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl8
        {
            get { return _formcontrol8; }
            set { _formcontrol8 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol9 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl9
        {
            get { return _formcontrol9; }
            set { _formcontrol9 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol10 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl10
        {
            get { return _formcontrol10; }
            set { _formcontrol10 = value; RaisePropertyChanged(); }
        }

        private SolidColorBrush _formcontrol11 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl11
        {
            get { return _formcontrol11; }
            set { _formcontrol11 = value; RaisePropertyChanged(); }
        }

        private SolidColorBrush _formcontrol12 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl12
        {
            get { return _formcontrol12; }
            set { _formcontrol12 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol13 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl13
        {
            get { return _formcontrol13; }
            set { _formcontrol13 = value; RaisePropertyChanged(); }
        }
        private SolidColorBrush _formcontrol14 = new SolidColorBrush() { Color = Colors.DarkGray };

        public SolidColorBrush FormControl14
        {
            get { return _formcontrol14; }
            set { _formcontrol14 = value; RaisePropertyChanged(); }
        } 
        #endregion

        public DelegateCommand<Guid> CmdGoToEventAdministration { get; set; }
        public DelegateCommand<Guid> CmdGoToPromotionAdministration { get; set; }
        public DelegateCommand CmdCreateEvent { get; set; }
        public DelegateCommand CmdCreatePromotion { get; set; }
        public DelegateCommand BtnUpdate { get; set; }
        public DelegateCommand BtnLogout { get; set; }

        public UserAdministrationPageViewModel()
        {
            GetCategories();
            // Delegates
            CmdGoToEventAdministration = new DelegateCommand<Guid>(GoToEventAdministration);
            CmdGoToPromotionAdministration = new DelegateCommand<Guid>(GoToPromotionAdministration);
            CmdCreateEvent = new DelegateCommand(CreateEvent);
            CmdCreatePromotion = new DelegateCommand(CreatePromotion);
            BtnUpdate = new DelegateCommand(Update);
            BtnLogout = new DelegateCommand(Logout);

            GetData();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            GetData();
            return base.OnNavigatedToAsync(parameter, mode, state);
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
            var list = new List<bool>();
            list.Add(this.Firstname != null && this.Firstname.Length > 0); //1
            list.Add(this.Lastname != null && this.Lastname.Length > 0); //2
            list.Add(this.UserName != null && this.UserName.Length > 0); //3
            list.Add(this.Password == this.PasswordConfirm); //4
            list.Add(this.Password == this.PasswordConfirm);//5 
            list.Add(this.Birthday != null); //6
            if (this.CompanyName != null)
            {
                list.Add(this.CompanyName != null && this.CompanyName?.Length > 0);//7
                list.Add(this.Street != null && this.Street?.Length > 0); //8
                list.Add(this.Postalcode != null && this.Postalcode?.Length > 0); //9
                list.Add(this.City != null && this.City?.Length > 0); //10
                list.Add(this.Facebook != null && this.Facebook.Length > 0); //11
                list.Add(this.Image != null && this.Image.Length > 0); //12
                list.Add(this.Description != null && this.Description.Length > 0); //13
            }
            Formcontrol = list;
            if (Formcontrol.Count > 0)
            {
                FormControl1 = Formcontrol.ElementAt(0) ? green : red;//1
                FormControl2 = Formcontrol.ElementAt(1) ? green : red;//2
                FormControl3 = Formcontrol.ElementAt(2) ? green : red;//3
                FormControl4 = Formcontrol.ElementAt(3) ? green : red;//4
                FormControl5 = Formcontrol.ElementAt(4) ? green : red;//5
                FormControl6 = Formcontrol.ElementAt(5) ? green : red;//6
                if (this.CompanyName != null)
                {
                    FormControl7 = Formcontrol.ElementAt(6) ? green : red;//7
                    FormControl8 = Formcontrol.ElementAt(7) ? green : red;//8
                    FormControl9 = Formcontrol.ElementAt(8) ? green : red;//9
                    FormControl10 = Formcontrol.ElementAt(9) ? green : red;//10
                    FormControl11 = Formcontrol.ElementAt(10) ? green : red;//11
                    FormControl12 = Formcontrol.ElementAt(11) ? green : red;//12
                    FormControl13 = Formcontrol.ElementAt(12) ? green : red;//13
                }
            }
            if (Formcontrol.All(f => f))
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
                        Description = Description,
                        Facebook = Facebook,
                        Name = CompanyName,
                        Street = Street,
                        ZipCode = Postalcode,
                        Image = Image
                    };
                    myContent = JsonConvert.SerializeObject(tempCompany);
                    buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    result = await client.PutAsync(new Uri("http://localhost:51070/api/Company/" + FK_CompanyID), byteContent);
                }
                FormControl1 = gray;
                FormControl2 = gray;
                FormControl3 = gray;
                FormControl4 = gray;
                FormControl5 = gray;
                FormControl6 = gray;
                if (this.CompanyName != null)
                {
                    FormControl7 = gray;
                    FormControl8 = gray;
                    FormControl9 = gray;
                    FormControl10 = gray;
                    FormControl11 = gray;
                    FormControl12 = gray;
                    FormControl13 = gray;
                }
                NavigationService.Navigate(typeof(Views.MainPage));
            }
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
                Image = company.Image;
                Description = company.Description;

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
