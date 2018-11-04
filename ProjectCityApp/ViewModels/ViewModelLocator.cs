using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCityApp.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<CategoriesViewModel>(true);
            SimpleIoc.Default.Register<LogOffViewModel>(true);
            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<PromotedViewModel>(true);
            SimpleIoc.Default.Register<SearchViewModel>(true);
            SimpleIoc.Default.Register<SettingsViewModel>(true);
            SimpleIoc.Default.Register<StartViewModel>(true);
            SimpleIoc.Default.Register<UserViewModel>(true);
            SimpleIoc.Default.Register<CompaniesViewModel>(true);
        }

        public CategoriesViewModel Categories
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoriesViewModel>();
            }
        }

        public LogOffViewModel LogOff
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LogOffViewModel>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public PromotedViewModel Promoted
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PromotedViewModel>();
            }
        }

        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public StartViewModel Start
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartViewModel>();
            }
        }

        public UserViewModel User
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserViewModel>();
            }
        }

        public CompaniesViewModel Companies
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompaniesViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
