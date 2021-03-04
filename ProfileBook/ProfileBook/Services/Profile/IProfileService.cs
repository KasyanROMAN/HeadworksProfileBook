using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public interface IProfileService
    {
        Task<bool> SaveProfile(Models.DataProfile profile);
        Task<string> GetImagePath(Models.DataProfile profile);
    }
}
