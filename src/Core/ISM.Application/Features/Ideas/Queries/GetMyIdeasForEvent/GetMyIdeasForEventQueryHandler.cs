using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Extensions;
using ISM.Application.Features.Ideas.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetMyIdeasForEvent;

internal class GetMyIdeasForEventQueryHandler : IRequestHandler<GetMyIdeasForEventQuery, PaginatedResult<IdeaListItemDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetMyIdeasForEventQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<IdeaListItemDto>> Handle(GetMyIdeasForEventQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.Pagination ?? new PaginationParams();
        var participant = await _uow.ParticipantProfiles.GetByUserIdAsync(request.CurrentUserId, cancellationToken) ?? throw new NotFoundException("Participant profile not found");
        var query = _uow.Ideas
            .QueryByEventId(request.EventId)
            .Where(i => i.ConfidentialLink.ParticipantProfileId == participant.Id);

        return await query
            .ProjectTo<IdeaListItemDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(pagination, cancellationToken);
    }
}
