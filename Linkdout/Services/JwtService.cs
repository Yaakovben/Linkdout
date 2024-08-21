using Linkdout.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Linkdout.Services
{
    public class JwtService
    {
        private IConfiguration config;
        public JwtService(IConfiguration _config)
        {
            config = _config;
        }

        public string genJWToken(UserModel user)
        {
            //configלקבל את המפתח ואת הזמן שהטוקן יהיה בתוקף מה
            string? key = config.GetValue("JWT:key", string.Empty);
            int? exp = config.GetValue("JWT:exp", 3);

            // לייצור מפתח בייטים
            SymmetricSecurityKey secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials crd = new SigningCredentials(secKey,SecurityAlgorithms.HmacSha256);

            // יצירת טוקן
            Claim[] claims = new[]
            {
                new Claim("id",user.Id.ToString()),
                new Claim("userName",user.UserName),
            };
            //מקבל אובייקט טוקן מכל הדאטה
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes((double)exp),
                claims: claims,
                signingCredentials: crd
                );
            //מקבל את הסטרינג טוקן
            string tkn = new JwtSecurityTokenHandler().WriteToken(token);
            //מחזיר את הסטרינג טוקן
            return tkn;

        }

    }
}
