using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.Application.DTOs.Campaign
{
    public record CampaignDTO(
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        CampaignTypeEnum Type,
        CampaignStatusEnum Status);
}
