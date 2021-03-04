using System.Threading.Tasks;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<bool> RegUser(string login, string password, string confirmPassword);
    }
}
