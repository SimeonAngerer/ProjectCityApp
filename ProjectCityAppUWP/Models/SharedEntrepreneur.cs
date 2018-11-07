using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCityAppUWP.Models
{
    public class SharedEntrepreneur
    {
        public Guid PK_EntrepreneurID { get; set; }
        public Guid FK_CompanyID { get; set; }
        public Guid FK_UserID { get; set; }
    }
}
