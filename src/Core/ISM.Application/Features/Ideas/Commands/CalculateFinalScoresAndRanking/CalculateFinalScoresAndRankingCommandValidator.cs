using FluentValidation;

namespace ISM.Application.Features.Ideas.Commands.CalculateFinalScoresAndRanking;

public class CalculateFinalScoresAndRankingCommandValidator : AbstractValidator<CalculateFinalScoresAndRankingCommand>
{
    public CalculateFinalScoresAndRankingCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
