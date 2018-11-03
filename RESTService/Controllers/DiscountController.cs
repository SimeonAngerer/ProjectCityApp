using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class DiscountController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        #region CRUD

        public IEnumerable<SharedDiscount> Get()
        {
            return model.Discounts.Select(x => new SharedDiscount()
            {
                Code = x.Code,
                Description = x.Description,
                PK_DiscountID = x.PK_DiscountID,
                Title = x.Title
            });
        }

        public SharedDiscount Get(Guid id)
        {
            var tempValue = model.Discounts.SingleOrDefault(x => x.PK_DiscountID == id);
            return new SharedDiscount()
            {
                Code = tempValue.Code,
                Description = tempValue.Description,
                PK_DiscountID = tempValue.PK_DiscountID,
                Title = tempValue.Title
            };
        }

        public void Post([FromBody]SharedDiscount value)
        {
            model.Discounts.Add(new Discount()
            {
                Code = value.Code,
                Description = value.Description,
                PK_DiscountID = value.PK_DiscountID,
                Title = value.Title
            });
            model.SaveChanges();
        }

        public void Put(Guid id, [FromBody]SharedDiscount value)
        {
            var tempValue = model.Discounts.SingleOrDefault(x => x.PK_DiscountID == id);

            if (!String.IsNullOrEmpty(value.Code)) { tempValue.Code = value.Code; }
            if (!String.IsNullOrEmpty(value.Description)) { tempValue.Description = value.Description; }
            if (!String.IsNullOrEmpty(value.Title)) { tempValue.Title = value.Title; }

            model.SaveChanges();
        }

        public void Delete(Guid id)
        {
            model.Discounts.Remove(model.Discounts.SingleOrDefault(x => x.PK_DiscountID == id));
            model.SaveChanges();
        }

        #endregion
    }
}
