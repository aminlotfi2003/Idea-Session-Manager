using MediatR;

namespace ISM.Application.Features.Ideas.Commands.CalculateFinalScoresAndRanking;

public record CalculateFinalScoresAndRankingCommand(Guid EventId) : IRequest;
