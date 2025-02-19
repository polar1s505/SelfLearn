using MediatR;
using PromoManagementPlatform.Application.DTOs.Result;
using PromoManagementPlatform.Domain.Constants;

namespace PromoManagementPlatform.Application.Campaign.Commands.Manager.Create
{
    public record CreateCampaignCommand(
        string ManagerId, 
        string Name, 
        string Description, 
        DateTime StartDate, 
        DateTime EndDate, 
        CampaignTypeEnum Type) 
        : IRequest<Result<string>>;
}
