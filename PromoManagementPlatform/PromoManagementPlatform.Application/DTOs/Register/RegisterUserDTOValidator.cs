using FluentValidation;

namespace PromoManagementPlatform.Application.DTOs.Register
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Matches("^[a-zA-Z]+$").WithMessage("Name must contain only letters")
                .MaximumLength(50);

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Lastname is required")
                .Matches("^[a-zA-Z]+$").WithMessage("Lastname must contain only letters")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"(?=.*[0-9])").WithMessage("Password must contain at least one digit.")
                .Matches(@"(?=.*[A-Z])").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"(?=.*[!@#$%^&*(),.?""':;{}|<>])").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
