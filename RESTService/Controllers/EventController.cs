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

        public List<SharedEvent> GetEventsByCompanyID(Guid companyID)
        {
            return model.Events.Where(x => x.Company.PK_CompanyID == companyID).Select(x => new SharedEvent()
            {
                City = x.City,
                Date = x.Date,
                EventID = x.PK_EventID,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }
    }
}
