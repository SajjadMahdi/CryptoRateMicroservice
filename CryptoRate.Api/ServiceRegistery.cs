using CryptoRate.Common.Events;
using MassTransit;

namespace Products.Api
{
    public static class ServiceRegistery
    {
        public static IServiceCollection AddMessagingConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
                });

                x.AddConsumers(typeof(IntegrationBaseEvent).Assembly);
            });
            // OPTIONAL, but can be used to configure the bus options
            builder.Services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });

            return builder.Services;

        }
    }
}