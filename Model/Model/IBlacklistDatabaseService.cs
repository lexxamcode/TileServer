namespace Domain.Model;

/// <summary>
/// Интерфейс для взаимодействия с базой данных заблокированных IP-адресов
/// </summary>
public interface IBlacklistDatabaseService
{
    /// <summary>
    /// Заблокировать IP-адрес
    /// </summary>
    /// <param name="ipAddress">IP-адрес</param>
    public void BlockIpAddress(string ipAddress);

    /// <summary>
    /// Проверить, заблокирован ли IP-адрес
    /// </summary>
    /// <param name="ipAddress">IP-адрес</param>
    /// <returns>Заблокирован адрес или нет</returns>
    public bool IsBlocked(string ipAddress);
}
