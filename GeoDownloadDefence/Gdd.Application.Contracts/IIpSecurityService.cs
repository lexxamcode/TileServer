using Gdd.Domain.Model;
using Gdd.Domain.Shared.Enums;

namespace Gdd.Domain.Services;

public interface IIpSecurityService
{
    public Task<IpStatus> ValidateRequestAndReturnIpStatus(string ipAddress, TileCoordinates tileCoordinates);
}
