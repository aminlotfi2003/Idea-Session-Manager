using ISM.Application.DTOs.Auth;
using MediatR;

namespace ISM.Application.Commands.Auth.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : IRequest<ChangePasswordResultDto>;
