using Gdd.Domain.Model;
using Gdd.Domain.Model.Requests;

namespace Gdd.Domain.Services;

public interface IRequestsRepository
{
    public Task<string> AddRequest(Request request);

    public Task<IEnumerable<Request>> GetAllRequestsByIp(string clientIp, GetListRequestFilter? filter);
}
