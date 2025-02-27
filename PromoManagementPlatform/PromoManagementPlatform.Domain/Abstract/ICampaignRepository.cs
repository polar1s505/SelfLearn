using PromoManagementPlatform.Domain.Constants;
using PromoManagementPlatform.Domain.Entities;

namespace PromoManagementPlatform.Domain.Abstract
{
    public interface ICampaignRepository
    {
        Task<IEnumerable<Campaign>> GetCampaignsAsync();
        Task<IEnumerable<Campaign>> GetManagerCampaignsAsync(string managerId);
        Task<Campaign?> GetCampaignByIdAsync(Guid campaignId);
        Task CreateCampaignAsync(Campaign campaign);
        Task UpdateCampaignStatusAsync(Guid campaignId, CampaignStatusEnum status);
        Task DeleteCampaignAsync(Guid campaignId);
    }
}
