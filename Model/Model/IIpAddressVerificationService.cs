namespace Domain.Model;

public interface IIpAddressVerificationService
{
    public Task<bool> IsPotencialIntruderAsync(Request request);
    public Task<string> IndexRequestAsync(Request request);
}
