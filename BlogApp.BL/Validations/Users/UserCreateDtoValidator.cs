using BlogApp.BL.DTOs.Users;
using FluentValidation;

namespace BlogApp.BL.Validations.Users;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty()
            .WithMessage("Username cannot be empty.")
            .Length(4, 32)
            .WithMessage("Username must be between 4 and 32 characters.");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .MaximumLength(128)
            .WithMessage("Email can be a maximum of 128 characters.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(user => user.Name)
           .NotEmpty()
           .WithMessage("Name cannot be empty.")
           .MaximumLength(32)
           .WithMessage("Name can be maximum 32 characters.");

        RuleFor(user => user.Surname)
           .NotEmpty()
           .WithMessage("Surname cannot be empty.")
           .MaximumLength(32)
           .WithMessage("Surname can be maximum 32 characters.");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}
