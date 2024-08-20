using Gdd.Domain.Model;

namespace Gdd.Domain.Services;

public interface ITileManager
{
    public Task<byte[]> GetTile(TileCoordinates tileCoordinates);
}
