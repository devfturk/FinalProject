using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module // using Autofac sen bir modülsün.
        //startup'da kurduğumuz yapıyı autofacle kurmamızı sağlar.
    {
        //override Load.
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            //services.addsingleton|ProductManager instance ver ne zaman birisi senden
            //Iproductservice istediğinde|tek bir instance oluştur, datanın kendisini tutmaz.

            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            //yukarıdaki açıklama bu satırın classları içinde geçerlidir.
            //bu aşamalardan sonra .net'e kendi autofac moduülümü kullanmak istediğimi belirmeliyim.
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();//ders 13 ders sonu. Business RUle için.
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();//çalışan uygulama içerisinde

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()//implemente edilmiş interfaceleri bul 
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()//onlar için aspect interceptorselector'ı çağır. bütün sınıflar için burayı çalıştırıyor.
                    //git bak bu adamın bir aspect'i var mı '[....]' bu demek.
                }).SingleInstance();
        }
    }
}
