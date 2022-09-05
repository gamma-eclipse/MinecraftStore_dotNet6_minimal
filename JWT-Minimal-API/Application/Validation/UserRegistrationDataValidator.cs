using FluentValidation;
using JWT_Minimal_API.Application.Dtos;

namespace JWT_Minimal_API.Application.Validation
{
    public class UserRegistrationDataValidator : AbstractValidator<UserRegistrationData>
    {
        public UserRegistrationDataValidator()
        {
            RuleFor(u => u.Username).NotEmpty().Length(3, 20);
            RuleFor(u => u.Password).NotEmpty().Length(3, 20);
            RuleFor(u => u.EmailAddress).EmailAddress();
        }
    }
}
