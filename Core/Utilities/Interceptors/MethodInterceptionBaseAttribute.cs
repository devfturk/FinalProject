using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //classlara veya methodlara ekleyebilirsin,Birden fazla ekleyebilirsin,ve inherid edilen bir noktada da bu attirubute çalışsın
    
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; } // öncelik, hangi attiribute önce çalışsın.

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
