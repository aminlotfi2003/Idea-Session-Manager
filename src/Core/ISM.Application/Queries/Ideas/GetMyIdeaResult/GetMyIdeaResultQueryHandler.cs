using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Ideas;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Queries.Ideas.GetMyIdeaResult;

internal class GetMyIdeaResultQueryHandler : IRequestHandler<GetMyIdeaResultQuery, IdeaResultDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetMyIdeaResultQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IdeaResultDto> Handle(GetMyIdeaResultQuery request, CancellationToken cancellationToken)
    {
        var participant = await _uow.ParticipantProfiles.GetByUserIdAsync(request.CurrentUserId, cancellationToken) ?? throw new NotFoundException("Participant not found");
        var idea = await _uow.Ideas.GetWithDetailsAsync(request.IdeaId, cancellationToken) ?? throw new NotFoundException("Idea not found");
        if (idea.ConfidentialLink.ParticipantProfileId != participant.Id)
            throw new ForbiddenException();

        return _mapper.Map<IdeaResultDto>(idea);
    }
}
