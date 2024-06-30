using CryptoRate.Services.Identity.Domain.Services;
using CryptoRate.Common.Exeptions;

namespace CryptoRate.Services.Identity.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get;set; }
        public string Salt { get;set; }

        public User()
        {
            
        }

        public User(string email,string name)
        {

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new CryptoExeption("empty_user_email",
                    "User email can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new CryptoExeption("empty_user_name",
                    "User name can not be empty.");
            }
            Email = email.ToLowerInvariant();
            Name = name;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new CryptoExeption("empty_password",
                    "Password can not be empty.");
            }

            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter) =>
            Password.Equals(encrypter.GetHash(password, Salt));
    }
}
