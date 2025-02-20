using PromoManagementPlatform.Domain.Abstract;
using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.Application.BackgroundServices
{
    public class UpdateCampaignStatusService
    {
        private readonly ICampaignRepository _campaignRepository;

        public UpdateCampaignStatusService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task UpdateCampaignStatusAsync()
        {
            var campaigns = await _campaignRepository.GetCampaignsAsync();
            foreach (var campaign in campaigns)
            {
                if (campaign.StartDate <= DateTime.UtcNow && campaign.Status == CampaignStatusEnum.PendingStart)
                {
                    await _campaignRepository.UpdateCampaignStatusAsync(campaign.Id, CampaignStatusEnum.Active);
                }
                else if (campaign.EndDate <= DateTime.UtcNow && campaign.Status == CampaignStatusEnum.Active)
                {
                    await _campaignRepository.UpdateCampaignStatusAsync(campaign.Id, CampaignStatusEnum.Ended);
                }
            }
        }
    }
}
