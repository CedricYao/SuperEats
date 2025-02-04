using Autofac;
using SuperEats.IoC;

namespace SuperEats.Bootstrap
{
    public class AutofacBootstrapper
    {
        public static IContainer Execute()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ApiModule>();
            return builder.Build();
        }
    }
}