using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClasses
{
    public class SharedEvent
    {
        public Guid PK_EventID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public Guid FK_CompanyID { get; set; }
    }
}
