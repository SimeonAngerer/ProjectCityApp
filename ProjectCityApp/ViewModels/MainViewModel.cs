using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCityApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string CurrentUser { get; set; }

        public MainViewModel()
        {
            CurrentUser = "Simeon";
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentUser"] = "Simeon";
        }
    }
}
