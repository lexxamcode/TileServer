using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TileProxyServer.Controllers;

[ApiController]
[Route("tiles/{z}/{x}/{y}")]
public class TileProxyController(IOptionsSnapshot<ProxyConfiguration> proxyConfiguration) : ControllerBase
{
    private readonly ProxyConfiguration _proxyConfiguration = proxyConfiguration.Value;

    [HttpGet]
    public async Task<IActionResult> GetTile(int z, int x, int y)
    {
        var tileUrl = _proxyConfiguration.TileServerUrl.Replace("{z}", z.ToString())
            .Replace("{x}", x.ToString())
            .Replace("{y}", y.ToString());

        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(tileUrl);

            var stream = await response.Content.ReadAsByteArrayAsync();
            if (response.Content.Headers.ContentType?.MediaType is null)
                return StatusCode(500, $"Error: {tileUrl}");

            return new FileContentResult(stream, "image/png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {tileUrl} {ex.Message}");
        }
    }
}
