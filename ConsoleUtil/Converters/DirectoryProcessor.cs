using ConsoleUtil.Infrastructure;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleUtil
{
    internal abstract class DirectoryProcessor
    {
        protected IDirectoryReader Reader { get; set; }
        protected IFileWriter Writer { get; set; }

        /// <summary>
        /// Strategy method of path conversion.
        /// </summary>
        /// <param name="path">Path to be converted.</param>
        /// <returns></returns>
        public abstract string Convert(string path);

        protected DirectoryProcessor(IDirectoryReader reader, IFileWriter writer)
        {
            Reader = reader;
            Writer = writer;
        }

        /// <summary>
        /// Define files processing behavior.
        /// </summary>
        /// <param name="startDirectory">Directory to start processing.</param>
        /// <param name="outFilePath">Result file path.</param>
        /// <param name="searchOption">Defines behavior of enumeration through directory. Possible options: Top, Recursive.</param>
        /// <returns></returns>
        public virtual async Task ProcessFiles(string startDirectory, string outFilePath, SearchOption searchOption)
        {
            try
            {
                Writer.Initialize(outFilePath);

                foreach (var fileName in Reader.ReadFileNames(startDirectory, searchOption))
                {
                    var convertedFileName = Convert(fileName);
                    await Writer.AppendToEndAsync(convertedFileName);
                }
            }
            finally
            {
                Writer.Dispose();
            }
        }
    }
}
