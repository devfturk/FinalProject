using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        //i�lemleri a�a��daki fonksiyonda ger�ekle�tiriyoruz. Dok�mandan yard�m alarak yapmak faydal�.
        //
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//service sa�lay�c� fabrikas� olarak kullan(new AutofacServiceProviderFactory) install package. Dependency�njection.
                .ConfigureContainer<ContainerBuilder>(builder => //AutofacBusinessModule nerede.
                {
                    builder.RegisterModule(new AutofacBusinessModule()); // mod�l� kaydet.
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
