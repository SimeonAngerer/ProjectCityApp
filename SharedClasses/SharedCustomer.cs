using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClasses
{
    public class SharedCustomer
    {
        public Guid PK_CustomerID { get; set; }
        public Guid FK_UserID { get; set; }
    }
}
