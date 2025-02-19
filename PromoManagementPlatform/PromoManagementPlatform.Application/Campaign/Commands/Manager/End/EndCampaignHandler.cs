using MediatR;
using PromoManagementPlatform.Application.Campaign.Commands.Manager.Start;
using PromoManagementPlatform.Application.DTOs.Result;
using PromoManagementPlatform.Domain.Abstract;
using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.Application.Campaign.Commands.Manager.End
{
    public class EndCampaignHandler : IRequestHandler<EndCampaignCommand, Result<string>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public EndCampaignHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<Result<string>> Handle(EndCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetCampaignByIdAsync(request.CampaignId);

            if (campaign == null)
            {
                return Result<string>.Failure("Campaign not found");
            }

            await _campaignRepository.UpdateCampaignStatusAsync(request.CampaignId, CampaignStatusEnum.Ended);

            return Result<string>.Success($"{campaign.Name} was ended successfully");
        }
    }
}
