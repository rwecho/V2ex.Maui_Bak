using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace V2ex;

public class V2exSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
        ConfigureBlazorise(context.Services);
        ConfigureFlxor(context.Services);
        context.Services.AddAuthorizationCore();
    }

    private void ConfigureBlazorise(IServiceCollection services)
    {
        services.AddBlazorise(options =>
        {
            options.Debounce = true;
            options.DebounceInterval = 500;
        })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
    }

    private static void ConfigureFlxor(IServiceCollection services)
    {
        services.AddFluxor(o => o.ScanAssemblies(typeof(V2exSharedModule).Assembly));
    }
}