using DataRepository;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class CompaniesController : ApiController
    {
        public IEnumerable<SharedCompany> GetAllCompanies()
        {
            return new DataHandler().GetAllCompanies();
        }
    }
}
