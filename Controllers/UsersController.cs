using AnimeList.Models;
using AnimeList.Repository;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 

using System;

namespace AnimeList.Controllers
{   
   
    public class UsersController: BaseController<Users>
    {
        public UsersRepository<AnimeListDbContext> context {get;set;}
        private readonly IConfiguration _config;
        public UsersController(UsersRepository<AnimeListDbContext> _repo, IConfiguration config):base(_repo){
            this.context=_repo;
            this._config = config;
        }

        [HttpPost]
        [AllowAnonymous] 
        [Route("login")]
        public IActionResult Login([FromBody]Users login)
        {
            IActionResult response = Unauthorized();
            Users user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }


        Users AuthenticateUser(Users loginCredentials)
        {
            Users user = context.FindUserLoggin(loginCredentials).FirstOrDefault();
            return user;
        }

        string GenerateJWTToken(Users userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.User),
                new Claim("name", userInfo.Name.ToString()),
                new Claim("role", userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}