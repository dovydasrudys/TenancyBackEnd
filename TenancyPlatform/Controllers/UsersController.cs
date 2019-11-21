using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using TenancyPlatform.Contexts;
using TenancyPlatform.Models;
using TenancyPlatform.Services;

namespace TenancyPlatform.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TenancyContext _context;
        private IUserService _userService;
        private IConfiguration _configuration;

        public UsersController(TenancyContext context, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            AuthenticatedUser authenticatedUser = _userService.Authenticate(userParam.UserName, userParam.Password);

            if (authenticatedUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(authenticatedUser);
        }

        [AllowAnonymous]
        [HttpGet("facebook")]
        public async Task<ActionResult> AuthenticateFacebookUser()
        {
            string authenticationCode = HttpContext.Request.Query.First(q => q.Key == "code").Value;

            HttpClient httpClient = new HttpClient();
            string clientId = _configuration["Authentication:Facebook:AppId"];
            string clientSecret = _configuration["Authentication:Facebook:AppSecret"];
            string redirectUri = "https://localhost:44318/api/users/facebook";

            var response = await httpClient.GetAsync("https://graph.facebook.com/v5.0/oauth/access_token" + $"?client_id={clientId}&redirect_uri={redirectUri}&client_secret={clientSecret}&code={authenticationCode}");

            string facebookToken = "";
            long id;
            string first_name = "";
            string last_name = "";
            string email = "";

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var myJObject = JObject.Parse(responseJson);
                facebookToken = myJObject.SelectToken("access_token").Value<string>();

                response = await httpClient.GetAsync("https://graph.facebook.com/me" + $"?fields=first_name,last_name,email&access_token={facebookToken}");
                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                    myJObject = JObject.Parse(responseJson);

                    id = myJObject.SelectToken("id").Value<long>();
                    first_name = myJObject.SelectToken("first_name").Value<string>();
                    last_name = myJObject.SelectToken("last_name").Value<string>();
                    email = myJObject.SelectToken("email").Value<string>();
                }
                else
                {
                    throw new Exception("Error receiving information about user from facebook");
                }
            }
            else
            {
                throw new Exception("Error receiving FB access token");
            }

            return Ok(new {id, first_name, last_name, email, facebookToken});
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
