using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class PromotionController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        public IEnumerable<SharedPromotion> Get()
        {
            return model.Promotions.Select(x => new SharedPromotion()
            {
                Description = x.Description,
                Expiration = x.Expiration,
                FK_CompanyID = x.FK_CompanyID,
                FK_DiscountID = x.FK_DiscountID,
                PK_PromotionID = x.PK_PromotionID,
                Start = x.Start,
                Title = x.Title
            });
        }

        public SharedPromotion Get(Guid id)
        {
            var tempValue = model.Promotions.SingleOrDefault(x => x.PK_PromotionID == id);
            return new SharedPromotion()
            {
                Description = tempValue.Description,
                Expiration = tempValue.Expiration,
                FK_CompanyID = tempValue.FK_CompanyID,
                FK_DiscountID = tempValue.FK_DiscountID,
                PK_PromotionID = tempValue.PK_PromotionID,
                Start = tempValue.Start,
                Title = tempValue.Title
            };
        }

        public void Post([FromBody]SharedPromotion value)
        {
            model.Promotions.Add(new Promotion()
            {
                Description = value.Description,
                Expiration = value.Expiration,
                FK_CompanyID = value.FK_CompanyID,
                FK_DiscountID = value.FK_DiscountID,
                PK_PromotionID = value.PK_PromotionID,
                Start = value.Start,
                Title = value.Title
            });
            model.SaveChanges();
        }

        public void Put(Guid id, [FromBody]SharedPromotion value)
        {
            var tempValue = Get(id);

            if (!String.IsNullOrEmpty(value.Description)) { tempValue.Description = value.Description; }
            if (value.Expiration != DateTime.MinValue) { tempValue.Expiration = value.Expiration; }
            if (value.Start != DateTime.MinValue) { tempValue.Start = value.Start; }
            if (!String.IsNullOrEmpty(value.Title)) { tempValue.Title = value.Title; }

            model.SaveChanges();
        }

        public void Delete(Guid id)
        {
            model.Promotions.Remove(model.Promotions.SingleOrDefault(x => x.PK_PromotionID == id));
            model.SaveChanges();
        }
    }
}
