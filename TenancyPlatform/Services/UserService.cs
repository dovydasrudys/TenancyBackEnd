using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TenancyPlatform.Contexts;
using TenancyPlatform.Helpers;
using TenancyPlatform.Models;

namespace TenancyPlatform.Services
{
    public interface IUserService
    {
        AuthenticatedUser Authenticate(string username, string password);
        string GenerateJwtToken(string id);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly TenancyContext _context;

        public UserService(IOptions<AppSettings> appSettings, TenancyContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticatedUser Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token

            AuthenticatedUser authenticatedUser = new AuthenticatedUser()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Token = GenerateJwtToken(user.Id.ToString())
            };

            return authenticatedUser;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _context.Users.ToList().Select(x => {
                x.Password = null;
                return x;
            });
        }

        public string GenerateJwtToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
