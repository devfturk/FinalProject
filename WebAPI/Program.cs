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
        //iþlemleri aþaðýdaki fonksiyonda gerçekleþtiriyoruz. Dokümandan yardým alarak yapmak faydalý.
        //
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//service saðlayýcý fabrikasý olarak kullan(new AutofacServiceProviderFactory) install package. DependencyÝnjection.
                .ConfigureContainer<ContainerBuilder>(builder => //AutofacBusinessModule nerede.
                {
                    builder.RegisterModule(new AutofacBusinessModule()); // modülü kaydet.
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
