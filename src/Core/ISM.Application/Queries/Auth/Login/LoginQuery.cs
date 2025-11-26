using ISM.Application.DTOs.Auth;
using MediatR;

namespace ISM.Application.Queries.Auth.Login;

public record LoginQuery(string Email, string Password) : IRequest<LoginResponseDto>;
