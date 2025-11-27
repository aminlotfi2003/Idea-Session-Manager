using FluentValidation;

namespace ISM.Application.Features.Ideas.Commands.SubmitIdea;

public class SubmitIdeaCommandValidator : AbstractValidator<SubmitIdeaCommand>
{
    public SubmitIdeaCommandValidator()
    {
        RuleFor(x => x.Idea.EventId).NotEmpty();
        RuleFor(x => x.Idea.Title).NotEmpty();
        RuleFor(x => x.Idea.Description).NotEmpty();
        RuleFor(x => x.Idea.ParticipantEmail).NotEmpty().EmailAddress();
    }
}
