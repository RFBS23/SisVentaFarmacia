using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; //referencias
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class N_Recursos
    {
        //encriptacion de texto SHA256
        public static string ConvertitSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            //usamos la referencia de "system.Security.Cryptography"
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
