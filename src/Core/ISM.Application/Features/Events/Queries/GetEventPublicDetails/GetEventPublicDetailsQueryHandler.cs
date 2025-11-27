using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Events.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventPublicDetails;

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
        var entity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        return _mapper.Map<InnovationEventDetailDto>(entity);
    }
}
