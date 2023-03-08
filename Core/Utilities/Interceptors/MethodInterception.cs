using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //içleri boş methodlar.Validation'da onBefore için kullancaz sadece diğerlerini boş bırakcaz
        //methodun başında çalışcak ve bitcek.
        //burası bizim methodların içerisi aslında.//invacation method
        //yani biz buraya ne kural verirsek çalışmadan önce bu kurallardan geçicek.
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)//invocation benim çalıştırmak istediğim methodum
        {
            var isSuccess = true;
            OnBefore(invocation);// add methodu için -> on before dediğimde methodun başında çalışır.
            //genellikle on before ve onexception kullanılır.
            //bu da bize try catch yapısını her yerde kullanmamızı sağlayan bir ortam oluşturur.
            try
            {
                invocation.Proceed();//method çalışırken
            }
            catch (Exception e)
            {
                isSuccess = false; 
                OnException(invocation, e);//hata aldığında
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);//başarılı olduğunda
                }
            }
            OnAfter(invocation);//method'dan sonra
        }
    }
}
