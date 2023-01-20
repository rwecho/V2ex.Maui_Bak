using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using V2ex;
using Volo.Abp.Threading;
using Volo.Abp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddApplication<AppModule>(options =>
{
	options.Services.ReplaceConfiguration(builder.Configuration);
});

var app = builder.Build();
var appServices = app.Services.GetRequiredService<IAbpApplicationWithExternalServiceProvider>();

AsyncHelper.RunSync(() => appServices.InitializeAsync(app.Services));

await app.RunAsync();
