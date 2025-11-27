using FluentValidation;

namespace ISM.Application.Features.Auth.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(q => q.Email).NotEmpty().EmailAddress();
        RuleFor(q => q.Password).NotEmpty();
    }
}
