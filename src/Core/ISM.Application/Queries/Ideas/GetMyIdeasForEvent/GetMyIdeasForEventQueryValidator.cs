using FluentValidation;

namespace ISM.Application.Queries.Ideas.GetMyIdeasForEvent;

public class GetMyIdeasForEventQueryValidator : AbstractValidator<GetMyIdeasForEventQuery>
{
    public GetMyIdeasForEventQueryValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(x => x.CurrentUserId).NotEmpty();
    }
}
