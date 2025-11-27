using ISM.Application.Features.Auth.Dtos;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Features.Auth.Commands.RegisterParticipant;

public record RegisterParticipantCommand(string Email, string Password, string FullName, ParticipantType ParticipantType) : IRequest<ParticipantRegistrationResultDto>;
