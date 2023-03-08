using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Core.Extensions;

namespace Business.BusinessAspects.Autofac
{
    //JWT için yazdığımız aspect.
    public class SecuredOperation : MethodInterception
    {
        //JWT için
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;//JWT göndererek istek yapıyoruz. Herkese bir tane thread verir.(HTTPcontext)

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();//servisi injectlemem için.

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
