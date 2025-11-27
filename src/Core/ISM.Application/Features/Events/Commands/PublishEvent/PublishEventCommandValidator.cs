using FluentValidation;

namespace ISM.Application.Features.Events.Commands.PublishEvent;

public class PublishEventCommandValidator : AbstractValidator<PublishEventCommand>
{
    public PublishEventCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
