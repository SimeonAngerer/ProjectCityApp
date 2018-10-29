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
        // GET: api/Discount
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Discount/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Discount
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Discount/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Discount/5
        public void Delete(int id)
        {
        }
    }
}
