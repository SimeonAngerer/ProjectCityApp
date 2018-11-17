using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class EventDetailPageViewModel : ViewModelBase
    {
		public SharedEvent Event { get; set; }
		public SharedCompany Company { get; set; }
		public DelegateCommand<Guid> CmdGoToCompanyDetail { get; set; }


		public EventDetailPageViewModel()
		{
			CmdGoToCompanyDetail = new DelegateCommand<Guid>(GoToCompanyDetail);
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
