using Prism.Services;
using Prism.Services.Dialogs;
using ProfileBook.Dialogs;
using ProfileBook.Services.Repository;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProfileBook.Properties;

namespace ProfileBook.Services.Main
{
    public class MainService : IMainService
    {
        private readonly IRepository<Models.DataProfile> profileRepository;
        private readonly IRepository<Models.Settings> settingsRepository;
        private readonly IPageDialogService pageDialogService;
        private readonly IDialogService dialogService;
        public async Task<Models.Settings> GetSettings()
        {
            string sqlCommand = $"SELECT * FROM Settings WHERE UserId='{App.CurrentUser.Id}'";
            Models.Settings result = await settingsRepository.FindWithCommand(sqlCommand);

            if (result != null) {
                return result;
            }
            result = LocalService.GetDefaultSettings();
            await settingsRepository.Add(result);

            return result;
        }


        public async Task<IEnumerable<Models.DataProfile>> GetProfiles(int user_id)
        {
            if (App.CurrentSettings == null) {
                App.CurrentSettings = await GetSettings();
            }

            string sqlCommand = $"SELECT * FROM Profiles WHERE UserId='{App.CurrentUser.Id}' ORDER BY ";
            return await profileRepository.GetAllWithCommand(sqlCommand);
        }

        public async Task<bool> RemoveProfile(Models.DataProfile profile)
        {
            if (await pageDialogService.DisplayAlertAsync(AppResources.MainDelete, AppResources.MainDeleteConfirm, AppResources.Accept, AppResources.Denie)) {

                if (File.Exists(profile.Image))
                    File.Delete(profile.Image);

                await profileRepository.Remove(profile);
                return true;
            }
            return false;
        }

        public async Task ShowImage(string image_path)
        {
            await dialogService.ShowDialogAsync(nameof(ShowImageDialog), new DialogParameters {
                {"Image", ImageSource.FromFile(image_path) }
            });
        }

        public MainService(IRepository<Models.DataProfile> profileRepository, IRepository<Models.Settings> settingsRepository, IPageDialogService pageDialogService, IDialogService dialogService)
        {
            this.profileRepository = profileRepository;
            this.settingsRepository = settingsRepository;
            this.pageDialogService = pageDialogService;
            this.dialogService = dialogService;
        }
    }
}
