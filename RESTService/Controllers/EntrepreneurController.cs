using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class EntrepreneurController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        public IEnumerable<SharedEntrepreneur> Get()
        {
            return model.Entrepreneurs.Select(x => new SharedEntrepreneur()
            {
                FK_CompanyID = x.FK_CompanyID,
                FK_UserID = x.FK_UserID,
                PK_EntrepreneurID = x.PK_EntrepreneurID
            });
        }

        public SharedEntrepreneur Get(Guid id)
        {
            var tempValue = model.Entrepreneurs.SingleOrDefault(x => x.PK_EntrepreneurID == id);
            return new SharedEntrepreneur()
            {
                FK_CompanyID = tempValue.FK_CompanyID,
                FK_UserID = tempValue.FK_UserID,
                PK_EntrepreneurID = tempValue.PK_EntrepreneurID
            };
        }

        public void Post([FromBody]SharedEntrepreneur value)
        {
            model.Entrepreneurs.Add(new Entrepreneur()
            {
                FK_CompanyID = value.FK_CompanyID,
                FK_UserID = value.FK_UserID,
                PK_EntrepreneurID = value.PK_EntrepreneurID
            });
            model.SaveChanges();
        }

        //public void Put(Guid id, [FromBody]SharedEntrepreneur value)
        //{
        //}

        public void Delete(Guid id)
        {
            model.Entrepreneurs.Remove(model.Entrepreneurs.SingleOrDefault(x => x.PK_EntrepreneurID == id));
            model.SaveChanges();
        }
    }
}
