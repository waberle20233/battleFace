using battleFaceDataAccess;
using battleFaceDataAccess.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace battleFaceJWTAuth.Security
{
    public class SecurityManager
    {

        public static User GetUser(ApplicationDBContext db, string userName)
        {
            User user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();

            return user;
        }

        public static bool CheckPassword(ApplicationDBContext db, User user, string password)
        {
            User_Key user_Key = db.User_Keys.Where(u => u.UserId == user.Id).First();

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, user_Key.Password, password);

            if(result == PasswordVerificationResult.Failed)
            {
                return false;
            }

            return true;
        }

        public static string HashPassword(User user, string password)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }

        public static string BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var key = Encoding.ASCII.GetBytes("thisisoursupersecretkeyiwouldneverkeepinsourcecodebutwouldlikelybeinavaultorenvironmentvariable");

            
            var JWToken = new JwtSecurityToken(
                issuer: "http://localhost:45092/",
                audience: "http://localhost:45092/",
                claims: claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                //Using HS256 Algorithm to encrypt Token
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            return token;
        }
    }
}
