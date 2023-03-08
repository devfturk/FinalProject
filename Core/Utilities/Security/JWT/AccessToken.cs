using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }//anahtar değeri.
        public DateTime Expiration { get; set; }//bitiş zamanı.

    }
}
