using Helpers;
using SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class DataHandler
    {
        CityAppEntities model = new CityAppEntities();

        public List<SharedCompany> GetAllCompanies()
        {
            return model.Companies.Select(x => new SharedCompany(){
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

        public SharedUser CheckCredentials(string userName, string password)
        {
            var tempUser = model.Users.SingleOrDefault(x => x.UserName == userName && x.Password == HashMethods.GetStringSha256Hash(password));
            if(tempUser != null)
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
