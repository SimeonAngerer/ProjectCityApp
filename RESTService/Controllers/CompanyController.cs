﻿using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class CompanyController : ApiController
    {
        CityAppEntities model = new CityAppEntities();

        #region CRUD

        public IEnumerable<SharedCompany> Get()
        {
            return model.Companies.Select(x => new SharedCompany()
            {
                City = x.City,
                PK_CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode,
                FK_CategoryID = x.FK_CategoryID,
                Description = x.Description
            }).ToList();
        }

        public SharedCompany Get(Guid id)
        {
            var tempValue = model.Companies.SingleOrDefault(x => x.PK_CompanyID == id);
            return new SharedCompany()
            {
                City = tempValue.City,
                Facebook = tempValue.Facebook,
                FK_CategoryID = tempValue.FK_CategoryID,
                Image = tempValue.Image,
                Name = tempValue.Name,
                PK_CompanyID = tempValue.PK_CompanyID,
                Street = tempValue.Street,
                ZipCode = tempValue.Zipcode,
                Description = tempValue.Description
            };
        }
        public void Post([FromBody]SharedCompany value)
        {
            model.Companies.Add(new Company()
            {
                City = value.City,
                Facebook = value.Facebook,
                FK_CategoryID = value.FK_CategoryID,
                Image = value.Image,
                Name = value.Name,
                PK_CompanyID = value.PK_CompanyID,
                Street = value.Street,
                Zipcode = value.ZipCode,
                Description = value.Description
            });

            model.SaveChanges();
        }

        public void Put(Guid id, [FromBody]SharedCompany value)
        {
            var tempValue = model.Companies.SingleOrDefault(x => x.PK_CompanyID == id);

            if (!String.IsNullOrEmpty(value.City)) { tempValue.City = value.City; }
            if (!String.IsNullOrEmpty(value.Facebook)) { tempValue.Facebook = value.Facebook; }
            if (!String.IsNullOrEmpty(value.Image)) { tempValue.Image = value.Image; }
            if (!String.IsNullOrEmpty(value.Name)) { tempValue.Name = value.Name; }
            if (!String.IsNullOrEmpty(value.Street)) { tempValue.Street = value.Street; }
            if (!String.IsNullOrEmpty(value.ZipCode)) { tempValue.Zipcode = value.ZipCode; }
            if (!String.IsNullOrEmpty(value.Description)) { tempValue.Description = value.Description; }

            model.SaveChanges();
        }

        public void Delete(Guid id)
        {
            model.Companies.Remove(model.Companies.SingleOrDefault(x => x.PK_CompanyID == id));
            model.SaveChanges();
        }

        #endregion

        public IEnumerable<SharedCompany> GetCompaniesWithPromotion(DateTime searchDate)
        {
            var asdf =  model.Companies.Where(x => x.Promotions.Count(y => y.Start <= searchDate && searchDate <= y.Expiration) >= 1).Select(x => new SharedCompany()
            {
                City = x.City,
                PK_CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode,
                FK_CategoryID = x.FK_CategoryID,
                Description = x.Description
            });
            return asdf;
        }

        public IEnumerable<SharedCompany> GetCompaniesByCategoryName(string categoryName)
        {
            return model.Companies.Where(x => x.Category.Name.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase)).Select(x => new SharedCompany()
            {
                City = x.City,
                PK_CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode,
                FK_CategoryID = x.FK_CategoryID,
                Description = x.Description
            });
        }

        public IEnumerable<SharedCompany> GetCompaniesByCategoryID(Guid categoryID)
        {
            return model.Companies.Where(x => x.Category.PK_CategoryID == categoryID).Select(x => new SharedCompany()
            {
                City = x.City,
                PK_CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode,
                FK_CategoryID = x.FK_CategoryID,
                Description = x.Description
            });
        }

        public IEnumerable<SharedCompany> GetCompaniesBySearchString(string searchString)
        {
            //Thread.Sleep(500);
            return model.Companies.Where(x => String.IsNullOrEmpty(searchString) || x.Name.Contains(searchString) || x.Street.Contains(searchString) || x.City.Contains(searchString)).Select(x => new SharedCompany()
            {
                City = x.City,
                PK_CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode,
                FK_CategoryID = x.FK_CategoryID,
                Description = x.Description
            });
        }

        public IEnumerable<SharedCompany> GetFollowedCompaniesByUser(Guid userID)
        {
            return model.Followers.Where(x => x.FK_UserID == userID).Select(x => new SharedCompany()
            {
                City = x.Company.City,
                PK_CompanyID = x.Company.PK_CompanyID,
                Facebook = x.Company.Facebook,
                Image = x.Company.Image,
                Name = x.Company.Name,
                Street = x.Company.Street,
                ZipCode = x.Company.Zipcode,
                FK_CategoryID = x.Company.FK_CategoryID,
                Description = x.Company.Description
            });
        }
    }
}
