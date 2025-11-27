using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Events.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventDetails;

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
