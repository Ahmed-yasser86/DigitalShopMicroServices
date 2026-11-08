using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace OrderingApplication.Application
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddAppliactionServices(this IServiceCollection Services)
        {

            Services.AddMediatR(cfg => cfg.
            RegisterServicesFromAssembly
            (Assembly.GetExecutingAssembly()));
            return Services;
        }
    }
}