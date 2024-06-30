using CryptoRate.Common.Events;
using Discount.Application.EventBusConsumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CryptoRate.Identity
{
    public static class ServiceRegistery
    {
        public static IServiceCollection AddMessagingConfiguration(this WebApplicationBuilder builder)
        {

            builder.Services.AddMassTransit(
               config =>
               {
                   config.AddConsumer<AddUserConsumer>();
                   config.UsingRabbitMq((ctx, cfg) =>
                   {
                       cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
                       cfg.ReceiveEndpoint(EventBusConstants.AddUserQueue, c =>
                       {
                           c.ConfigureConsumer<AddUserConsumer>(ctx);
                       });


                   }
                   );
               });
            return builder.Services;
        }

    }
}