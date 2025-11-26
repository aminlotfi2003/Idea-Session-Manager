using MediatR;

namespace ISM.Application.Commands.Ideas.CalculateFinalScoresAndRanking;

public record CalculateFinalScoresAndRankingCommand(Guid EventId) : IRequest;
