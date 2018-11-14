using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClasses
{
    public class SharedPromotion
    {
        public Guid PK_PromotionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Expiration { get; set; }
        public Guid FK_CompanyID { get; set; }
    }
}
