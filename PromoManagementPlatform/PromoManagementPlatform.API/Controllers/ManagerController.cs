using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromoManagementPlatform.Application.Campaign.Commands.Manager.Create;
using PromoManagementPlatform.Application.Campaign.Commands.Manager.End;
using PromoManagementPlatform.Application.Campaign.Commands.Manager.Start;
using PromoManagementPlatform.Application.Campaign.Queries;
using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.API.Controllers
{
    [Route("api/manager")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRolesConstants.Manager)]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("campaign")]
        public async Task<IActionResult> CreateCampaignAsync([FromBody] CreateCampaignCommand createCampaignCommand)
        {
            var result = await _mediator.Send(createCampaignCommand);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        [HttpPatch("campaign/status")]
        public async Task<IActionResult> UpdateCampaignStatusAsync([FromQuery] Guid id, [FromQuery] CampaignStatusEnum status)
        {
            if(status.Equals(CampaignStatusEnum.Active))
            {
                var result = await _mediator.Send(new StartCampaignCommand(CampaignId: id));
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok();
            }

            if (status.Equals(CampaignStatusEnum.Ended))
            {
                var result = await _mediator.Send(new EndCampaignCommand(CampaignId: id));
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }
                return Ok();
            }

            return BadRequest("Invalid status");
        }

        [HttpGet("campaigns/{managerId}")]
        public async Task<IActionResult> GetManagerCampaignsAsync(string managerId)
        {
            var result = await _mediator.Send(new GetManagerCampaignsQuery(managerId));
            return Ok(result);
        }
    }
}
