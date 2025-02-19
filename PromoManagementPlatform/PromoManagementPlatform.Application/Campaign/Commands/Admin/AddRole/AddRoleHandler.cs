using MediatR;
using PromoManagementPlatform.Application.Abstract;
using PromoManagementPlatform.Application.DTOs.Result;
using PromoManagementPlatform.Domain.Entities;

namespace PromoManagementPlatform.Application.Campaign.Commands.Admin.AddRole
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, Result<string>>
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManagerWrapper;

        public AddRoleHandler(IUserManagerWrapper<ApplicationUser> userManagerWrapper)
        {
            _userManagerWrapper = userManagerWrapper;
        }

        public async Task<Result<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManagerWrapper.FindIdAsync(request.UserId);

            if(user == null)
            {
                return Result<string>.Failure("User not found");
            }

            var result = await _userManagerWrapper.AddToRoleAsync(user, request.Role);

            if (!result.Succeeded)
            {
                return Result<string>.Failure("Failed to add role");
            }

            return Result<string>.Success("Role added successfully");
        }
    }
}
