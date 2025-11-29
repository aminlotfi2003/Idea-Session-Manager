using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Extensions;
using ISM.Application.Features.Evaluations.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Evaluations.Queries.GetAssignedIdeasForJudge;

internal class GetAssignedIdeasForJudgeQueryHandler : IRequestHandler<GetAssignedIdeasForJudgeQuery, PaginatedResult<JudgeAssignedIdeaDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAssignedIdeasForJudgeQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<JudgeAssignedIdeaDto>> Handle(GetAssignedIdeasForJudgeQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.Pagination ?? new PaginationParams();
        _ = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");

        var query = _uow.Ideas.Query()
            .Where(i => i.InnovationEventId == request.EventId && i.Evaluations.Any(ev => ev.JudgeId == request.JudgeId));

        return await query
            .ProjectTo<JudgeAssignedIdeaDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(pagination, cancellationToken);
    }
}
