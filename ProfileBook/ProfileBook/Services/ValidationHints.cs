using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ProfileBook.Properties;
using ProfileBook.Localization;

namespace ProfileBook.Validators
{
    public static class ValidationHints
    {
        public static string GetSignUpHints(string login, string password, string confirmPassword)
        {
            string alert = String.Empty;
            if (!Regex.IsMatch(login, @"^[a-zA-Z][a-zA-Z0-9]{3,16}$")) {
                alert +=  "Login:\n * Minimum 4 chars, maximum 16 chars.\n * Can not starts with number\n";
            }
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$")) {
                alert += "Passsword:\n * Minimum 8 chars, maximum 16 chars.\n * At least 1 uppercase letter, 1 lowercase letter and 1 number\n";
            }
            if (!confirmPassword.Equals(password)) {
                alert += "Confirm password:\n * Must match the password\n";
            }

            return alert;
        }

        public static string GetProfileHints(DataProfile profile, LocalizedResources resources)
        {
            string alert = String.Empty;

            if (profile.Name == null || profile.Name.Equals(String.Empty)) {
                alert += resources["AddEditValidateName"];
            }
            if (profile.NickName == null || profile.NickName.Equals(String.Empty)) {
                alert += resources["AddEditValidateNickName"];
            }

            return alert;
        }
    }
}
