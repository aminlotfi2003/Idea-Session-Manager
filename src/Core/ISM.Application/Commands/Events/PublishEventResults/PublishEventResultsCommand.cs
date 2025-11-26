using MediatR;

namespace ISM.Application.Commands.Events.PublishEventResults;

public record PublishEventResultsCommand(Guid EventId) : IRequest;
