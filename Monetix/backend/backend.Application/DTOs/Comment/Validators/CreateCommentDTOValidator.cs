using FluentValidation;

namespace backend.Application.DTOs.Comment.Validators
{
    public class CreateCommentDTOValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentDTOValidator()
        {
            RuleFor(c => c.Title)
                .MinimumLength(5).WithMessage("Title must be 5 characters")
                .MaximumLength(280).WithMessage("Title cannot be over 280 characters");

            RuleFor(c => c.Content)
                .MinimumLength(5).WithMessage("Title must be 5 characters")
                .MaximumLength(280).WithMessage("Title cannot be over 280 characters");
        }
    }
}
