using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper//1.15 1.30// 1.40-1.45
    {
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)//ona verdiğimiz bir passwordun hash'ini ve saltını oluşturan yapıyı oluşturcak. out keyword?
        {   //disposible pattern.
            using (var hmac = new System.Security.Cryptography.HMACSHA512())//kriptografide kullandığımız class var hmac. newlediğimiz instance .net içinde algoritma seçmemize yarıyor.
            {
                //out keywordü ile verdiğimiz için kullandığımızda döndürmüş olacağız.
                passwordSalt = hmac.Key;//salt olarak bu değeri vereceğiz.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//byte array olarak istediği için passwordün byte değerini alıyoruz. ve hash methoduna veriyoruz.
            }
        }
        //bu methodda kullanıcının gönderdiği passwordü yine aynı algoritmayla hashleseydin karşına passwordHash çıkar mıydı?
        public static bool VerifyPasswordHash(string password, byte[] passwordHash,byte[] passwordSalt)//doğrulama yapacağızımız için out keyword kullanmıyoruz.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))//aynı şekil hashleyeceğimiz için saltı veriyoruz.
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//kullanıcı login yaparken gönderdiği passwordü aynı key ile tekrar hashledik.
                for (int i = 0; i < computedHash.Length; i++)//oluşan hash ve veritabanındaki hashi karşılaştırdık.
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}