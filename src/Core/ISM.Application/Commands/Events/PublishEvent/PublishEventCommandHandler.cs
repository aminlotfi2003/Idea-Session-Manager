using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Events;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Commands.Events.PublishEvent;

internal class PublishEventCommandHandler : IRequestHandler<PublishEventCommand, InnovationEventDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PublishEventCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<InnovationEventDetailDto> Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        entity.Publish();
        await _uow.SaveChangesAsync(cancellationToken);
        return _mapper.Map<InnovationEventDetailDto>(entity);
    }
}
