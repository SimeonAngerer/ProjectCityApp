using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class FollowerController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        #region CRUD

        public IEnumerable<SharedFollower> Get()
        {
            return model.Followers.Select(x => new SharedFollower()
            {
                FK_CompanyID = x.FK_CompanyID,
                FK_CustomerID = x.FK_CustomerID,
                PK_FollowerID = x.PK_FollowerID
            });
        }

        public void Post([FromBody]SharedFollower value)
        {
            model.Followers.Add(new Follower()
            {
                FK_CompanyID = value.FK_CompanyID,
                FK_CustomerID = value.FK_CustomerID,
                PK_FollowerID = value.PK_FollowerID
            });
            model.SaveChanges();
        }

        //public void Put(Guid id, [FromBody]SharedFollower value)
        //{
        //}

        public void Delete(Guid id)
        {
            model.Followers.Remove(model.Followers.SingleOrDefault(x => x.PK_FollowerID == id));
            model.SaveChanges();
        }

        #endregion

        public void Follow(Guid companyGuid, Guid userId)
        {
            model.Followers.Add(new Follower()
            {
                FK_CompanyID = companyGuid,
                FK_CustomerID = model.Customers.Single(x => x.FK_UserID == userId).PK_CustomerID,
                PK_FollowerID = Guid.NewGuid()
            });
            model.SaveChanges();
        }
    }
}
