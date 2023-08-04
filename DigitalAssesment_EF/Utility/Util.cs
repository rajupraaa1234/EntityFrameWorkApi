using System;
using System.IdentityModel.Tokens.Jwt;
using DigitalAssesment_EF.Models.Login;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DigitalAssesment_EF.Utility
{
	public class Util
	{
		public static string getUserByJwtToken(string bearer)
		{
            try
            {
                string token = bearer.Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                string userId = tokenS.Claims.ToArray()[0].ToString();
                userId = userId.Split(" ")[1];
                return userId;
            }
            catch(Exception ex)
            {
                return null;
            }
          
        }

        public static string GenerateToken(LoginClass model , IConfiguration _config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,model.username),
                new Claim("password" , model.password)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

