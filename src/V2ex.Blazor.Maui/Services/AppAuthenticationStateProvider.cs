using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace V2ex.Services;

[Volo.Abp.DependencyInjection.Dependency(ReplaceServices = true)]
[ExposeServices(typeof(AuthenticationStateProvider), typeof(AppAuthenticationStateProvider))]
public class AppAuthenticationStateProvider : AuthenticationStateProvider, IScopedDependency
{
    public AppAuthenticationStateProvider(IdentityTokenManager identityTokenManager)
    {
        IdentityTokenManager = identityTokenManager;
    }

    private IdentityTokenManager IdentityTokenManager { get; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        try
        {
            var accessToken = await this.IdentityTokenManager.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(accessToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken);

                if (jsonToken is not JwtSecurityToken jwtToken)
                {
                    throw new InvalidOperationException("Can not parse access token");
                }
                identity = new ClaimsIdentity(jwtToken.Claims, "Server authentication");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task LoginAsync(string userName, string password)
    {
        Check.NotNullOrEmpty(userName, nameof(userName));
        Check.NotNullOrEmpty(password, nameof(password));
        await this.IdentityTokenManager.AcquireAccessTokenAsync(userName, password);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await this.IdentityTokenManager.DismissAccessTokenAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
