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
	public class EventAdministrationPageViewModel : ViewModelBase
	{
		#region Properties
		public Guid PK_EventID { get; set; }

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; RaisePropertyChanged(); }
		}

		private DateTimeOffset date;
		public DateTimeOffset Date
		{
			get { return date; }
			set { date = value; RaisePropertyChanged(); }
		}

		private string street;
		public string Street
		{
			get { return street; }
			set { street = value; RaisePropertyChanged(); }
		}

		private string zipCode;
		public string ZipCode
		{
			get { return zipCode; }
			set { zipCode = value; RaisePropertyChanged(); }
		}

		private string city;
		public string City
		{
			get { return city; }
			set { city = value; RaisePropertyChanged(); }
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


		public EventAdministrationPageViewModel()
		{
			BtnUpdate = new DelegateCommand(Update);
		}

		public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
		{
			Guid eventGuid = (Guid)parameter;           // Here you get the guid of the current event; Guid.Empty if it is a new event
			if (eventGuid != Guid.Empty)
			{
				GetData(eventGuid);
			} else
			{
                PK_EventID = Guid.Empty;
				Name = "";
				Date = DateTimeOffset.Now;
				Street = "";
				ZipCode = "";
				City = "";
				Image = "";
				Description = "";
			}
			return base.OnNavigatedToAsync(parameter, mode, state);
		}

		private async void GetData(Guid id)
		{
			// Get the Event
			HttpClient client = new HttpClient();
			string res = await client.GetStringAsync(new Uri("http://localhost:51070/api/event/" + id));
			SharedEvent resEvent = JsonConvert.DeserializeObject<SharedEvent>(res);

			PK_EventID = resEvent.PK_EventID;
			Name = resEvent.Name;
			Date = resEvent.Date;
			Street = resEvent.Street;
			ZipCode = resEvent.ZipCode;
			City = resEvent.City;
			Image = resEvent.Image;
			Description = resEvent.Description;
		}

		private async void Update()
		{
			SharedEvent tempEvent = new SharedEvent()
			{
				Name = Name,
				Date = Date.DateTime,
				Street = Street,
				ZipCode = ZipCode,
				City = City,
				Image = Image,
				Description = Description
			};
			if (PK_EventID == Guid.Empty)
			{
				tempEvent.PK_EventID = Guid.NewGuid();

				string tempUserId = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"];
				HttpClient client = new HttpClient();
				string temp = await client.GetStringAsync(new Uri("http://localhost:51070/api/User/" + tempUserId));
				SharedUser user = JsonConvert.DeserializeObject<SharedUser>(temp);
				tempEvent.FK_CompanyID = user.FK_CompanyID;

				var myContent = JsonConvert.SerializeObject(tempEvent);
				var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
				var byteContent = new ByteArrayContent(buffer);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var newEvent = await client.PostAsync(new Uri("http://localhost:51070/api/Event/"), byteContent);


			} else
			{
				HttpClient client = new HttpClient();
				var myContent = JsonConvert.SerializeObject(tempEvent);
				var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
				var byteContent = new ByteArrayContent(buffer);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var result = await client.PutAsync(new Uri("http://localhost:51070/api/Event/" + PK_EventID), byteContent);
			}

			

			NavigationService.Navigate(typeof(Views.UserAdministrationPage));
		}
	}
}
