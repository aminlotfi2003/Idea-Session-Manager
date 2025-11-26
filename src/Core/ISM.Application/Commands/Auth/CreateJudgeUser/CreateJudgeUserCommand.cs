using ISM.Application.DTOs.Auth;
using MediatR;

namespace ISM.Application.Commands.Auth.CreateJudgeUser;

public record CreateJudgeUserCommand(string FullName, string Email, string TemporaryPassword) : IRequest<JudgeCreatedDto>;
