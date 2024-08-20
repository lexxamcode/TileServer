namespace Gdd.Domain.Services;

/// <summary>
/// Интерфейс для работы с базой данных
/// заблокированных адресов
/// </summary>
public interface IBlacklistRepository
{
    /// <summary>
    /// Добавить новый адрес
    /// </summary>
    /// <param name="ip">Ip-адрес для добавления</param>
    public Task AddIp(string ip);

    /// <summary>
    /// Метод проверки существования адреса
    /// в базе данных
    /// </summary>
    /// <param name="ip">Ip-адрес</param>
    /// <returns>Есть ли адрес в базе данных</returns>
    public Task<bool> IsInDatabase(string ip);
}
