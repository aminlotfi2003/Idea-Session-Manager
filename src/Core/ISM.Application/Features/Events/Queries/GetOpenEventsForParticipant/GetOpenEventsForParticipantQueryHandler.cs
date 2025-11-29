using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Extensions;
using ISM.Application.Features.Events.Dtos;
using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetOpenEventsForParticipant;

internal class GetOpenEventsForParticipantQueryHandler : IRequestHandler<GetOpenEventsForParticipantQuery, PaginatedResult<InnovationEventListItemDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetOpenEventsForParticipantQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<InnovationEventListItemDto>> Handle(GetOpenEventsForParticipantQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.Pagination ?? new PaginationParams();
        var statuses = new[] { EventStatus.Published, EventStatus.IdeaSubmissionOpen };
        var query = _uow.InnovationEvents.QueryByStatus(statuses)
            .Where(e => e.AllowedParticipantGroup == request.AllowedGroup);

        return await query
            .ProjectTo<InnovationEventListItemDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(pagination, cancellationToken);
    }
}
