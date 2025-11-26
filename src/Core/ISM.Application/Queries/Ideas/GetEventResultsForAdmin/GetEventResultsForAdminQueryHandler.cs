using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Ideas;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Queries.Ideas.GetEventResultsForAdmin;

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
