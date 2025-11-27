using ISM.Application.Features.Auth.Dtos;
using MediatR;

namespace ISM.Application.Features.Auth.Commands.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : IRequest<ChangePasswordResultDto>;
