using Microsoft.Extensions.DependencyInjection;
using PruebaLogan.CORE.Core;
using PruebaLogan.CORE.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaLogan.IOC.Dependecies
{
    public class Dependencies
    {
        public static void RegistrarDependencias(IServiceCollection services)
        {
            services.AddScoped<IUserCore, UserCore>();
            services.AddScoped<IUserTypeCore, UserTypeCore>();


        }
    }
}
