using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class UsersController : ApiController
    {
        CityAppEntities model = new CityAppEntities();
        public SharedUser Get(string userName, string password)
        {
            var tempUser = model.Users.SingleOrDefault(x => x.UserName == userName && x.Password == HashMethods.GetStringSha256Hash(password));
            if (tempUser != null)
            {
                return new SharedUser()
                {
                    DateOfBirth = tempUser.DateOfBirth,
                    FirstName = tempUser.FirstName,
                    LastName = tempUser.LastName,
                    UserID = tempUser.PK_UserID,
                    UserName = tempUser.UserName
                };
            }
            else
            {
                return null;
            }
        }
    }
}
