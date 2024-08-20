using Gdd.Domain.Model;

namespace Gdd.Domain.Services;

public interface ITileRepository
{
    public Task<byte[]> GetTile(TileCoordinates tileCoordinates);
}
