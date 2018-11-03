using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class EventController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        #region CRUD

        public IEnumerable<SharedEvent> Get()
        {
            return model.Events.Select(x => new SharedEvent()
            {
                City = x.City,
                Date = x.Date,
                FK_CompanyID = x.FK_CompanyID,
                Name = x.Name,
                PK_EventID = x.PK_EventID,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }

        public void Post([FromBody]SharedEvent value)
        {
            model.Events.Add(new Event()
            {
                City = value.City,
                Date = value.Date,
                FK_CompanyID = value.FK_CompanyID,
                Name = value.Name,
                PK_EventID = value.PK_EventID,
                Street = value.Street,
                Zipcode = value.ZipCode
            });
            model.SaveChanges();
        }

        public void Put(Guid id, [FromBody]SharedEvent value)
        {
            var tempValue = model.Events.SingleOrDefault(x => x.PK_EventID == id);

            if (!String.IsNullOrEmpty(value.City)) { tempValue.City = value.City; }
            if (value.Date != DateTime.MinValue) { tempValue.Date = value.Date; }
            if (!String.IsNullOrEmpty(value.Name)) { tempValue.Name = value.Name; }
            if (!String.IsNullOrEmpty(value.Street)) { tempValue.Street = value.Street; }
            if (!String.IsNullOrEmpty(value.ZipCode)) { tempValue.Zipcode = value.ZipCode; }

            model.SaveChanges();
        }

        public void Delete(Guid id)
        {
            model.Events.Remove(model.Events.SingleOrDefault(x => x.PK_EventID == id));
            model.SaveChanges();
        }

        #endregion

        public List<SharedEvent> GetEventsByCompanyID(Guid companyID)
        {
            return model.Events.Where(x => x.Company.PK_CompanyID == companyID).Select(x => new SharedEvent()
            {
                City = x.City,
                Date = x.Date,
                PK_EventID = x.PK_EventID,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }
    }
}
