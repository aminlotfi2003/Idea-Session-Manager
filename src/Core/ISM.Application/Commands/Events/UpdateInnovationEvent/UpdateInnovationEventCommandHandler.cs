using AutoMapper;
using ISM.Application.Abstractions.Repositories;
using ISM.Application.DTOs.Events;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Commands.Events.UpdateInnovationEvent;

internal class UpdateInnovationEventCommandHandler : IRequestHandler<UpdateInnovationEventCommand, InnovationEventDetailDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UpdateInnovationEventCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<InnovationEventDetailDto> Handle(UpdateInnovationEventCommand request, CancellationToken cancellationToken)
    {
        var existing = await _uow.InnovationEvents.GetByIdAsync(request.Event.Id, cancellationToken) ?? throw new KeyNotFoundException("Event not found");
        if (existing.Status != EventStatus.Draft)
        {
            throw new InvalidOperationException("Only draft events can be updated");
        }

        existing = _mapper.Map(request.Event, existing);
        _uow.InnovationEvents.Update(existing);
        await _uow.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InnovationEventDetailDto>(existing);
    }
}
