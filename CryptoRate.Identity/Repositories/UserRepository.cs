using CryptoRate.Common.Models;
using CryptoRate.Services.Identity.Domain.Models;
using CryptoRate.Services.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
namespace CryptoRate.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MicroserviceDbContext _context;

        public UserRepository(MicroserviceDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Models.User> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return new Domain.Models.User()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Salt = user.Salt
            };
        }

        public async Task<Domain.Models.User> GetByEmail(string email)
        {
            var user = await _context.Users
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

            return new Domain.Models.User()
            {
                Email = email,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Salt = user.Salt
            };
        }

        public async Task AddUser(Domain.Models.User user)
        {
            await _context.Users.AddAsync(new Common.Models.User
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Salt = user.Salt
            });
            await _context.SaveChangesAsync();
        }

    }
}
