using ConsoleUtil.Infrastructure;

namespace ConsoleUtil.Converters
{
    internal class AllPathProcessor : DirectoryProcessor
    {
        public AllPathProcessor(IDirectoryReader reader, IFileWriter writer)
            : base(reader, writer)
        { }

        public override string Convert(string path)
        {
            return path;
        }
    }
}
