using FluentValidation;
using JWT_Minimal_API.Application.Dtos;

namespace JWT_Minimal_API.Application.Validation
{
    public class UserCredentialsValidator: AbstractValidator<UserCredentialsData>
    {
        public UserCredentialsValidator()
        {
            RuleFor(u => u.Username).NotEmpty().Length(3, 20);
            RuleFor(u=>u.Password).NotEmpty().Length(3, 20);
        }
    }
}
