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
        CityAppEntities model = new CityAppEntities();

        #region CRUD

        public IEnumerable<SharedCustomer> Get()
        {
            return model.Customers.Select(x => new SharedCustomer()
            {
                FK_UserID = x.FK_UserID,
                PK_CustomerID = x.PK_CustomerID
            });
        }

        public SharedCustomer Get(Guid id)
        {
            var tempValue = model.Customers.SingleOrDefault(x => x.PK_CustomerID == id);

            return new SharedCustomer()
            {
                FK_UserID = tempValue.FK_UserID,
                PK_CustomerID = tempValue.PK_CustomerID
            };
        }

        public void Post([FromBody]SharedCustomer value)
        {
            model.Customers.Add(new Customer()
            {
                FK_UserID = value.FK_UserID,
                PK_CustomerID = value.PK_CustomerID
            });
            model.SaveChanges();
        }

        //public void Put(Guid id, [FromBody]SharedCustomer value)
        //{
        //}

        public void Delete(Guid id)
        {
            model.Customers.Remove(model.Customers.SingleOrDefault(x => x.PK_CustomerID == id));
            model.SaveChanges();
        }

        #endregion
    }
}
