using Gdd.Domain.Model;
using Gdd.Domain.Services;
using Gdd.Repository.Utils;
using Microsoft.Extensions.Options;

namespace Domain.Model.Services;

public class RemoteTileRepository(
    IOptionsMonitor<TileServerConfiguration> remoteTileServerConfiguration)
    : ITileRepository
{
    private readonly TileServerConfiguration _remoteTileServerConfiguration = remoteTileServerConfiguration.CurrentValue;

    public async Task<byte[]> GetTile(TileCoordinates tileCoordinates)
    {
        using var httpClient = new HttpClient();

        var tileUrl = TileUrlBuilder.Build(_remoteTileServerConfiguration.Url, tileCoordinates);
        var response = await httpClient.GetAsync(tileUrl);

        var stream = await response.Content.ReadAsByteArrayAsync();
        if (response.Content.Headers.ContentType?.MediaType is null)
            throw new InvalidDataException("MediaType is unknown");

        return [.. stream];
    }
}
