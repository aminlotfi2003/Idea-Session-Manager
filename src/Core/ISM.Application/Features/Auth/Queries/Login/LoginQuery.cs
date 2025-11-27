using ISM.Application.Features.Auth.Dtos;
using MediatR;

namespace ISM.Application.Features.Auth.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<LoginResponseDto>;
