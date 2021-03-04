
using ProfileBook.Models;
using System;
using System.IO;

namespace ProfileBook.Services
{
    public static class LocalService
    {
        private static string user_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "user_data.dat");
        private static string settings_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "settings.config");
        public static Models.Settings GetDefaultSettings()
        {
            var settings = new Models.Settings {
              
                UserId = App.CurrentUser.Id
            };
            return settings;
        }
        public static void SaveUser(User user)
        {
            delete_user();
            File.WriteAllText(user_path,
                $"{user.Id}|" +
                $"{user.Login}|" +
                $"{user.Password}");
            App.CurrentUser = user;
        }
        public static void SaveSettings(Models.Settings settings)
        {
            delete_settings();
            File.WriteAllText(settings_path,
                    $"{settings.Id}|" +
                 
                    $"{settings.UserId}");
            App.CurrentSettings = settings;
        }
        public static User ReadUser()
        {
            if (!File.Exists(user_path)) {
                return null;
            }
            string user_data = File.ReadAllText(user_path);
            string[] data = user_data.Split('|');

            try {
                var user = new User {
                    Id = Int32.Parse(data[0]),
                    Login = data[1],
                    Password = data[2],
                };
                return user;
            }
            catch (IndexOutOfRangeException) {
                return null;
            }
        }
        public static Models.Settings ReadSettings()
        {
            if (!File.Exists(settings_path)) {
                return null;
            }

            string settings_data = File.ReadAllText(settings_path);
            string[] data = settings_data.Split('|');

            try {
                var settings = new Models.Settings {
                    Id = Int32.Parse(data[0]),
                 
                    UserId = Int32.Parse(data[4])
                };
                return settings;
            }
            catch (IndexOutOfRangeException) {
                return null;
            }
        }
        public static void Delete()
        {
            delete_user();
            delete_settings();
        }

        private static void delete_user()
        {
            if (File.Exists(user_path)) {
                File.Delete(user_path);
            }
            App.CurrentUser = null;
        }
        private static void delete_settings()
        {
            if (File.Exists(settings_path)) {
                File.Delete(settings_path);
            }
            App.CurrentSettings = null;
        }
    }
}
