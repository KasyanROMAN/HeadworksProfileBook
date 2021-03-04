using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> SignIn(string login, string password);
    }
}
