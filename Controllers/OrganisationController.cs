using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User_Registartion.Entity;
using User_Registartion.Model;
using User_Registartion.Service.Interface;

namespace User_Registartion.Controllers
{
    [Route("api/organisations")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly IUserService _userService;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrganisationService _organisationService;
        public OrganisationController(IUserService userService, IHttpContextAccessor httpContextAccessor, IOrganisationService organisationService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _organisationService = organisationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserOrganizations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUser(userId);
            var organisations = user.Organisations;
            return Ok(new
            {
                status = "Success",
                message = "Organisation found",
                data = new
                {
                    organisation = organisations!.Select(x => new
                    {
                        x.OrgId,
                        x.Name,
                        x.Description
                    }).ToList()
                }
            });

        }

        [HttpGet("{orgId}")]
        [Authorize]
        public async Task<IActionResult> GetOrganizationById(string orgId)
        {
            var organisation = await _organisationService.GetOrganisationById(orgId);
            if (organisation == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                status = "success",
                message = "Organisation",
                data = new
                {
                    organisation.OrgId,
                    organisation.Name,
                    organisation.Description
                }
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrganization([FromBody] OrganisationRequestModel organisation)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userService.GetUser(userId);
            if (user == null)
            {
                return BadRequest(new
                {
                    status = "Bad Request",
                    message = "Client error",
                    statusCode = 400
                });
            }

            var newOrganisation = new Organisation
            {
                Name = organisation.Name,
                Description = organisation.Description,
            };

            var organisationReturned = await _organisationService.CreateOrganisation(organisation);
            return Created("", new
            {
                status = "success",
                message = "Organisation created successfully",
                data = new
                {
                    organisationReturned.OrgId,
                    organisationReturned.Name,
                    organisationReturned.Description
                }
            });


        }

        [HttpPost("{orgId}/users")]
        [Authorize]
        public async Task<IActionResult> AddUserToOrganization(string orgId, [FromBody] string userId)
        {
            var user = await _userService.GetUser(userId);
            var organisation = await _organisationService.GetOrganisationById(userId);
            return Ok(new
            {
                status = "success",
                message = "User added to organisation successfully"
            });
        }
    }
}