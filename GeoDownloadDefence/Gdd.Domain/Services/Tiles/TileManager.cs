using Gdd.Domain.Model;

namespace Gdd.Domain.Services.Tiles;

public class TileManager(ITileRepository tileRepository) : ITileManager
{
    private readonly ITileRepository _tileRepository = tileRepository;

    public async Task<byte[]> GetTile(TileCoordinates tileCoordinates)
    {
        return await _tileRepository.GetTile(tileCoordinates);
    }
}
