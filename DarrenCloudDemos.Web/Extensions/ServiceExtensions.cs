using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureDictionary<TOptions>(this IServiceCollection services, IConfigurationSection section) where TOptions : class, IDictionary<string, string>
        {
            var values = section.GetChildren().ToList();

            services.Configure<TOptions>(x => values.ForEach(v => x.Add(v.Key, v.Value)));

            return services;
        }
    }
}
