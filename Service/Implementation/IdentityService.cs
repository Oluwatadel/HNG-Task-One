using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using User_Registartion.Entity;
using User_Registartion.Repository.Interface;
using User_Registartion.Service.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace User_Registartion.Service.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public IdentityService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        

        public async Task<bool> IsValidUser(string email)
        {
            var isValid = await _userRepository.GetUser(email);
            return isValid == null ? false : true;
        }


    }
}
