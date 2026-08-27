using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingDomain.Application
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddAppliactionServices(this IServiceCollection Services)
        {


            return Services;
        }
    }
}
