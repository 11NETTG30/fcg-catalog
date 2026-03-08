namespace FCGCatalog.Application.Identidade.Security;

public interface ITokenSettings
{
    short ExpiracaoAccessTokenMinutos { get; }
    byte ExpiracaoRefreshTokenDias { get; }
    bool HabilitarSegurancaDeReusoRefreshToken { get; }
}