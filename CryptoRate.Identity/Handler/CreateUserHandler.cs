using CryptoRate.Common.Commands;
using CryptoRate.Services.Identity.Services;
using MediatR;

namespace CryptoRate.Identity.Handler
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.Register(request.Email, request.Password, request.Name);
            return 0;
        }
    }
}
