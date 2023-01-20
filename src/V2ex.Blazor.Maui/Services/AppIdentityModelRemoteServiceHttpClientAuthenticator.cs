using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.Authentication;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.IdentityModel;

namespace V2ex.Services;

[Volo.Abp.DependencyInjection.Dependency(ReplaceServices = true)]
public class AppIdentityModelRemoteServiceHttpClientAuthenticator :
    IdentityModelRemoteServiceHttpClientAuthenticator, ITransientDependency
{
    public AppIdentityModelRemoteServiceHttpClientAuthenticator(
        IdentityTokenManager identityTokenManager,
        IIdentityModelAuthenticationService identityModelAuthenticationService)
        : base(identityModelAuthenticationService)
    {
        IdentityTokenManager = identityTokenManager;
    }

    private IdentityTokenManager IdentityTokenManager { get; }

    public override async Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
    {
        //if (context.RemoteService.GetUseCurrentAccessToken() != false)
        //{
        //    var accessToken = await GetAccessTokenFromAccessTokenProviderOrNullAsync();
        //    if (accessToken != null)
        //    {
        //        context.Request.SetBearerToken(accessToken);
        //        return;
        //    }
        //}

        await base.Authenticate(context);
    }

    protected virtual async Task<string?> GetAccessTokenFromAccessTokenProviderOrNullAsync()
    {
        return await this.IdentityTokenManager.GetAccessTokenAsync();
    }
}
