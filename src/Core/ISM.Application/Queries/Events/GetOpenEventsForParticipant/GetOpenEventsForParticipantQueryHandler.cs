using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Events;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Queries.Events.GetOpenEventsForParticipant;

internal class GetOpenEventsForParticipantQueryHandler : IRequestHandler<GetOpenEventsForParticipantQuery, IReadOnlyCollection<InnovationEventListItemDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetOpenEventsForParticipantQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<InnovationEventListItemDto>> Handle(GetOpenEventsForParticipantQuery request, CancellationToken cancellationToken)
    {
        var statuses = new[] { EventStatus.Published, EventStatus.IdeaSubmissionOpen };
        var events = await _uow.InnovationEvents.GetByStatusAsync(statuses, cancellationToken);
        events = events.Where(e => e.AllowedParticipantGroup == request.AllowedGroup).ToList();
        return _mapper.Map<IReadOnlyCollection<InnovationEventListItemDto>>(events);
    }
}
