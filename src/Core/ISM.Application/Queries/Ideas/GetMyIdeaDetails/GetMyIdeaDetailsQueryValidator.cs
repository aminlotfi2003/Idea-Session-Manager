using FluentValidation;

namespace ISM.Application.Queries.Ideas.GetMyIdeaDetails;

public class GetMyIdeaDetailsQueryValidator : AbstractValidator<GetMyIdeaDetailsQuery>
{
    public GetMyIdeaDetailsQueryValidator()
    {
        RuleFor(x => x.IdeaId).NotEmpty();
        RuleFor(x => x.CurrentUserId).NotEmpty();
    }
}
