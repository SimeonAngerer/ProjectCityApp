using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class CustomerController : ApiController
    {
        // GET: api/Customer/5
        public SharedCustomer GetCustomer(Guid id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Customer
        public void PostNewCustomer([FromBody]SharedCustomer value)
        {
        }

        // PUT: api/Customer/5
        public void PutCustomer(Guid id, [FromBody]SharedCustomer value)
        {
        }

        // DELETE: api/Customer/5
        public void DeleteCustomer(Guid id)
        {
        }
    }
}
