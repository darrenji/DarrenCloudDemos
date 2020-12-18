using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Infrastructure;
using DDD.Domain.OrderAggregate;

namespace DDD.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OrderingContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(Order).Assembly, typeof(Program).Assembly);
        }
    }
}
