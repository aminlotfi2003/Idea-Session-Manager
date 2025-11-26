using FluentValidation;

namespace ISM.Application.Commands.Ideas.CalculateFinalScoresAndRanking;

public class CalculateFinalScoresAndRankingCommandValidator : AbstractValidator<CalculateFinalScoresAndRankingCommand>
{
    public CalculateFinalScoresAndRankingCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
