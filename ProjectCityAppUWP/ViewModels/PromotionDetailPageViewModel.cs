using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectCityAppUWP.ViewModels
{
    public class PromotionDetailPageViewModel : ViewModelBase
    {
        public string Test { get; set; }

        public PromotionDetailPageViewModel()
        {
            Test = "Promotion Detail";
        }
    }
}
