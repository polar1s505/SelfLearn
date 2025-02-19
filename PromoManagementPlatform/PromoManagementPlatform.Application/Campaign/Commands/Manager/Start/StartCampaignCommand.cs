using MediatR;
using PromoManagementPlatform.Application.DTOs.Result;

namespace PromoManagementPlatform.Application.Campaign.Commands.Manager.Start
{
    public record StartCampaignCommand(Guid CampaignId) : IRequest<Result<string>>;
}
