using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ProjectCityAppUWP.ViewModels
{
    public class PromotionAdministrationPageViewModel : ViewModelBase
    {
        public string Test { get; set; }

        public PromotionAdministrationPageViewModel()
        {
            Test = "Promotion Administration";
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Guid promotionGuid = (Guid)parameter;           // Here you get the guid of the current promotion
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
