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
        public SharedUser Get(string userName, string password)
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

        public List<SharedUser> GetByType(string type)
        {
            if (type == "Entrepreneur")
            {
                return model.Users.Where(x => model.Entrepreneurs.Count(y => y.FK_UserID == x.PK_UserID) > 0).Select(x => new SharedUser()
                {
                    DateOfBirth = x.DateOfBirth,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Password = x.Password,
                    PK_UserID = x.PK_UserID,
                    UserName = x.UserName
                }).ToList();
            }
            else if (type == "Customer")
            {
                return model.Users.Where(x => model.Customers.Count(y => y.FK_UserID == x.PK_UserID) > 0).Select(x => new SharedUser()
                {
                    DateOfBirth = x.DateOfBirth,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Password = x.Password,
                    PK_UserID = x.PK_UserID,
                    UserName = x.UserName
                }).ToList();
            }
            else
            {
                return null;
            }
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
    }
}
