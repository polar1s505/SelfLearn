using MediatR;
using PromoManagementPlatform.Application.DTOs.Campaign;
using PromoManagementPlatform.Application.DTOs.Result;

namespace PromoManagementPlatform.Application.Campaign.Queries
{
    public record GetManagerCampaignsQuery(string ManagerId) : IRequest<Result<List<CampaignDTO>>>;
}
