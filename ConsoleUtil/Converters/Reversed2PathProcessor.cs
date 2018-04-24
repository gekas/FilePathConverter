using ConsoleUtil.Infrastructure;
using System;
using System.Linq;

namespace ConsoleUtil.Converters
{

    internal class Reversed2PathProcessor : DirectoryProcessor
    {
        public Reversed2PathProcessor(IDirectoryReader reader, IFileWriter writer)
            : base(reader, writer)
        { }

        public override string Convert(string path)
        {
            var dirs = path.Split('\\', '/').Reverse();
            return String.Join("/", dirs);
        }
    }
}
