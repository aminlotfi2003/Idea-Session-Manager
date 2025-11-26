using MediatR;

namespace ISM.Application.Commands.Auth.Logout;

public record LogoutCommand(string RefreshToken) : IRequest;
