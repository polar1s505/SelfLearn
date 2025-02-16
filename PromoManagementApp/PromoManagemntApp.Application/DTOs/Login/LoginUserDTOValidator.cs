using FluentValidation;

namespace PromoManagemntApp.Application.DTOs.Login
{
    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Username cannot be empty.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.");
        }
    }
}
