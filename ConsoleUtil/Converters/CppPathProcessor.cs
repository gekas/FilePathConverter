using ConsoleUtil.Infrastructure;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleUtil.Converters
{
    internal class CppPathProcessor : DirectoryProcessor
    {
        public CppPathProcessor(IDirectoryReader reader, IFileWriter writer)
            : base(reader, writer)
        { }

        public override string Convert(string path)
        {
            return path.EndsWith(".cpp") ? $"{path} /" : string.Empty;
        }

        public override async Task ProcessFiles(string startDirectory, string outFilePath, SearchOption searchOption)
        {
            try
            {
                Writer.Initialize(outFilePath);

                foreach (var fileName in Reader.ReadFileNames(startDirectory, searchOption))
                {
                    var convertedFileName = Convert(fileName);

                    if (!string.IsNullOrEmpty(convertedFileName))
                    {
                        await Writer.AppendToEndAsync(convertedFileName);
                    }
                }
            }
            finally
            {
                Writer.Dispose();
            }
        }
    }
}
