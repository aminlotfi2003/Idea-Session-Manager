using ISM.Application.Features.Auth.Dtos;
using MediatR;

namespace ISM.Application.Features.Auth.Commands.CreateJudgeUser;

public record CreateJudgeUserCommand(string FullName, string Email, string TemporaryPassword) : IRequest<JudgeCreatedDto>;
