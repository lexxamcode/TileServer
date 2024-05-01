using Domain.Model;

namespace Domain.Services;

public interface IRequestsCacheRepository
{
    public List<Request> GetRequestsByIp(string ipAddress);
    public bool IsRequestValid(Request request);
    public Task AddNewRequest(Request request);
}
