using AutoMapper;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Ideas.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetEventResultsForAdmin;

internal class GetEventResultsForAdminQueryHandler : IRequestHandler<GetEventResultsForAdminQuery, IReadOnlyCollection<IdeaResultDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetEventResultsForAdminQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<IdeaResultDto>> Handle(GetEventResultsForAdminQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        return _mapper.Map<IReadOnlyCollection<IdeaResultDto>>(eventEntity.Ideas);
    }
}
