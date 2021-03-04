using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Main
{
    public interface IMainService
    {
        Task<IEnumerable<Models.DataProfile>> GetProfiles(int user_id);
        Task<bool> RemoveProfile(Models.DataProfile profile);
        Task ShowImage(string image_path);
        Task<Models.Settings> GetSettings();
    }
}
