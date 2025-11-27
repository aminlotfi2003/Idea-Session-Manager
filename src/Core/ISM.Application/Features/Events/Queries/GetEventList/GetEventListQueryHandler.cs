using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventList;

internal class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, IReadOnlyCollection<InnovationEventListItemDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetEventListQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<InnovationEventListItemDto>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Entities.InnovationEvent> events;
        if (request.Status.HasValue)
            events = await _uow.InnovationEvents.GetByStatusAsync(new[] { request.Status.Value }, cancellationToken);
        else
            events = await _uow.InnovationEvents.ListAsync(cancellationToken);

        return _mapper.Map<IReadOnlyCollection<InnovationEventListItemDto>>(events);
    }
}
