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
        // GET: api/Entrepreneur
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Entrepreneur/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Entrepreneur
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Entrepreneur/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Entrepreneur/5
        public void Delete(int id)
        {
        }
    }
}
