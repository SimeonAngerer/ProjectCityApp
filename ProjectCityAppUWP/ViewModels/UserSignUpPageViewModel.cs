﻿using Newtonsoft.Json;
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
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ProjectCityAppUWP.ViewModels
{
    public class UserSignUpPageViewModel : ViewModelBase
    {
        private SolidColorBrush red = new SolidColorBrush() { Color = Colors.Red };
        private SolidColorBrush green = new SolidColorBrush() { Color = Colors.Green };
        private SharedUser user = new SharedUser();
        private SharedCompany company = new SharedCompany();

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

        public async void GetCategories()
        {
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Category"));
            Categories = new ObservableCollection<SharedCategory>();
            var tempList = JsonConvert.DeserializeObject<List<SharedCategory>>(res);
            foreach (var item in tempList)
            {
                Categories.Add(item);
            }
            RaisePropertyChanged("Categories");
        }
        public ObservableCollection<SharedCategory> Categories
        {
            get; set;
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

        public string CompanyName
        {
            get { return this.company.Name; }
            set { this.company.Name = value; }
        }

       
        public string CompanyStreet
        {
            get { return this.company.Name; }
            set { this.company.Street = value; RaisePropertyChanged(); }
        }

        public string CompanyZipcode
        {
            get { return this.company.ZipCode; }
            set { this.company.ZipCode = value; }
        }

        public string CompanyCity
        {
            get { return this.company.City; }
            set { this.company.City = value; }
        }

        private SolidColorBrush _formcontrol11 = new SolidColorBrush() { Color = Colors.DarkGray };
        public SolidColorBrush FormControl11
        {
            get { return _formcontrol11; }
            set { _formcontrol11 = value; RaisePropertyChanged(); }
        }
        public SharedCategory CompanyCategory
        {
            get
            {
                return this.Categories?[this.SelectedComboIndex];
            }
        }

        
        public string CompanyImage
        {
            get { return this.company.Image; }
            set { this.company.Image = value; }
        }

        public string CompanyFacebook
        {
            get; set;
        }

        public DelegateCommand BtnSignUp { get; set; }

        public UserSignUpPageViewModel()
        {

            GetCategories();
            Customer = true;
            BtnSignUp = new DelegateCommand(SignUp);
            FormControl = new List<bool>();
        }

        private List<bool> _formcontrol;
        public List<bool> FormControl
        {
            get
            {
                return _formcontrol;
            }
            set
            {
                _formcontrol = value;
                RaisePropertyChanged();

            }
        }

        private async void SignUp()
        {
            var list = new List<bool>();
            list.Add(this.Firstname != null && this.Firstname.Length > 0); //1
            list.Add(this.Lastname != null && this.Lastname.Length > 0); //2
            list.Add(this.UserName != null && this.UserName.Length > 0); //3
            list.Add(this.Password != null && this.Password.Length > 0); //4
            list.Add(this.PasswordConfirm != null && this.Password == this.PasswordConfirm);//5 
            list.Add(this.Birthday != null); //6
            if (this.Entrepreneur)
            {
                list.Add(this.CompanyName != null && this.CompanyName?.Length > 0);//7
                list.Add(this.CompanyStreet != null && this.CompanyStreet?.Length > 0); //8
                list.Add(this.CompanyCity != null && this.CompanyCity?.Length > 0); //9
                list.Add(this.CompanyZipcode != null && this.CompanyZipcode?.Length > 0); //10
                list.Add(this.SelectedComboIndex != -1); //11
                list.Add(this.CompanyImage != null && this.CompanyImage.Length > 0); //12
                list.Add(this.CompanyFacebook != null && this.CompanyFacebook.Length > 0); //13
            }
            FormControl = list;
            if (FormControl.Count > 0)
            {
                FormControl1 = _formcontrol.ElementAt(0) ? green : red;
                FormControl2 = _formcontrol.ElementAt(1) ? green : red;
                FormControl3 = _formcontrol.ElementAt(2) ? green : red;
                FormControl4 = _formcontrol.ElementAt(3) ? green : red;
                FormControl5 = _formcontrol.ElementAt(4) ? green : red;
                FormControl6 = _formcontrol.ElementAt(5) ? green : red;
                if (this.Entrepreneur)
                {
                    FormControl7 = _formcontrol.ElementAt(6) ? green : red;
                    FormControl8 = _formcontrol.ElementAt(7) ? green : red;
                    FormControl9 = _formcontrol.ElementAt(8) ? green : red;
                    FormControl10 = _formcontrol.ElementAt(9) ? green : red;
                    FormControl11 = _formcontrol.ElementAt(10) ? green : red;
                    FormControl12 = _formcontrol.ElementAt(11) ? green : red;
                    FormControl13 = _formcontrol.ElementAt(12) ? green : red;
                }
            }

            if (FormControl.All(fc => fc))
            {
                user.PK_UserID = Guid.NewGuid();
                HttpClient client = new HttpClient();
                var myContent = JsonConvert.SerializeObject(user);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                //HttpContent content = new StringContent(JsonConvert.SerializeObject(user));
                if (!this.entrepreneur)
                {
                    clearCompany();
                }
                else
                {
                    company.PK_CompanyID = Guid.NewGuid();
                    user.FK_CompanyID = company.PK_CompanyID;
                    myContent = JsonConvert.SerializeObject(company);
                    buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    byteContent = new ByteArrayContent(buffer);
                    var companylocal = await client.PostAsync(new Uri("http://localhost:51070/api/Company/"), byteContent);
                }
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var currentuser = await client.PostAsync(new Uri("http://localhost:51070/api/User/"), byteContent);
                if (user != null)
                {
                    NavigationService.Navigate(typeof(Views.UserSignIn));
                    // TODO Inform Shell, currently it's a workaround
                }
            }
            else
            {
                
                //this.UsernameTb.BorderBrush = new SolidColorBrush() { Color= red};
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
            this.user.FK_CompanyID = default(Guid);
        }

        public int SelectedComboIndex { get; set; } = -1;


    }
}
