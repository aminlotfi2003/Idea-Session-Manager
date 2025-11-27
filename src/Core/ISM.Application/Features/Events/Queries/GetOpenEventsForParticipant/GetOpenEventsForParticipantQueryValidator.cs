using FluentValidation;

namespace ISM.Application.Features.Events.Queries.GetOpenEventsForParticipant;

public class GetOpenEventsForParticipantQueryValidator : AbstractValidator<GetOpenEventsForParticipantQuery>
{
    public GetOpenEventsForParticipantQueryValidator()
    {
        RuleFor(x => x.AllowedGroup).IsInEnum();
    }
}
