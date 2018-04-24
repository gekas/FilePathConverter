using System.Collections.Generic;
using System.IO;

namespace ConsoleUtil.Infrastructure
{
    internal interface IDirectoryReader
    {
        IEnumerable<string> ReadFileNames(string sourceDirectory, SearchOption searchOption);
    }
}
