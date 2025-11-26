using MediatR;

namespace ISM.Application.Commands.Events.ArchiveEvent;

public record ArchiveEventCommand(Guid EventId) : IRequest;
