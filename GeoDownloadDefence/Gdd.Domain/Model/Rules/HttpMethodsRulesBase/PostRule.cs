using Gdd.Domain.Model.Requests;
using Gdd.Domain.Model.Rules.Base;

namespace Gdd.Domain.Model.Rules.HttpMethodsRules;

public abstract class PostRule : Rule
{
    public override Task<bool> CheckIfRequestMatchesRuleAsync(Request request)
    {
        if (request.Method == HttpMethod.Post && AccessPolicy == AccessPolicy.Deny)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }
}
