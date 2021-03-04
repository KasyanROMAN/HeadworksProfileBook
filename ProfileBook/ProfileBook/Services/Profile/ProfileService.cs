using Prism.Services;
using Prism.Services.Dialogs;
using ProfileBook.Dialogs;
using ProfileBook.Services.Repository;
using ProfileBook.Validators;
using System;
using System.Threading.Tasks;
using ProfileBook.Properties;
using ProfileBook.Localization;

namespace ProfileBook.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Models.DataProfile> repository;
        private readonly IPageDialogService pageDialogService;
        private readonly IDialogService dialogService;
        private readonly LocalizedResources resources;
        public ProfileService(IRepository<Models.DataProfile> repository, IPageDialogService pageDialogService, IDialogService dialogService)
        {
            this.repository = repository;
            this.pageDialogService = pageDialogService;
            this.dialogService = dialogService;
        }
        public async Task<string> GetImagePath(Models.DataProfile profile)
        {
            IDialogResult result = await dialogService.ShowDialogAsync(nameof(PickImageDialog));
            string img_path = result.Parameters.GetValue<string>("ImagePath");

            if (img_path != null)
            {
                profile.Image = img_path;
                await repository.Update(profile);

                return img_path;
            }

            return null;
        }
        public async Task<bool> SaveProfile(Models.DataProfile profile)
        {
            string hints = ValidationHints.GetProfileHints(profile, resources);

            if (!hints.Equals(String.Empty)) {
                await pageDialogService.DisplayAlertAsync(resources["AddEditAlertTitle"], hints, resources["Confirm"]);
                return false;
            }
            await repository.AddOrUpdata(profile);
            return true;
        }
    }
}
