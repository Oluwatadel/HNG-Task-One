using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_Registartion.Entity;
using User_Registartion.Model;
using User_Registartion.Service.Interface;

namespace User_Registartion.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public Authentication(IUserService userService, IIdentityService identityService, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _userService = userService;
            _identityService = identityService;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegRequestModel request)
        {
           var newUser = await _userService.Register(request);
            if (newUser == null)
            {
                return BadRequest(new
                {
                    status = "Bad request",
                    message = "Registration unsuccessful",
                    statusCode = 400
                });
            }
            var token = await GenerateToken(newUser);

            return Created("", new
            {
                status = "success",
                message = "Registration Successfull",
                data = new
                {
                    accessToken = token,
                    user = new
                    {
                        newUser.UserId,
                        newUser.Email,
                        newUser.FirstName,
                        newUser.LastName,
                        newUser.Phone
                    }
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel request)
        {
            var login = await _identityService.IsValidUser(request.Email);
            if(!login)
            {
                return BadRequest(new
                {
                    status = "Bad request",
                    message = "Authentication failed",
                    statusCode = 401
                });
            }
            var newUser = await _userService.GetUser(request.Email);
            if (BCrypt.Net.BCrypt.Verify(request.Password, newUser.Password))
            {
                var token = await GenerateToken(newUser);
                return Ok(new
                {
                    status = "Successfull",
                    message = "Login successfull",
                    data = new
                    {
                        accessToken = token,
                        user = new
                        {
                            newUser.UserId,
                            newUser.LastName,
                            newUser.FirstName,
                            newUser.Email,
                            newUser.Phone
                        }
                    }
                });
            }
            return Unauthorized();


        }

        private async Task<string> GenerateToken(User user)
        {
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.UserId)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //if(claimIdentity.IsAuthenticated == true)
            //{
            //    await HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity), new AuthenticationProperties());
            //}

            //Generate Token
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
