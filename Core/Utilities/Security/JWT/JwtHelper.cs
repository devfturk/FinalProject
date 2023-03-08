using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }//apideki appssettings.json okumaya yarar.
        private TokenOptions _tokenOptions;//atadığımız nesne.
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//tokenoptions section'ını appsettingsden al ve tokenoptions sınıfıyla maple.
        }
        
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//expirationtime'ı set eder.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);//appsettings.json'da belirttiğimiz SecurityKey'i parametre olarak veriyoruz
                                                                                             //ardından SecurityKeyHelper classını kullanarak formatını ayarlayıp değişkene atıyoruz
                                                                                             //yani bunun sonucunda simetrikşifreleme ile byte tipinde bir securitykeyi alıyoruz..
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//hangi anahtarı kullancak hangi algoritmayı kullancak.
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//JWT üretimi.
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }
        //JWT oluşturma methodu.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),//yardımcı method.
                signingCredentials: signingCredentials
            );
            return jwt;
        }
        //kullanıcının bilgilerini ve claim bilgilerini alarak bana bir kullanıcı claim listesi oluştur.
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
