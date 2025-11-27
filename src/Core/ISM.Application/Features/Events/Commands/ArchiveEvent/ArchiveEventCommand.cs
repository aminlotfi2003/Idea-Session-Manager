using MediatR;

namespace ISM.Application.Features.Events.Commands.ArchiveEvent;

public record ArchiveEventCommand(Guid EventId) : IRequest;
