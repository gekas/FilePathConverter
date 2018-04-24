using System;
using System.Threading.Tasks;

namespace ConsoleUtil.Infrastructure
{
    internal interface IFileWriter : IDisposable
    {
        void Initialize(string filePath);
        Task AppendToEndAsync(string content);
    }
}
