using MediatR;
using PromoManagementPlatform.Application.DTOs.Result;
using PromoManagementPlatform.Domain.Abstract;
using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.Application.Campaign.Commands.Manager.Create
{
    public class CreateCampaignHandler : IRequestHandler<CreateCampaignCommand, Result<string>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public CreateCampaignHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<Result<string>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = new Domain.Entities.Campaign
            {
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Type = request.Type,
                Status = CampaignStatusEnum.PendingStart,
                UserId = request.ManagerId
            };

            if(campaign.StartDate < DateTime.UtcNow)
            {
                return Result<string>.Failure("Start date must be in the future");
            }

            await _campaignRepository.CreateCampaignAsync(campaign);

            return Result<string>.Success(campaign.Id.ToString());
        }
    }
}
