using Gdd.Domain.Model;
using Gdd.Domain.Services;
using Gdd.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Gdd.HttpApi.Controllers;

[ApiController]
[Route("tiles/{z}/{x}/{y}")]
public class TileProxyController(
    ILogger<TileProxyController> logger,
    IIpSecurityService ipSecurityService,
    ITileManager tileManager) : ControllerBase
{
    private readonly IIpSecurityService _ipSecurityService = ipSecurityService;
    private readonly ITileManager _tileService = tileManager;
    private readonly ILogger _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetTile(int z, int x, int y)
    {
        var clientIp = HttpContext.Connection.RemoteIpAddress;
        _logger.LogInformation("GetTile ({z}, {x}, {y}) | {clientIp})", z, x, y, clientIp);

        if (clientIp == null)
        {
            _logger.LogError("Request has an invalid IpAddress");
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var tileCoordinates = new TileCoordinates
        {
            Z = z,
            X = x,
            Y = y,
        };

        var ipStatus = await _ipSecurityService.ValidateRequestAndReturnIpStatus(clientIp.ToString(), tileCoordinates);

        if (ipStatus is IpStatus.Blocked or IpStatus.PotentialIntruder)
        {
            if (ipStatus is IpStatus.PotentialIntruder)
                _logger.LogWarning("Potential intruder detected from {}", clientIp);

            return StatusCode(StatusCodes.Status403Forbidden);
        }

        try
        {
            var tileBytes = await _tileService.GetTile(tileCoordinates);
            return File(tileBytes, "image/png");
        }
        catch (InvalidDataException ex)
        {
            _logger.LogError("Could not get {tileCoordinates} : {Message}", tileCoordinates, ex.Message);
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
