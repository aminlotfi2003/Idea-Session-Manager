using FluentValidation;

namespace ISM.Application.Features.Events.Commands.ArchiveEvent;

public class ArchiveEventCommandValidator : AbstractValidator<ArchiveEventCommand>
{
    public ArchiveEventCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
