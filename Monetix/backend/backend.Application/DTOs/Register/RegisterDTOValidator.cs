using FluentValidation;

namespace backend.Application.DTOs.Register
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"(?=.*[0-9])").WithMessage("Password must contain at least one digit.")
                .Matches(@"(?=.*[A-Z])").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"(?=.*[!@#$%^&*(),.?""':;{}|<>])").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
