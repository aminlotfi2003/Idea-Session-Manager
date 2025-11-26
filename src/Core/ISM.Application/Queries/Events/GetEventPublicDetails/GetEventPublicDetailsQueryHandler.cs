using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.GetEventPublicDetails;

internal class GetEventPublicDetailsQueryHandler : IRequestHandler<GetEventPublicDetailsQuery, InnovationEventDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetEventPublicDetailsQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<InnovationEventDetailDto> Handle(GetEventPublicDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new KeyNotFoundException("Event not found");
        return _mapper.Map<InnovationEventDetailDto>(entity);
    }
}
