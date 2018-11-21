using Newtonsoft.Json;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class UserController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        #region CRUD

        public IEnumerable<SharedUser> Get()
        {
            return model.Users.Select(x => new SharedUser()
            {
                DateOfBirth = x.DateOfBirth,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Password = x.Password,
                PK_UserID = x.PK_UserID,
                UserName = x.UserName
            });
        }

        public SharedUser Get(Guid id)
        {
            var tempValue = model.Users.SingleOrDefault(x => x.PK_UserID == id);
            return new SharedUser()
            {
                DateOfBirth = tempValue.DateOfBirth,
                FirstName = tempValue.FirstName,
                LastName = tempValue.LastName,
                PK_UserID = tempValue.PK_UserID,
                UserName = tempValue.UserName
            };
        }

        public void Post([FromBody]SharedUser value)
        {
            model.Users.Add(new User()
            {
                DateOfBirth = value.DateOfBirth,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Password = value.Password,
                PK_UserID = value.PK_UserID,
                UserName = value.UserName
            });
            model.SaveChanges();
        }

        public void Put(Guid id, [FromBody]SharedUser value)
        {
            var tempUser = model.Users.SingleOrDefault(x => x.PK_UserID == id);

            if (value.DateOfBirth != DateTime.MinValue) { tempUser.DateOfBirth = value.DateOfBirth; }
            if (!String.IsNullOrEmpty(value.FirstName)) { tempUser.FirstName = value.FirstName; }
            if (!String.IsNullOrEmpty(value.LastName)) { tempUser.LastName = value.LastName; }
            if (!String.IsNullOrEmpty(value.Password)) { tempUser.Password = value.Password; }
            if (!String.IsNullOrEmpty(value.UserName)) { tempUser.UserName = value.UserName; }

            model.SaveChanges();
        }

        public void Delete(Guid id)
        {
            model.Users.Remove(model.Users.SingleOrDefault(x => x.PK_UserID == id));
            model.SaveChanges();
        }

        #endregion

        public SharedUser GetByCredentials(string userName, string password)
        {
            var tempUser = model.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);
            if (tempUser != null)
            {
                return new SharedUser()
                {
                    DateOfBirth = tempUser.DateOfBirth,
                    FirstName = tempUser.FirstName,
                    LastName = tempUser.LastName,
                    PK_UserID = tempUser.PK_UserID,
                    UserName = tempUser.UserName
                };
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<SharedUser> GetByType(string type)
        {
            return model.Users.Where(x => x.Type == type).Select(x => new SharedUser()
            {
                DateOfBirth = x.DateOfBirth,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Password = x.Password,
                PK_UserID = x.PK_UserID,
                UserName = x.UserName,
                Type = x.Type,
                FK_CompanyID = x.FK_CompanyID.HasValue ? x.FK_CompanyID.Value : Guid.Empty
            });
        }

        public IEnumerable<SharedUser> GetFollowerByCompanyId(Guid companyId)
        {
            return model.Followers.Where(x => x.FK_CompanyID == companyId).Select(x => new SharedUser()
            {
                DateOfBirth = x.User.DateOfBirth,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                //Password = x.Customer.User.Password,
                PK_UserID = x.User.PK_UserID,
                UserName = x.User.UserName,
                FK_CompanyID = x.User.FK_CompanyID.HasValue ? x.User.FK_CompanyID.Value : Guid.Empty,
                Type = x.User.Type
            });
        }
    }
}
