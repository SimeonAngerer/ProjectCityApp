using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectCityAppUWP.Models
{
    public class SharedCategory
    {
        public Guid PK_CategoryID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DelegateCommand<Guid> Command { get; set; }
    }
}
