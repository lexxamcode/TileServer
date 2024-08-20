using Gdd.Domain.Model;
using Gdd.Domain.Model.Requests;
using Gdd.Domain.Services;
using Gdd.Domain.Shared.Enums;

namespace Gdd.Application.Services;

public class IpSecurityService(
    IBlacklistManager blacklistManager,
    IIntrusionDetectionService intrusionDetectionService) : IIpSecurityService
{
    private readonly IBlacklistManager _blacklistManager = blacklistManager;
    private readonly IIntrusionDetectionService _intrusionDetectionService = intrusionDetectionService;

    public async Task<IpStatus> ValidateRequestAndReturnIpStatus(string clientIp, TileCoordinates tileCoordinates)
    {
        var isBlocked = await _blacklistManager.IsBlockedAsync(clientIp);
        if (isBlocked)
            return IpStatus.Blocked;

        var currentRequest = new Request
        {
            ClientIp = clientIp,
            RequestTime = DateTime.UtcNow,
            Coordinates = tileCoordinates
        };

        var isPotentialIntruder = await _intrusionDetectionService.IsPotentialIntruderAsync(currentRequest);

        if (isPotentialIntruder)
        {
            await _blacklistManager.BlockIpAddressAsync(currentRequest.ClientIp);
            return IpStatus.PotentialIntruder;
        }

        return IpStatus.Safe;
    }
}
