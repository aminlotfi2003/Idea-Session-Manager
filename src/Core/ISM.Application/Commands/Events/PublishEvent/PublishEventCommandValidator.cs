using FluentValidation;

namespace ISM.Application.Commands.Events.PublishEvent;

public class PublishEventCommandValidator : AbstractValidator<PublishEventCommand>
{
    public PublishEventCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
