using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Ideas;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Queries.Ideas.GetMyIdeaDetails;

internal class GetMyIdeaDetailsQueryHandler : IRequestHandler<GetMyIdeaDetailsQuery, IdeaDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetMyIdeaDetailsQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IdeaDetailDto> Handle(GetMyIdeaDetailsQuery request, CancellationToken cancellationToken)
    {
        var participant = await _uow.ParticipantProfiles.GetByUserIdAsync(request.CurrentUserId, cancellationToken) ?? throw new NotFoundException("Participant not found");
        var idea = await _uow.Ideas.GetWithDetailsAsync(request.IdeaId, cancellationToken) ?? throw new NotFoundException("Idea not found");
        if (idea.ConfidentialLink.ParticipantProfileId != participant.Id)
            throw new ForbiddenException();

        return _mapper.Map<IdeaDetailDto>(idea);
    }
}
