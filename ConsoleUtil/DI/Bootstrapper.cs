using Autofac;

namespace ConsoleUtil.DI
{
    internal class Bootstrapper
    {
        public IContainer Container { get; private set; }

        public void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<IOModule>();
            Container = builder.Build();
        }
    }
}
