
namespace Gdd.Domain.Services;

public class BlacklistManager(IBlacklistRepository blacklistRepository) : IBlacklistManager
{
    private readonly IBlacklistRepository _blacklistRepository = blacklistRepository;

    public async Task BlockIpAddressAsync(string ipAddress)
    {
        await _blacklistRepository.AddIp(ipAddress);
    }

    public async Task<bool> IsBlockedAsync(string ipAddress)
    {
        return await _blacklistRepository.IsInDatabase(ipAddress);
    }
}
