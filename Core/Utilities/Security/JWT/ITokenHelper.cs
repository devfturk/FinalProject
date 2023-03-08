using Core.Entities.Concrete;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);//Token üretecek mekanizmanın imzası.
                                                                                 //şu user için oluştur ve içine operatioClaimsden gelen yetkileri koy.
                                                                                 //kullanıcı giriş yaptığında apiye gönderecek api bu methodu çalıştıracak.
                                                                                 //ilgili kullanıcı için veritabanına gidecek, veritabanından bu kullanıcının claimlerini bulacak
                                                                                 //ardından bir JWT üretip client'a vercek.

    }
}
