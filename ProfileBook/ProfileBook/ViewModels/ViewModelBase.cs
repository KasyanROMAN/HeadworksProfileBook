﻿using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Localization;
using ProfileBook.Properties;

namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        public LocalizedResources Resources { get; private set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            
               
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
