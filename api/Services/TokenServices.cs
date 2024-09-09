using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenServices(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Signinkey"]));
        }


        public string createToken(ApppUser user)
        {
            var claims = new List<Claim> {

                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var now = DateTime.UtcNow;
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = now.AddHours(3),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            
            var token = tokenhandler.CreateToken(tokenDescription);

            return tokenhandler.WriteToken(token);
        }
    }
}