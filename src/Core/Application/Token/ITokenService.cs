namespace RewardsPlus.Application.Token;

public interface ITokenService : ITransientService
{
    Task<List<TokenDto>> GetAllAsync();
    Task<bool> ExistsWithIdAsync(string id);
    Task<TokenDto> GetByIdAsync(string id);
    Task<string> GiftAsync(GiftTokensRequest request, CancellationToken cancellationToken);
}