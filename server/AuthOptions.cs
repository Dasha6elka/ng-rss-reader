using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace server
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        public const string KEY = "yQqJE6uMiYmLifRUgxnly3I5JGX3p2YP";

        public const int LIFETIME = 1 * 60 * 24;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
