using Autofac;
using ConsoleUtil.Converters;
using ConsoleUtil.Infrastructure;

namespace ConsoleUtil.DI
{
    internal class IOModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileWriter>().As<IFileWriter>();
            builder.RegisterType<DirectoryReader>().As<IDirectoryReader>();

            builder.Register(c => new AllPathProcessor(c.Resolve<IDirectoryReader>(), c.Resolve<IFileWriter>()));
            builder.Register(c => new CppPathProcessor(c.Resolve<IDirectoryReader>(), c.Resolve<IFileWriter>()));
            builder.Register(c => new Reversed1PathProcessor(c.Resolve<IDirectoryReader>(), c.Resolve<IFileWriter>()));
            builder.Register(c => new Reversed2PathProcessor(c.Resolve<IDirectoryReader>(), c.Resolve<IFileWriter>()));
        }
    }
}
