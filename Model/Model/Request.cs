namespace Domain.Model;

/// <summary>
/// Класс запроса
/// </summary>
public class Request
{
    /// <summary>
    /// IP-адрес клиента, отправившего запрос
    /// </summary>
    public string ClientIp { get; set; } = string.Empty;

    /// <summary>
    /// Время запроса
    /// </summary>
    public DateTime RequestTime { get; set; }

    /// <summary>
    /// Z координата тайла
    /// </summary>
    public int Z { get; set; }

    /// <summary>
    /// X координата тайла
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Y координата тайла
    /// </summary>
    public int Y { get; set; }
}
