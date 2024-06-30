using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoRate.Common.Auth
{
   public interface IJwtHandler
    {
        JsonWebToken Create(int userId);
    }
}
