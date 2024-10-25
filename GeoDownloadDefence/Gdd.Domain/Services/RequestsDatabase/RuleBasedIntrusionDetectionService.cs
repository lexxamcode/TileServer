using Gdd.Domain.Model.Requests;

namespace Gdd.Domain.Services.RequestsDatabase;

public class RuleBasedIntrusionDetectionService : IIntrusionDetectionService
{
    public Task<bool> IsPotentialIntruderAsync(Request request)
    {
        throw new NotImplementedException();
    }
}
