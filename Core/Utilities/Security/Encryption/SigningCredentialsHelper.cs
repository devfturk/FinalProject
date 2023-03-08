using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //web.apinin kullanabileceği jsonwebtokenlarının oluşturulabilmesi için credentiallar vericek.
        //sen bir hashing işlemi yapacaksın, anahtar olarak bu securitykey kullan , şifreleme olarakda güvenlik algoritmalarında hmacsha512 kullan.
        //asp.net'e hashingi anlatıyoruz aslında.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) //securitykey formatında vericez
        {
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);//imzalama nesnesini döndürecek.
        }
    }
}
