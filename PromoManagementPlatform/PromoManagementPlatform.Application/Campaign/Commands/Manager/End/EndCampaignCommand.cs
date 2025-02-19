using MediatR;
using PromoManagementPlatform.Application.DTOs.Result;

namespace PromoManagementPlatform.Application.Campaign.Commands.Manager.End
{
    public record EndCampaignCommand(Guid CampaignId) : IRequest<Result<string>>;
}
