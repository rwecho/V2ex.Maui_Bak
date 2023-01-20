using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace V2ex;

[DependsOn(typeof(AbpAutofacModule),
	typeof(V2exSharedModule),
	typeof(AbpHttpClientIdentityModelModule))]
public class AppModule : AbpModule
{
}