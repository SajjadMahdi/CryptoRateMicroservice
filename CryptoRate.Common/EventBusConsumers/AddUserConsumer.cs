using CryptoRate.Common.Commands;
using CryptoRate.Common.Events;
using MassTransit;
using MediatR;

namespace Discount.Application.EventBusConsumers
{
    public class AddUserConsumer : IConsumer<AddUserEvent>
    {
        private readonly IMediator _mediator;

        public AddUserConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<AddUserEvent> context)
        {
            CreateUserCommand createCouponCommand = new CreateUserCommand
            {
                Email = context.Message.Email,
                Name = context.Message.Name,
                Password = context.Message.Password
            };
            var result = await _mediator.Send(createCouponCommand);
        }
    }
}
