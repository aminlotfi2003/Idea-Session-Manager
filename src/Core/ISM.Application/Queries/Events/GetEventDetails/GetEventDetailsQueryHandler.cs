using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Events;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Queries.Events.GetEventDetails;

internal class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, InnovationEventDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetEventDetailsQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<InnovationEventDetailDto> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        return _mapper.Map<InnovationEventDetailDto>(entity);
    }
}
