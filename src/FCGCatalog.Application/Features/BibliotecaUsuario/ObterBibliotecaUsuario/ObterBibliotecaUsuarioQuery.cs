using MediatR;

namespace FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;

public sealed record ObterBibliotecaUsuarioQuery(Guid UsuarioId) : IRequest<ObterBibliotecaUsuarioResponse>;
