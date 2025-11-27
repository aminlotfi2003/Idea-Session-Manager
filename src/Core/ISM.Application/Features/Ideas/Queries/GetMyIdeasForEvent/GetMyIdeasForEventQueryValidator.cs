using FluentValidation;

namespace ISM.Application.Features.Ideas.Queries.GetMyIdeasForEvent;

public class GetMyIdeasForEventQueryValidator : AbstractValidator<GetMyIdeasForEventQuery>
{
    public GetMyIdeasForEventQueryValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(x => x.CurrentUserId).NotEmpty();
    }
}
