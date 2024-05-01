using Model;

namespace RequestsHandler;

public interface IRequestHandler
{
    public bool ValidateRequest(RequestInfo requestInfo);
    public bool AddRequest(RequestInfo requestInfo);
}
