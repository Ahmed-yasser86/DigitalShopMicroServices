using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingInfra.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddAppliactionServices(this IServiceCollection Services, IConfiguration configuration)
        {
            var GetConnectionString = configuration.GetConnectionString("DataBase");

            return Services;
        }

    }
}
