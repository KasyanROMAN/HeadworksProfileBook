using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Settings
{
    public interface ISettingsService
    {
        Task UpdateSettings(Models.Settings settings);
    }
}
