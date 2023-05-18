using Microsoft.Extensions.DependencyInjection;
using QueueForChildren.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
