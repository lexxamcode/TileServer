namespace Domain.Services;

public interface IClientIpsRepository
{
    public Task BanIp(string ip);
    public Task UnbanIp(string ip);
    public bool IsBanned(string ip);
}
