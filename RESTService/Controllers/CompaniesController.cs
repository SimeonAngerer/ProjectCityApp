using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTService.Controllers
{
    public class CompaniesController : ApiController
    {
        CityAppEntities model = new CityAppEntities();
      
        public List<SharedCompany> GetAllCompanies()
        {
            return model.Companies.Select(x => new SharedCompany()
            {
                City = x.City,
                CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }

        public List<SharedCompany> GetCompaniesWithPromotion(DateTime searchDate)
        {
            return model.Companies.Where(x => x.Promotions.Count(y => y.Start <= searchDate && searchDate <= y.Expiration) >= 1).Select(x => new SharedCompany()
            {
                City = x.City,
                CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }

        public List<SharedCompany> GetCompaniesByCategoryName(string categoryName)
        {
            return model.Companies.Where(x => x.Category.Name.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase)).Select(x => new SharedCompany()
            {
                City = x.City,
                CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }

        public List<SharedCompany> GetCompaniesByCategoryID(Guid categoryID)
        {
            return model.Companies.Where(x => x.Category.PK_CategoryID == categoryID).Select(x => new SharedCompany()
            {
                City = x.City,
                CompanyID = x.PK_CompanyID,
                Facebook = x.Facebook,
                Image = x.Image,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }

        public List<SharedEvent> GetEventsByCompanyID(Guid companyID)
        {
            return model.Events.Where(x => x.Company.PK_CompanyID == companyID).Select(x => new SharedEvent()
            {
                City = x.City,
                Date = x.Date,
                EventID = x.PK_EventID,
                Name = x.Name,
                Street = x.Street,
                ZipCode = x.Zipcode
            }).ToList();
        }

    }
}
