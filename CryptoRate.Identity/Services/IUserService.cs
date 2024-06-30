using CryptoRate.Common.Auth;

namespace CryptoRate.Services.Identity.Services
{
    public interface IUserService
    {
        Task Register(string email, string password, string name);
        Task<JsonWebToken> Login(string email, string password);
    }
}
