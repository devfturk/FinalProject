using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        //Web.apide veya autofacde oluşturduğumuz injectionları oluşturabilmeme yarıyor.
        //İstediğim herhangi bir interfacein servisdeki karşılığını bu tool vasıtasıyla alabilirim
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
