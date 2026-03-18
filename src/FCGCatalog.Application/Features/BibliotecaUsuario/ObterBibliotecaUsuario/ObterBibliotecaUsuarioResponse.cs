namespace FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;

public sealed record ObterBibliotecaUsuarioResponse(
	Guid Id,
	Guid UsuarioId,
	Guid JogoId,
	string JogoTitulo,
	DateTime DataCompra);
