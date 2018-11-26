using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class EventAdministrationPageViewModel : ViewModelBase
    {
        public string Test { get; set; }

        public EventAdministrationPageViewModel()
        {
            Test = "Event Administration";
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Guid eventGuid = (Guid)parameter;           // Here you get the guid of the current event; Guid.Empty if it is a new event
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
