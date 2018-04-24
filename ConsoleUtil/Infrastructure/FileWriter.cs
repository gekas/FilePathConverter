using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleUtil.Infrastructure
{
    internal class FileWriter : IFileWriter
    {
        private FileStream _fileStream;
        private StreamWriter _streamWriter;

        public void Initialize(string filePath)
        {
            _fileStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            _streamWriter = new StreamWriter(_fileStream);
        }

        public async Task AppendToEndAsync(string content)
        {
            if (!string.IsNullOrEmpty(content.Trim()))
            {
               await _streamWriter.WriteLineAsync("\n" + content);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_streamWriter != null)
                {
                    _fileStream.Flush();
                    
                    _streamWriter.Dispose();
                    _fileStream.Dispose();
                }
            }
        }
    }
}
