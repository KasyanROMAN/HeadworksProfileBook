using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.Services.Main;
using ProfileBook.Views;
using ProfileBook.Views.Main;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Localization;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        private readonly IMainService mainService;

        public ObservableCollection<DataProfile> ProfileList { get; set; }

        private bool isEmpty;
        public bool IsEmpty {
            get => isEmpty;
            set => SetProperty(ref isEmpty, value, nameof(IsEmpty));
        }

        public MainListPageViewModel(INavigationService navigationService, IMainService mainService) : base(navigationService)
        {
            App.CurrentSettings = LocalService.ReadSettings();

            this.mainService = mainService;
            this.IsEmpty = true;
            this.ProfileList = new ObservableCollection<DataProfile>();

            this.LogOutCommand = new Command(executeLogOut);
            this.AddProfileCommand = new Command(executeAddProfile);
            this.SettingsCommand = new Command(executeSettingsCommand);
        }
        private async Task updateProfileList()
        {
            this.ProfileList.Clear();
            var profiles = await mainService.GetProfiles(App.CurrentUser.Id);

            if (profiles != null) {
                profiles.ToList().ForEach(item => {
                    item.RemoveProfile += Item_RemoveProfile;
                    item.EditProfile += Item_EditProfile;
                    item.ShowImage += Item_ShowImage;
                    this.ProfileList.Add(item);
                });
            }

            UpdateLabel();
        }

        public ICommand LogOutCommand { get; private set; }

  
        private async void executeLogOut()
        {
         
            LocalService.Delete();
            await NavigationService.NavigateAsync($"/NavigationPage/{nameof(SignInPage)}");
        }

        public ICommand AddProfileCommand { get; private set; }

        private async void executeAddProfile()
        {
            await NavigationService.NavigateAsync(nameof(AddEditPage));
        }
        
        public ICommand SettingsCommand { get; private set; }

        private async void executeSettingsCommand()
        {
            await NavigationService.NavigateAsync(nameof(SettingsPage));
        }


        private async void Item_ShowImage(object sender, System.EventArgs e)
        {
            if (sender is DataProfile profile) {
                await this.mainService.ShowImage(profile.Image);
            }
        }

        private async void Item_EditProfile(object sender, System.EventArgs e)
        {
            if (sender is DataProfile profile) {
                var nav_params = new NavigationParameters {
                    { "Profile", profile }
                };
                await NavigationService.NavigateAsync("AddEditPage", nav_params);
            }
        }
        private async void Item_RemoveProfile(object sender, System.EventArgs e)
        {
            if (sender is DataProfile profile && await mainService.RemoveProfile(profile)) {
                profile.EditProfile -= Item_EditProfile;
                profile.RemoveProfile -= Item_RemoveProfile;
                profile.ShowImage -= Item_ShowImage;
                this.ProfileList.Remove(profile);
                UpdateLabel();
            }
        }


        private void UpdateLabel()
        {
            IsEmpty = (ProfileList.Count <= 0);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (App.UpdateList) {
                await updateProfileList();
                App.UpdateList = false;
            }
            
        }
    }
}
