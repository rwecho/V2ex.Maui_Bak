using Volo.Abp.DependencyInjection;

namespace V2ex;

public class HelloWorldService : ITransientDependency
{
    public string SayHello()
    {
        return "Hello, World!";
    }
}