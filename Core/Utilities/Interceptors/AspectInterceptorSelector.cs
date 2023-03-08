using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();//git classın attiributelerini oku listeye koy
            var methodAttributes = type.GetMethod(method.Name)//git methodun attiributelerini oku
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);//listeye koy
            //loglama altyapısı eklenebilir.

            return classAttributes.OrderBy(x => x.Priority).ToArray();//Çalışma sırasını da priorty'e göre sırala
        }
    }
}
