using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Extensions;
using ISM.Application.Features.Ideas.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using ISM.SharedKernel.Common.Pagination;
using MediatR;

namespace ISM.Application.Features.Ideas.Queries.GetEventResultsForAdmin;

internal class GetEventResultsForAdminQueryHandler : IRequestHandler<GetEventResultsForAdminQuery, PaginatedResult<IdeaResultDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetEventResultsForAdminQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<IdeaResultDto>> Handle(GetEventResultsForAdminQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.Pagination ?? new PaginationParams();
        _ = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");

        var query = _uow.Ideas.Query()
            .Where(i => i.InnovationEventId == request.EventId);

        return await query
            .ProjectTo<IdeaResultDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(pagination, cancellationToken);
    }
}
