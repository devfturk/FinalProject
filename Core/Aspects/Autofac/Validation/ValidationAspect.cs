using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) // bana validatorType'ı ver
        {
            //defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) // gönderilen validatorType, ivalidator değilse hata fırlat.
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil.");
            }

            _validatorType = validatorType; // hata fırlatmazsa sınıf içindeki validatortype'la gönderilen validator type eşitlenir.
        }
        protected override void OnBefore(IInvocation invocation) //method interceptiondaki onBefore'u override ediyoruz.//add update create vs. gibi method olabilir.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // reflection, çalışma anında bir şeyleri çalıştırmamızı sağlar.//Activator ve sonrası şu demek _validatorType'ın bir instance'ını oluştur.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];// sonra _validatorType'ın base type'ını bul, ve onun generic çalışma veri tiplerinden ilkini bul 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);// onun parametrelerini bul. (ilgili methodun)//validator'ün tipine eşit olan parametreleri git bul. 
            foreach (var entity in entities)//her birini tek tek gez
            { 
                ValidationTool.Validate(validator, entity);//validation tool'u kullanarak validate et.
            }
        }
    }
}
