namespace Gdd.Domain.Model.Requests;

/// <summary>
/// Класс запроса
/// </summary>
public class Request
{
    /// <summary>
    /// Метод запроса
    /// </summary>
    public HttpMethod? Method { get; set; }

    /// <summary>
    /// IP-адрес клиента, отправившего запрос
    /// </summary>
    public string ClientIp { get; set; } = string.Empty;

    /// <summary>
    /// Время запроса
    /// </summary>
    public DateTime RequestTime { get; set; }

    /// <summary>
    /// Координаты запрашиваемого тайла
    /// </summary>
    public TileCoordinates Coordinates { get; set; } = new();
}
