namespace TileProxyServer;

/// <summary>
/// Конфигурация прокси-сервера
/// </summary>
public class ProxyConfiguration
{
    /// <summary>
    /// Ip-адрес основного сервера
    /// </summary>
    public string TileServerUrl { get; set; } = string.Empty;

    /// <summary>
    /// Ip-адрес ElasticSearch
    /// </summary>
    public string ElasticSearchUrl { get; set; } = string.Empty;

    /// <summary>
    /// Индекс ElasticSearch
    /// </summary>
    public string ElasticSearchIndex { get; set; } = string.Empty;

    /// <summary>
    /// Строка подключения к Sqlite
    /// </summary>
    public string SqliteConnectionString { get; set; } = string.Empty;
}
