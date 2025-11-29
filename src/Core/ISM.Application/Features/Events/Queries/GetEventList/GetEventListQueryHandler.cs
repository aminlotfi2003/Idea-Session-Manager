using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Extensions;
using ISM.Application.Features.Events.Dtos;
using ISM.SharedKernel.Common.Pagination;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ISM.Application.Features.Events.Queries.GetEventList;

internal class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, PaginatedResult<InnovationEventListItemDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetEventListQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<InnovationEventListItemDto>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.Pagination ?? new PaginationParams();

        var query = _uow.InnovationEvents.Query();

        if (request.Status.HasValue)
            query = _uow.InnovationEvents.QueryByStatus(new[] { request.Status.Value });

        return await query
           .ProjectTo<InnovationEventListItemDto>(_mapper.ConfigurationProvider)
           .ToPaginatedResultAsync(pagination, cancellationToken);
    }
}
