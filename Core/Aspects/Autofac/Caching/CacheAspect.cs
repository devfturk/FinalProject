using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");//Relectedtype -> namespace, .fullname -> base sınıfı method name->method ismi
                                                                                                                   //örnek -> nameSpace.IProductService.GetAll
            var arguments = invocation.Arguments.ToList();// method'un parametrelerini listeye çevir.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";//parametre değeri varsa getall sonrası "(..params) olarak koyuyorum./yoksa null geçeriz.
            if (_cacheManager.IsAdd(key))//cachede var mı?
            {
                invocation.ReturnValue = _cacheManager.Get(key);//methodu hiç çalıştırmadan şimdi geridön -> return value'de bu olsun.
                return;
            }
            invocation.Proceed();//methodu devam ettir.
            _cacheManager.Add(key, invocation.ReturnValue, _duration);//cache'e değeri ekle
        }
    }
}
