using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class CategoryController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        #region CRUD

        public IEnumerable<SharedCategory> Get()
        {
            return model.Categories.Select(x => new SharedCategory()
            {
                Image = x.Image,
                Name = x.Name,
                PK_CategoryID = x.PK_CategoryID
            }).ToList();
        }

        public void Post([FromBody]SharedCategory value)
        {
            model.Categories.Add(new Category()
            {
                Image = value.Image,
                Name = value.Name,
                PK_CategoryID = value.PK_CategoryID
            });

            model.SaveChanges();
        }

        public void Put(Guid id, [FromBody]SharedCategory value)
        {
            var tempValue = model.Categories.SingleOrDefault(x => x.PK_CategoryID == id);

            if (!String.IsNullOrEmpty(value.Image)) { tempValue.Image = value.Image; }
            if (!String.IsNullOrEmpty(value.Name)) { tempValue.Name = value.Name; }

            model.SaveChanges();
        }

        public void Delete(Guid id)
        {
            model.Categories.Remove(model.Categories.SingleOrDefault(x => x.PK_CategoryID == id));
            model.SaveChanges();
        }

        #endregion
    }
}
