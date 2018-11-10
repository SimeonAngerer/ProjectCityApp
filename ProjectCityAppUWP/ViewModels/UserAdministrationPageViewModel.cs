using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectCityAppUWP.ViewModels
{
    public class UserAdministrationPageViewModel : ViewModelBase
    {
        public DelegateCommand BtnLogout { get; set; }
        public UserAdministrationPageViewModel()
        {
            BtnLogout = new DelegateCommand(Logout);
        }

        private void Logout()
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values.Clear();
            NavigationService.Navigate(typeof(Views.MainPage));
        }
    }
}
