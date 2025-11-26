using FluentValidation;

namespace ISM.Application.Commands.Events.UpdateInnovationEvent;

public class UpdateInnovationEventCommandValidator : AbstractValidator<UpdateInnovationEventCommand>
{
    public UpdateInnovationEventCommandValidator()
    {
        RuleFor(x => x.Event.Id).NotEmpty();
        RuleFor(x => x.Event.Title).NotEmpty();
    }
}
