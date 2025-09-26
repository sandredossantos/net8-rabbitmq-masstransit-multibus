using MassTransit;
using Messaging.Abstractions;
using Messaging.Events;
using Messaging.Producers;
using Messaging.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPublishers(this IServiceCollection services)
        {
            services.AddScoped<IEventPublisher<CreatedEvent>, EventPublisher<CreatedEvent, IPrimaryBus>>();
            services.AddScoped<IEventPublisher<UpdatedEvent>, EventPublisher<UpdatedEvent, ISecondaryBus>>();

            return services;
        }

        public static IServiceCollection AddProducers(this IServiceCollection services)
        {
            services.AddScoped<ICreatedProducer, CreatedProducer>();
            services.AddScoped<IUpdatedProducer, UpdatedProducer>();

            return services;
        }

        public static IServiceCollection AddMassTransitBind(this IServiceCollection services)
        {
            services.AddMassTransit<IPrimaryBus>(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 5673, "/", h =>
                    {
                        h.Username("user1");
                        h.Password("pass1");
                    });

                    cfg.ClearSerialization();
                    cfg.UseRawJsonDeserializer();
                    cfg.UseRawJsonSerializer();

                    cfg.Message<CreatedEvent>(m => m.SetEntityName("exchange-created"));

                    cfg.Publish<CreatedEvent>(p =>
                    {
                        p.ExchangeType = "direct";
                    });

                    cfg.Send<CreatedEvent>(s =>
                    {
                        s.UseRoutingKeyFormatter(_ => "routing-key-01");
                    });
                });
            });

            services.AddMassTransit<ISecondaryBus>(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 5674, "/", h =>
                    {
                        h.Username("user2");
                        h.Password("pass2");
                    });

                    cfg.ClearSerialization();
                    cfg.UseRawJsonDeserializer();
                    cfg.UseRawJsonSerializer();

                    cfg.Message<UpdatedEvent>(m => m.SetEntityName("exchange-updated"));

                    cfg.Publish<UpdatedEvent>(p =>
                    {
                        p.ExchangeType = "direct";
                    });

                    cfg.Send<UpdatedEvent>(s =>
                    {
                        s.UseRoutingKeyFormatter(_ => "routing-key-02");
                    });
                });
            });

            return services;
        }
    }
}
