using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromoManagementPlatform.Application.Campaign.Commands.Admin.AddRole;
using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.API.Controllers
{
    [Route("api/admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRolesConstants.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("add-user-role")]
        public async Task<IActionResult> AddUserRoleAsync([FromBody] AddRoleCommand addRoleCommand)
        {
            var result = await _mediator.Send(addRoleCommand);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
    }
}
