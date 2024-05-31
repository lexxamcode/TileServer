using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TileProxyServer.Controllers;

[ApiController]
[Route("tiles/{z}/{x}/{y}")]
public class TileProxyController(IOptionsSnapshot<ProxyConfiguration> proxyConfiguration,
                                 ILogger<TileProxyController> logger,
                                 IIpAddressVerificationService ipAddressVerificationService) : ControllerBase
{
    private readonly ProxyConfiguration _proxyConfiguration = proxyConfiguration.Value;
    private readonly IIpAddressVerificationService _ipAddressVerificationService = ipAddressVerificationService;
    private readonly ILogger _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetTile(int z, int x, int y)
    {
        var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        _logger.LogInformation("GetTile z={z} x={x} y={y} - {clientIp}", z, x, y, clientIp);

        var currentRequest = new Request()
        {
            ClientIp = clientIp,
            RequestTime = DateTime.UtcNow,
            Z = z,
            X = x,
            Y = y
        };

        await _ipAddressVerificationService.IndexRequestAsync(currentRequest);
        var isPotentialIntruder = await _ipAddressVerificationService.IsPotencialIntruderAsync(currentRequest);

        var tileUrl = _proxyConfiguration.TileServerUrl;

        if (isPotentialIntruder)
        {
            _logger.LogInformation("Potential Intruder Detected!");
            tileUrl = tileUrl.Replace("{z}/{x}/{y}", "broken_tile");
        }
        else
        {
            tileUrl = tileUrl.Replace("{z}", z.ToString())
            .Replace("{x}", x.ToString())
            .Replace("{y}", y.ToString());
        }

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
