using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.InfraStructure.Data;
using Ordering.InfraStructure.Data.Intercepteors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingInfra.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddAppliactionServices
            (this IServiceCollection Services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DataBase");

            Services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptore>();
            Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();

            Services.AddDbContext<ApplicationDBContext>((sp , options) => {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
                });


            return Services;
        }

    }
}
