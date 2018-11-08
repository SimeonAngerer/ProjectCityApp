using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectCityAppUWP.Models
{
    public class SharedCompany
    {
        public Guid PK_CompanyID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Facebook { get; set; }
        public Guid FK_CategoryID { get; set; }
        public DelegateCommand<Guid> Command { get; set; }
    }
}
