using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class CategoriesController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        public List<SharedCategory> GetAllCategories()
        {
            return model.Categories.Select(x => new SharedCategory()
            {
                CategoryID = x.PK_CategoryID,
                Name = x.Name
            }).ToList();
        }
    }
}
