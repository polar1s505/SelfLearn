using AutoMapper;
using MediatR;
using PromoManagementPlatform.Application.Abstract;
using PromoManagementPlatform.Application.DTOs.Campaign;
using PromoManagementPlatform.Application.DTOs.Result;
using PromoManagementPlatform.Domain.Abstract;
using PromoManagementPlatform.Domain.Entities;

namespace PromoManagementPlatform.Application.Campaign.Queries
{
    public class GetManagerCampaignsHandler : IRequestHandler<GetManagerCampaignsQuery, Result<List<CampaignDTO>>>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUserManagerWrapper<ApplicationUser> _userManagerWrapper;
        private readonly IMapper _mapper;

        public GetManagerCampaignsHandler(ICampaignRepository campaignRepository, IUserManagerWrapper<ApplicationUser> userManagerWrapper, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _userManagerWrapper = userManagerWrapper;
            _mapper = mapper;
        }

        public async Task<Result<List<CampaignDTO>>> Handle(GetManagerCampaignsQuery request, CancellationToken cancellationToken)
        {
            var manager = await _userManagerWrapper.FindIdAsync(request.ManagerId);

            if (manager == null)
            {
                return Result<List<CampaignDTO>>.Failure("Manager not found");
            }

            var campaigns = (await _campaignRepository.GetManagerCampaignsAsync(request.ManagerId)).ToList();

            return Result<List<CampaignDTO>>.Success(_mapper.Map<List<CampaignDTO>>(campaigns));
        }
    }
}
