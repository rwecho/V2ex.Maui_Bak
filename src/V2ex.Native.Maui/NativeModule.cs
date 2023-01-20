using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace V2ex;

[DependsOn(typeof(AbpAutofacModule))]
public class AppModule : AbpModule
{
}
