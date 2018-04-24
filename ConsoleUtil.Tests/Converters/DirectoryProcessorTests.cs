using Autofac;
using ConsoleUtil.Converters;
using ConsoleUtil.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleUtil.Tests.Converters
{
    [TestClass]
    public class DirectoryProcessorTests
    {
        private IContainer _container;
        private Mock<IDirectoryReader> _directoryReaderMock;
        private Mock<IFileWriter> _fileWriterMock;

        [TestInitialize]
        public void Initialize()
        {
            CreateMocks();

            var builder = new ContainerBuilder();
            RegisterInstances(builder);
            _container = builder.Build();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _container = null;
            _directoryReaderMock = null;
            _fileWriterMock = null;
        }

        [TestMethod]
        public void DirectoryProcessor_ProcessFiles_AllFilesWritten()
        {
            // Arrange
            var processor = _container.Resolve<AllPathProcessor>();

            // Act
            processor.ProcessFiles("", "", SearchOption.AllDirectories)
                     .Wait();

            // Assert
            _fileWriterMock.Verify<Task>(x => x.AppendToEndAsync(It.IsAny<string>()), Times.Exactly(3));
        }

        [TestMethod]
        public void CppDirectoryProcessor_ProcessFiles_CppFilesWritten()
        {
            // Arrange
            var processor = _container.Resolve<CppPathProcessor>();

            // Act
            processor.ProcessFiles("", "", SearchOption.AllDirectories)
                     .Wait();

            // Assert
            _fileWriterMock.Verify<Task>(x => x.AppendToEndAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        private void CreateMocks()
        {
            _fileWriterMock = new Mock<IFileWriter>();

            _fileWriterMock.Setup(w => w.AppendToEndAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true)).Verifiable();

            _directoryReaderMock = new Mock<IDirectoryReader>();
            _directoryReaderMock.Setup(w => w.ReadFileNames(It.IsAny<string>(), It.IsAny<SearchOption>()))
                .Returns(new List<string>
                    { "File1.cs", "File2.cpp", "File3.py" });
        }

        private void RegisterInstances(ContainerBuilder builder)
        {
            builder.RegisterInstance(_fileWriterMock.Object).As<IFileWriter>();
            builder.RegisterInstance(_directoryReaderMock.Object).As<IDirectoryReader>();
            builder.Register(b => new AllPathProcessor(b.Resolve<IDirectoryReader>(), b.Resolve<IFileWriter>()));
            builder.Register(b => new CppPathProcessor(b.Resolve<IDirectoryReader>(), b.Resolve<IFileWriter>()));
        }
    }
}
