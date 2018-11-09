using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;

namespace ProjectCityAppUWP.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private string currentUser;

        public string CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }

        private Visibility isLoggedIn;

        public Visibility IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; RaisePropertyChanged(); }
        }

        public ShellViewModel()
        {
            IsLoggedIn = Visibility.Collapsed;
            CurrentUser = "Simeon Angerer";
        }
    }
}
