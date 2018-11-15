using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCityAppUWP.ViewModels
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<CategoriesPageViewModel>();
            SimpleIoc.Default.Register<CompaniesPageViewModel>();
            SimpleIoc.Default.Register<CompanyDetailPageViewModel>();
            SimpleIoc.Default.Register<EventAdministrationPageViewModel>();
            SimpleIoc.Default.Register<EventDetailPageViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<PromotionAdministrationPageViewModel>();
            SimpleIoc.Default.Register<PromotionDetailPageViewModel>();
            SimpleIoc.Default.Register<SearchPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<UserAdministrationPageViewModel>();
            SimpleIoc.Default.Register<UserMainPageViewModel>();
            SimpleIoc.Default.Register<UserSignInPageViewModel>();
            SimpleIoc.Default.Register<UserSignUpPageViewModel>();
        }

        public CategoriesPageViewModel Categories
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoriesPageViewModel>();
            }
        }
        public CompaniesPageViewModel Companies
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompaniesPageViewModel>();
            }
        }
        public CompanyDetailPageViewModel CompanyDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompanyDetailPageViewModel>();
            }
        }
        public EventAdministrationPageViewModel EventAdministration
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EventAdministrationPageViewModel>();
            }
        }
        public EventDetailPageViewModel EventDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EventDetailPageViewModel>();
            }
        }
        public MainPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }
        public PromotionAdministrationPageViewModel PromotionAdministration
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PromotionAdministrationPageViewModel>();
            }
        }
        public PromotionDetailPageViewModel PromotionDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PromotionDetailPageViewModel>();
            }
        }
        public SearchPageViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchPageViewModel>();
            }
        }
        public SettingsPageViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsPageViewModel>();
            }
        }
        public ShellViewModel Shell
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ShellViewModel>();
            }
        }
        public UserAdministrationPageViewModel UserAdministration
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserAdministrationPageViewModel>();
            }
        }
        public UserMainPageViewModel UserMain
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserMainPageViewModel>();
            }
        }
        public UserSignInPageViewModel UserSignIn
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserSignInPageViewModel>();
            }
        }
        public UserSignUpPageViewModel UserSignUp
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserSignUpPageViewModel>();
            }
        }
    }
}
