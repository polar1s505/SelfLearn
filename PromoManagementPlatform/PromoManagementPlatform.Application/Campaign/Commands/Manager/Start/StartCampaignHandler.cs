using MediatR;
using PromoManagementPlatform.Application.DTOs.Result;
using PromoManagementPlatform.Domain.Abstract;
using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.Application.Campaign.Commands.Manager.Start
{
    public class StartCampaignHandler : IRequestHandler<StartCampaignCommand, Result<string>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public StartCampaignHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<Result<string>> Handle(StartCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetCampaignByIdAsync(request.CampaignId);

            if (campaign == null)
            {
                return Result<string>.Failure("Campaugn not found");
            }
            await _campaignRepository.UpdateCampaignStatusAsync(request.CampaignId, CampaignStatusEnum.Active);

            return Result<string>.Success($"{campaign.Name} was started successfully");
        }
    }
}
