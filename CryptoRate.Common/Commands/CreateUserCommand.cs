using CryptoRate.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoRate.Common.Commands
{
    public class CreateUserCommand : User,IRequest<int>
    {
    }
}
