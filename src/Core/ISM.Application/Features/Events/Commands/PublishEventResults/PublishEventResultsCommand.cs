using MediatR;

namespace ISM.Application.Features.Events.Commands.PublishEventResults;

public record PublishEventResultsCommand(Guid EventId) : IRequest;
