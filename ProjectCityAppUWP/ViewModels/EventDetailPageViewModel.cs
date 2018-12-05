using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.Foundation;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class EventDetailPageViewModel : ViewModelBase
    {
		public SharedEvent Event { get; set; }
		public SharedCompany Company { get; set; }
		public DelegateCommand<Guid> CmdGoToCompanyDetail { get; set; }
        public DelegateCommand CmdAddAppointment { get; set; }
		public DelegateCommand BtnTextToSpeech { get; set; }


		public EventDetailPageViewModel()
		{
			CmdGoToCompanyDetail = new DelegateCommand<Guid>(GoToCompanyDetail);
            CmdAddAppointment = new DelegateCommand(AddAppointment);
			BtnTextToSpeech = new DelegateCommand(TextToSpeech);
		}

		private async void TextToSpeech()
		{
			SpeechSynthesizer synthesizer = new SpeechSynthesizer();
			string str = "Event name: " + Event.Name + " by " + Company.Name + ". Event description: " +
				Event.Description + ". Date: " + Event.Date + ". Address: " + Event.Street + " " + Event.ZipCode + " " + Event.City + " ";
			SpeechSynthesisStream ttsStream = await synthesizer.SynthesizeTextToStreamAsync(str);
			MediaElement audioPlayer = new MediaElement();
			audioPlayer.AutoPlay = true;
			audioPlayer.SetSource(ttsStream, "");
		}

		private async void AddAppointment()
        {
            // Create an Appointment that should be added the user's appointments provider app.
            var appointment = new Windows.ApplicationModel.Appointments.Appointment();
            appointment.Subject = Event.Name;
            appointment.StartTime = new DateTimeOffset(Event.Date);
            appointment.Location = Event.Street + ", " + Event.ZipCode + " " + Event.City;
            String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(appointment, new Rect());

            if (appointmentId != String.Empty)
            {
                var dialogue = new MessageDialog("Created new Appointment with the ID: " + appointmentId);
                await dialogue.ShowAsync();
            }
            else
            {
                var dialogue = new MessageDialog("Failed to create new Appointment");
                await dialogue.ShowAsync();
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
		{
			GetEvent((Guid)parameter);
			return base.OnNavigatedToAsync(parameter, mode, state);
		}

		public async void GetEvent(Guid eventId)
		{
			HttpClient client = new HttpClient();
			string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/event/" + eventId));
			var tempEvent = JsonConvert.DeserializeObject<SharedEvent>(res);
			Event = tempEvent;
			RaisePropertyChanged("Event");

			string res2 = await client.GetStringAsync(new Uri("http://localhost:51070/api/company/" + Event.FK_CompanyID));
			var tempCompany = JsonConvert.DeserializeObject<SharedCompany>(res2);
			Company = tempCompany;
			RaisePropertyChanged("Company");
		}

		private void GoToCompanyDetail(Guid guid)
		{
			NavigationService.Navigate(typeof(Views.CompanyDetailPage), guid);
		}
	}
}
