using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Alugamer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Alugamer.Auth
{
    public class TokenService
    {
        public static string GenerateToken(UserInfo userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authKey = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["authKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userInfo)),
                    new Claim(ClaimTypes.Role, userInfo.Admin ? "ADMIN" : "USER")
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(authKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static UserInfo GetUserInfo(HttpContext context)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = context.Request.Cookies.Where(o => o.Key.Equals("auth")).Select(o => o.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(token))
                return null;

            var decodedToken = tokenHandler.ReadJwtToken(token);

            Claim userInfoClaim = decodedToken.Claims.Where(o => o.Type == ClaimTypes.UserData).FirstOrDefault();

            if (userInfoClaim == null) return null;

            return JsonConvert.DeserializeObject<UserInfo>(userInfoClaim.Value);
        }

        public static void RevokeToken(HttpContext context)
        {
            context.Response.Cookies.Delete("auth");
        }

    }
}
