using Volo.Abp.Modularity;
namespace V2ex;

[DependsOn(typeof(V2exSharedModule))]
public class AppModule: AbpModule
{

}