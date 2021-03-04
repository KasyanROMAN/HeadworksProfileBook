using Prism;
using Prism.Ioc;
using ProfileBook.Dialogs;
using ProfileBook.Models;
using ProfileBook.Repositories;
using ProfileBook.Services;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Main;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.ViewModels;
using ProfileBook.ViewModels.Dialogs;
using ProfileBook.Views;
using System;
using System.IO;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
namespace ProfileBook
{
    public partial class App
    {
        public static User CurrentUser { get; set; }
        public static Settings CurrentSettings { get; set; }
        public static bool UpdateList { get; set; }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            CurrentUser = LocalService.ReadUser();

            if (CurrentUser == null)
            {
                await NavigationService.NavigateAsync("NavigationPage/SignInPage");
            }
            else
            {
                UpdateList = true;
                await NavigationService.NavigateAsync("NavigationPage/MainListPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register(typeof(IRepository<>), typeof(Repository<>));
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.Register<IAuthorizationService, AuthorizationService>();
            containerRegistry.Register<IMainService, MainService>();
            containerRegistry.Register<IProfileService, ProfileService>();
            containerRegistry.Register<ISettingsService, SettingsService>();
            containerRegistry.RegisterDialog<ShowImageDialog, ShowImageDialogViewModel>();
            containerRegistry.RegisterDialog<PickImageDialog, PickImageDialogViewModel>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, RegistrationPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MainListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPage, AddEditPageViewModel>();
        }

    }
}
