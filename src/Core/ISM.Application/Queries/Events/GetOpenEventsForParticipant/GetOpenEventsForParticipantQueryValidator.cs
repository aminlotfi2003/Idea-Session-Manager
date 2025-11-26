using FluentValidation;

namespace ISM.Application.Queries.Events.GetOpenEventsForParticipant;

public class GetOpenEventsForParticipantQueryValidator : AbstractValidator<GetOpenEventsForParticipantQuery>
{
    public GetOpenEventsForParticipantQueryValidator()
    {
        RuleFor(x => x.AllowedGroup).IsInEnum();
    }
}
