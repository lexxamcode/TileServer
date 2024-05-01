using Domain.Model;
using Domain.Services;

namespace Repository.Services;

public class RedisRequestsCacheRepository : IRequestsCacheRepository
{
    public Task AddNewRequest(Request request)
    {
        throw new NotImplementedException();
    }

    public List<Request> GetRequestsByIp(string ipAddress)
    {
        throw new NotImplementedException();
    }

    public bool IsRequestValid(Request request)
    {
        throw new NotImplementedException();
    }
}
