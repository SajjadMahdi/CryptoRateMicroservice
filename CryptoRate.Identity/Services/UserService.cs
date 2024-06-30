using CryptoRate.Common.Auth;
using CryptoRate.Common.Exeptions;
using CryptoRate.Services.Identity.Domain.Repositories;
using CryptoRate.Services.Identity.Domain.Services;

namespace CryptoRate.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }
        public async Task Register(string email, string password, string name)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                throw new CryptoExeption("email_in_use",
                    $"Email :'{email}' is already in use");
            }

            user = new Domain.Models.User(email, name);
            user.SetPassword(password, _encrypter);
            await _userRepository.AddUser(user);
        }

        public async Task<JsonWebToken> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                throw new CryptoExeption("invalid_credentials",
                    $"Invalid User");
            }

            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new CryptoExeption("invalid_credentials",
                    $"Invalid User");
            }

            return _jwtHandler.Create(user.Id);
        }
    }
}
