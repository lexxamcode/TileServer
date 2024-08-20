namespace Gdd.Domain.Services;

/// <summary>
/// Интерфейс для работы с заблокированными IP-адресами
/// </summary>
public interface IBlacklistManager
{
    /// <summary>
    /// Заблокировать IP-адрес
    /// </summary>
    /// <param name="ipAddress">IP-адрес</param>
    public Task BlockIpAddressAsync(string ipAddress);

    /// <summary>
    /// Проверить, заблокирован ли IP-адрес
    /// </summary>
    /// <param name="ipAddress">IP-адрес</param>
    /// <returns>Заблокирован адрес или нет</returns>
    public Task<bool> IsBlockedAsync(string ipAddress);
}
