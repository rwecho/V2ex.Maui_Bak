using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.Authentication;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.IdentityModel;

namespace V2ex.Services;

public class IdentityTokenManager: ITransientDependency
{
    internal Task AcquireAccessTokenAsync(string userName, string password)
    {
        return Task.CompletedTask;
    }

    internal Task DismissAccessTokenAsync()
    {
        return Task.CompletedTask;
    }

    internal Task<string> GetAccessTokenAsync()
    {
        return Task.FromResult("");
    }
}
