using ISM.Application.DTOs.Auth;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Commands.Auth.RegisterParticipant;

public record RegisterParticipantCommand(string Email, string Password, string FullName, ParticipantType ParticipantType) : IRequest<ParticipantRegistrationResultDto>;
