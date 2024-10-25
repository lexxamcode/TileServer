using Gdd.Domain.Model.Requests;
using Gdd.Domain.Services;

namespace Gdd.Domain.Model.Rules;

public class IntegerParametersSequenceRule(IRequestsRepository requestsRepository) : Rule
{
    public Dictionary<string, int> Parameters { get; set; } = [];
    public SequenceType SequenceType { get; set; }
    public GetListRequestFilter SequenceFilter { get; set; } = new();

    public override async Task<bool> CheckIfRequestMatchesRuleAsync(Request request)
    {
        await requestsRepository.AddRequest(request);

        var requests = await requestsRepository.GetAllRequestsByIp(request.ClientIp, SequenceFilter);

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

    private bool CheckIfSequenceEqualToSortedByTime(IEnumerable<Request> sortedByTimeSequence, IEnumerable<Request> actualSequene)
    {
        switch (SequenceType)
        {
            case SequenceType.Ascending:
                {
                    break;
                }
        }
    }
}
