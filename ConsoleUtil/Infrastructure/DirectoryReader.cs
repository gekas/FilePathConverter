using System.Collections.Generic;
using System.IO;

namespace ConsoleUtil.Infrastructure
{
    internal class DirectoryReader : IDirectoryReader
    {
        public IEnumerable<string> ReadFileNames(string sourceDirectory, SearchOption searchOption)
        {
            foreach (string file in Directory.EnumerateFiles(sourceDirectory, "*", searchOption))
            {
                yield return file;
            }
        }
    }
}
