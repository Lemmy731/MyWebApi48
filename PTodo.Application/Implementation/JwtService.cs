using ETodo.Domain.Dto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MTodo.Infrastruture;
using PTodo.Application.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace PTodo.Application.Implementation
{
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
         _configuration = configuration;
        }
        public Tokens Authenticate(AppTodoItem user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "Admin")

                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Tokens { Token = tokenHandler.WriteToken(token)};
     
    }
    }
}
