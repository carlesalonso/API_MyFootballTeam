using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace API_MyFootballTeam.Areas.API
{
    public class Utilidades
    {
        public static string cadenaConexion = "Server=tcp:sqlmyteam.database.windows.net,1433;Initial Catalog=MyTeam;Persist Security Info=False;User ID=Morgan;Password=Assassin3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //--------------------
        // Funcion para hacer el hash con la contraseña y el id
        //--------------------
        public static string Hasheo(string password, string id)
        {
            byte[] salt2byte;
            salt2byte = UTF8Encoding.UTF8.GetBytes(id + id + id + id + id + id + id + id + id);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt2byte, 10000);
            byte[] hash2 = pbkdf2.GetBytes(32);
            String HashString = Convertir.hash(hash2);
            return HashString;
        }

        class Convertir
        {
            public static string hash(byte[] hash)
            {
                var HashString = Convert.ToBase64String(hash);
                return HashString;
            }

            public static string salt(byte[] salt)
            {
                var SaltString = Convert.ToBase64String(salt);
                return SaltString;
            }
        }
    }
}