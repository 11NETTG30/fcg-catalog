namespace FCGCatalog.Application.Features.BibliotecaUsuario.ObterBibliotecaUsuario;

public sealed record ObterBibliotecaUsuarioResponse(
	Guid JogoId,
	string JogoTitulo,
	DateTime DataCompra);
