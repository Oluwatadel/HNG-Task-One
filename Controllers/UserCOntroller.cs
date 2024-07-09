using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Registartion.Entity;
using User_Registartion.Service.Interface;

namespace User_Registartion.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserCOntroller : ControllerBase
    {
        private readonly IUserService _userService;

        public UserCOntroller(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return BadRequest(new
                {
                    status = "Unsuccessful",
                    message = "User not found",
                    StatusCode = 400
                });
            }
            return Ok(new
            {
                status = "success",
                message = "Organisation",
                data = new
                {
                    user.LastName,
                    user.FirstName,
                    user.Email,
                    user.Phone
                }
            });
        }
    }

}
