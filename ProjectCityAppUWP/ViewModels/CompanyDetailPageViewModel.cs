using Microsoft.Toolkit.Uwp.Notifications;
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
using Windows.Media.SpeechSynthesis;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class CompanyDetailPageViewModel : ViewModelBase
    {
        private Visibility visible;

        public Visibility Visible
        {
            get { return visible; }
            set { visible = value; RaisePropertyChanged(); }
        }

        public SharedCompany Company { get; set; }
        public DelegateCommand BtnFacebook { get; set; }
        public DelegateCommand BtnLike { get; set; }
        public DelegateCommand<Guid> CmdGoToEventDetail { get; set; }
        public DelegateCommand<Guid> CmdGoToPromotionDetail { get; set; }

        public ObservableCollection<SharedEvent> Events { get; set; }
        public ObservableCollection<SharedPromotion> Promotions { get; set; }

		public DelegateCommand BtnTextToSpeech { get; set; }


		public CompanyDetailPageViewModel()
        {
            BtnFacebook = new DelegateCommand(OpenFacebook);
            BtnLike = new DelegateCommand(LikeCompany);
            CmdGoToEventDetail = new DelegateCommand<Guid>(GoToEventDetail);
            CmdGoToPromotionDetail = new DelegateCommand<Guid>(GoToPromotionDetail);
			BtnTextToSpeech = new DelegateCommand(TextToSpeech);
		}

		private async void TextToSpeech()
		{
			SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            string description = Company.Description.Trim() == "" ? "No description" : Company.Description;
			string str = "Company name: " + Company.Name + ".. description: "+ description +". Address: " + Company.Street + " " + Company.ZipCode + " " + Company.City;
			SpeechSynthesisStream ttsStream = await synthesizer.SynthesizeTextToStreamAsync(str);
			MediaElement audioPlayer = new MediaElement();
			audioPlayer.AutoPlay = true;
			audioPlayer.SetSource(ttsStream, "");
		}

		private async void LikeCompany()
        {
            string currentUser = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"];
            if (!String.IsNullOrEmpty(currentUser))
            {
                HttpClient client = new HttpClient();
                var res = await client.PostAsync(
                    new Uri($"http://localhost:51070/api/Follower?companyGuid={Company.PK_CompanyID}&userId={currentUser}"),
                    null);
                string resultContent = await res.Content.ReadAsStringAsync();
                CreateToast(resultContent);
            }
        }

        private void CreateToast(string resultContent)
        {
            string title = resultContent == "true" ? "SUCCESS: You are now following..." : "WARNING: You are already following...";
            string content = Company.Name;
            string image = Company.Image;
            string logo = "ms-appdata:///local/Andrew.jpg";

            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },

                        new AdaptiveText()
                        {
                            Text = content
                        },

                        new AdaptiveImage()
                        {
                            Source = image
                        }
                    },
                    AppLogoOverride = new ToastGenericAppLogo()
                    {
                        Source = logo,
                        HintCrop = ToastGenericAppLogoCrop.Circle
                    }
                }
            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual
            };

            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private async void OpenFacebook()
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(Company.Facebook));
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Visible = Visibility.Collapsed;
            GetCompany((Guid)parameter);
            GetEvents((Guid)parameter);
            GetPromotions((Guid)parameter);
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GetCompany(Guid companyId)
        {
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/company/" + companyId));
            var tempCompany = JsonConvert.DeserializeObject<SharedCompany>(res);
            Company = tempCompany;
            RaisePropertyChanged("Company");
        }

        public async void GetEvents(Guid companyId)
        {
            if (Events == null) { Events = new ObservableCollection<SharedEvent>(); }
            Events.Clear();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Event?companyID=" + companyId));
            var list = JsonConvert.DeserializeObject<List<SharedEvent>>(res);
            foreach (var item in list)
            {
                // Navigate to eventpage
                // item.Command = new DelegateCommand<Guid>(GoToEventDetail);
                Events.Add(item);
            }
            if(list.Count > 0) { Visible = Visibility.Visible; }
            RaisePropertyChanged("Events");
        }

        public async void GetPromotions(Guid companyId)
        {
            if (Promotions == null) { Promotions = new ObservableCollection<SharedPromotion>(); }
            Promotions.Clear();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/Promotion?companyID=" + companyId));
            var list = JsonConvert.DeserializeObject<List<SharedPromotion>>(res);
            foreach (var item in list)
            {
                // Navigate to promotionpage
                // item.Command = new DelegateCommand<Guid>(GoToPromotionDetail);
                Promotions.Add(item);
            }
            if (list.Count > 0) { Visible = Visibility.Visible; }
            RaisePropertyChanged("Promotions");
        }

        private void GoToEventDetail(Guid guid)
        {
            NavigationService.Navigate(typeof(Views.EventDetailPage), guid);
        }

        private void GoToPromotionDetail(Guid guid)
        {
            NavigationService.Navigate(typeof(Views.PromotionDetailPage), guid);
        }

    }
}
