﻿using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.ApplicationModel.UserActivities;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class PromotionDetailPageViewModel : ViewModelBase
    {
		public SharedPromotion Promotion { get; set; }
		public SharedCompany Company { get; set; }
		public DelegateCommand BtnDiscount { get; set; }
        public DelegateCommand BtnTextToSpeech { get; set; }

        public PromotionDetailPageViewModel()
        {
			BtnDiscount = new DelegateCommand(OpenDiscountPdf);
            BtnTextToSpeech = new DelegateCommand(TextToSpeech);
		}

        private async void TextToSpeech()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
			string str = "Promotion title: " + Promotion.Title + " by " + Company.Name + ". Promotion description: " + 
				Promotion.Description + ". Start date: " + Promotion.Start + ". Expiration date: " + Promotion.Expiration;
			SpeechSynthesisStream ttsStream = await synthesizer.SynthesizeTextToStreamAsync(str);
            MediaElement audioPlayer = new MediaElement();
            audioPlayer.AutoPlay = true;
            audioPlayer.SetSource(ttsStream, "");
        }

        UserActivitySession _currentActivity;
        private async void OpenDiscountPdf()
		{
            var success = await Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf"));
            await GenerateActivityAsync();
        }

        private async Task GenerateActivityAsync()
        {
            // Get the default UserActivityChannel and query it for our UserActivity. If the activity doesn't exist, one is created.
            UserActivityChannel channel = UserActivityChannel.GetDefault();
            UserActivity userActivity = await channel.GetOrCreateUserActivityAsync("MainPage");

            // Populate required properties
            userActivity.VisualElements.DisplayText = "ProjectCityAppUWP Activity";
            userActivity.VisualElements.Description = "You downloaded a discount PDF!";
            userActivity.ActivationUri = new Uri("my-app://MainPage?action=edit");

            //Save
            await userActivity.SaveAsync(); //save the new metadata

            // Dispose of any current UserActivitySession, and create a new one.
            _currentActivity?.Dispose();
            _currentActivity = userActivity.CreateSession();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
		{
			GetPromotion((Guid)parameter);
			return base.OnNavigatedToAsync(parameter, mode, state);
		}

		public async void GetPromotion(Guid promotionId)
		{
			HttpClient client = new HttpClient();
			string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/promotion/" + promotionId));
			var tempPromotion = JsonConvert.DeserializeObject<SharedPromotion>(res);
			Promotion = tempPromotion;
			RaisePropertyChanged("Promotion");

			string res2 = await client.GetStringAsync(new Uri("http://localhost:51070/api/company/" + Promotion.FK_CompanyID));
			var tempCompany = JsonConvert.DeserializeObject<SharedCompany>(res2);
			Company = tempCompany;
			RaisePropertyChanged("Company");
		}
	}
}
