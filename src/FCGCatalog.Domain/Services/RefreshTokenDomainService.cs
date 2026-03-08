using FCGCatalog.Domain.Entities;

namespace FCGCatalog.Domain.Services;

public interface IRefreshTokenDomainService
{
    Task RevogarCadeiaDescendente(RefreshToken refreshToken, Guid refreshTokenId);
}
