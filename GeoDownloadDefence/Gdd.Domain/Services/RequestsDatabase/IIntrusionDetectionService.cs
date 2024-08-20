using Gdd.Domain.Model.Requests;

namespace Gdd.Domain.Services;

public interface IIntrusionDetectionService
{
    public Task<bool> IsPotentialIntruderAsync(Request request);
}
