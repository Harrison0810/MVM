using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MVM.Domain.Helpers
{
    public class Helper
    {
        public static string EncodePassword(string pass, string salt) //encrypt password    
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            //return Convert.ToBase64String(inArray);    
            return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }

        private static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }

        public static int GetUserIdToken(HttpContext context)
        {
            int userId = 0;
            string token = GetToken(context);
            JwtSecurityToken jwtToken = new JwtSecurityToken(token);

            if (jwtToken != null)
            {
                IEnumerable<Claim> claims = jwtToken.Claims;
                Claim claim = claims.FirstOrDefault(x => string.Equals(x.Type, "Id"));

                if (!(claim is null))
                {
                    userId = Convert.ToInt32(claim.Value);
                }
            }

            return userId;
        }

        private static string GetToken(HttpContext context)
        {
            string token = string.Empty;
            KeyValuePair<string, StringValues> oKeyValuePair = context.Request.Headers.Where(x => string.Equals(x.Key, "Authorization", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (oKeyValuePair.Key != null && !string.IsNullOrEmpty(oKeyValuePair.Value))
            {
                token = Regex.Replace(oKeyValuePair.Value, "Bearer ", "");
            }
            return token;
        }
    }
}
