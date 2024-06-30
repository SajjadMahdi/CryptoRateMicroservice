
using CryptoRate.Services.Identity.Domain.Models;

namespace CryptoRate.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task AddUser(User user);

    }
}
