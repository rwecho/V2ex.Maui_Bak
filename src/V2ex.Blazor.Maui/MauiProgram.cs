using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Threading;

namespace V2ex;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        SetupSerilog();
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureContainer(new AbpAutofacServiceProviderFactory(new Autofac.ContainerBuilder()));


        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Logging.AddSerilog(dispose: true);

        ConfigureConfiguration(builder);
        builder.Services.AddApplication<AppModule>(options =>
        {
            options.Services.ReplaceConfiguration(builder.Configuration);
        });

        var app = builder.Build();

        var appServices = app.Services.GetRequiredService<IAbpApplicationWithExternalServiceProvider>();

        AsyncHelper.RunSync(() => appServices.InitializeAsync(app.Services));

        return app;
    }

    private static void ConfigureConfiguration(MauiAppBuilder builder)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;
        // todo can not load emmbeded file.
        //builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);
    }

    private static void SetupSerilog()
    {
        var flushInterval = new TimeSpan(0, 0, 1);

        var file = Path.Combine(AppContext.BaseDirectory,
            "Logs",
            "V2ex.log");

        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File(file, flushToDiskInterval: flushInterval,
            encoding: System.Text.Encoding.UTF8,
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 22)
        .CreateLogger();
    }
}