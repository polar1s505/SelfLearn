using MediatR;
using Microsoft.AspNetCore.Identity;
using PromoManagementPlatform.Application.DTOs.Result;

namespace PromoManagementPlatform.Application.Campaign.Commands.Admin.AddRole
{
    public record AddRoleCommand(string UserId, string Role) : IRequest<Result<string>>;
}
