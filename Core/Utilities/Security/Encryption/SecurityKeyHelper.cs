using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{//şifreleme olan sistemlerde her şeyi byte arrayi olarak vermemiz gerekiyor.
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)//dönüş tipi appsettingsde oluşturduğumuz token.securitykeyi temsil ediyor.
                                                                       //parametrede de onun içeriğini veriyoruz.
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));//simetrik ve asimetrik key olarak ayrılıyor biz simetrik verdik.
        }
    }
}
