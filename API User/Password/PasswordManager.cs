using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship_Shooting_Game.API.Password
{
    public class PasswordManager
    {
        public static string EncryptPassword(string password)
        {
            // Encriptar la contraseña
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verificar si la contraseña coincide con su versión encriptada
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
