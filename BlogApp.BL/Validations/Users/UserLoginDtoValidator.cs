using BlogApp.BL.DTOs.Users;
using FluentValidation;

namespace BlogApp.BL.Validations.Users;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty()
            .WithMessage("Username cannot be empty.")
            .Length(4, 32)
            .WithMessage("Username must be between 4 and 32 characters.");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters.");
    }
}
