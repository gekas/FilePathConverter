using ConsoleUtil.Infrastructure;
using System.Linq;

namespace ConsoleUtil.Converters
{
    internal class Reversed1PathProcessor : DirectoryProcessor
    {
        public Reversed1PathProcessor(IDirectoryReader reader, IFileWriter writer)
            : base(reader, writer)
        { }

        public override string Convert(string path)
        {
            return new string(path.Reverse().ToArray());
        }
    }
}
