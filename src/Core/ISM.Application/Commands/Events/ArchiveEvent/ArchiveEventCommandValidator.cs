using FluentValidation;

namespace ISM.Application.Commands.Events.ArchiveEvent;

public class ArchiveEventCommandValidator : AbstractValidator<ArchiveEventCommand>
{
    public ArchiveEventCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
