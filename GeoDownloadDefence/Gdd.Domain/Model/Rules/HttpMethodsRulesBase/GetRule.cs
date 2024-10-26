using Gdd.Domain.Model.Requests;
using Gdd.Domain.Model.Rules.Base;

namespace Gdd.Domain.Model.Rules.HttpMethodsRules;

public abstract class GetRule : Rule
{
    public override async Task<bool> CheckIfRequestMatchesRuleAsync(Request request)
    {
        if (request.Method == HttpMethod.Get && AccessPolicy == AccessPolicy.Deny)
            return await Task.FromResult(false);

        return await Task.FromResult(true);
    }
}
