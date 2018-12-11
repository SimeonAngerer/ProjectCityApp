using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class PromotionAdministrationPageViewModel : ViewModelBase
    {
        #region Properties
        public Guid PK_PromotionID { get; set; }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        private DateTimeOffset start;
        public DateTimeOffset Start
        {
            get { return start; }
            set { start = value; RaisePropertyChanged(); }
        }

        private DateTimeOffset expiration;
        public DateTimeOffset Expiration
        {
            get { return expiration; }
            set { expiration = value; RaisePropertyChanged(); }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(); }
        }
        #endregion Properties

        public DelegateCommand BtnUpdate { get; set; }
        public DelegateCommand BtnDelete { get; set; }

        private string editType;
        public string EditType
        {
            get { return editType; }
            set { editType = value; RaisePropertyChanged(); }
        }


        public PromotionAdministrationPageViewModel()
        {
            BtnUpdate = new DelegateCommand(Update);
            BtnDelete = new DelegateCommand(Delete);
            EditType = "Edit Promotion";
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Guid promotionGuid = (Guid)parameter;           // Here you get the guid of the current promotion; Guid.Empty if it is a new promotion
            if (promotionGuid != Guid.Empty)
            {
                EditType = "Edit Promotion";
                GetData(promotionGuid);
            }
            else
            {
                PK_PromotionID = Guid.Empty;
                EditType = "New Promotion";
                Title = "";
                Start = DateTimeOffset.Now;
                Expiration = DateTimeOffset.Now;
                Image = "";
                Description = "";
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
        private async void GetData(Guid id)
        {
            // Get the Promotion
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/promotion/" + id));
            SharedPromotion resPromotion = JsonConvert.DeserializeObject<SharedPromotion>(res);

            PK_PromotionID = resPromotion.PK_PromotionID;
            Title = resPromotion.Title;
            Start = resPromotion.Start;
            Expiration = resPromotion.Expiration;
            Image = resPromotion.Image;
            Description = resPromotion.Description;
        }

        private async void Update()
        {
            SharedPromotion tempPromotion = new SharedPromotion()
            {
                Title = Title,
                Start = Start.DateTime,
                Expiration = Expiration.DateTime,
                Image = Image,
                Description = Description
            };
            if (PK_PromotionID == Guid.Empty)
            {
                tempPromotion.PK_PromotionID = Guid.NewGuid();

                string tempUserId = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"];
                HttpClient client = new HttpClient();
                string temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/User/" + tempUserId));
                SharedUser user = JsonConvert.DeserializeObject<SharedUser>(temp);
                tempPromotion.FK_CompanyID = user.FK_CompanyID;

                var myContent = JsonConvert.SerializeObject(tempPromotion);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var newPromotion = await client.PostAsync(new Uri("http://localhost:51070/api/Promotion/"), byteContent);


            }
            else
            {
                HttpClient client = new HttpClient();
                var myContent = JsonConvert.SerializeObject(tempPromotion);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PutAsync(new Uri("http://localhost:51070/api/Promotion/" + PK_PromotionID), byteContent);
            }

            NavigationService.Navigate(typeof(Views.UserAdministrationPage));
        }

        private async void Delete()
        {
            if (PK_PromotionID != Guid.Empty)
            {
                HttpClient client = new HttpClient();
                await client.DeleteAsync(new Uri("http://localhost:51070/api/Promotion/" + PK_PromotionID));
            }
            NavigationService.Navigate(typeof(Views.UserAdministrationPage));
        }
    }
}
