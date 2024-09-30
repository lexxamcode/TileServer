using Gdd.Domain.Model;
using Gdd.Domain.Model.Requests;

namespace Gdd.Domain.Services;

public class IntrusionDetectionService(IRequestsRepository requestsRepository) : IIntrusionDetectionService
{
    private readonly IRequestsRepository _requestsRepository = requestsRepository;

    public async Task<bool> IsPotentialIntruderAsync(Request request)
    {
        await _requestsRepository.AddRequest(request);

        var filter = new GetListRequestFilter
        {
            Take = 20,
            Size = 10000,
            From = 0
        };

        var requests = await _requestsRepository.GetAllRequestsByIp(request.ClientIp, filter);

        if (requests.Count() < 100)
            return false;

        var requestsSortedByCoordinates = requests
            .OrderBy(r => r.Coordinates.Z)
            .ThenBy(r => r.Coordinates.X)
            .ThenBy(r => r.Coordinates.Y);

        var requestsSortedByTime = requests.OrderBy(r => r.RequestTime);

        var areRequestListsEqual =
            requestsSortedByCoordinates.SequenceEqual(requestsSortedByTime) ||
            requestsSortedByCoordinates.Reverse().SequenceEqual(requestsSortedByTime);

        return areRequestListsEqual;
    }
}
