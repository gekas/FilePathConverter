using Autofac;
using ConsoleUtil.Converters;
using ConsoleUtil.DI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleUtil.Tests
{
    [TestClass]
    public class ConvertersTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();
            _container = bootstrapper.Container;
        }

        [TestMethod]
        public void AllPathConverter_Convert_PathReversed()
        {
            // Arrange
            var path = "C:/Windows";
            var reversedPath = "C:/Windows";
            var converter = _container.Resolve<AllPathProcessor>();

            // Act
            var result = converter.Convert(path);

            // Assert
            Assert.IsTrue(result == reversedPath);
        }

        [TestMethod]
        public void CppPathConverter_CppFile_SlashAdded()
        {
            // Arrange
            var path = "C:/Main.cpp";
            var reversedPath = "C:/Main.cpp /";
            var converter = _container.Resolve<CppPathProcessor>();

            // Act
            var result = converter.Convert(path);

            // Assert
            Assert.IsTrue(result == reversedPath);
        }

        [TestMethod]
        public void CppPathConverter_NotCppFile_EmptyPath()
        {
            // Arrange
            var path = "C:/Main.cs";
            var reversedPath = string.Empty;
            var converter = _container.Resolve<CppPathProcessor>();

            // Act
            var result = converter.Convert(path);

            // Assert
            Assert.IsTrue(result == reversedPath);
        }

        [TestMethod]
        public void Reversed1PathConverter_Convert_StringReversed()
        {
            // Arrange
            var path = "C:/Windows";
            var reversedPath = "swodniW/:C";
            var converter = _container.Resolve<Reversed1PathProcessor>();

            // Act
            var result = converter.Convert(path);

            // Assert
            Assert.IsTrue(result == reversedPath);
        }

        [TestMethod]
        public void Reversed2PathConverter_Convert_PathReversed()
        {
            // Arrange
            var path = "C:/Windows";
            var reversedPath = "Windows/C:";
            var converter = _container.Resolve<Reversed2PathProcessor>();

            // Act
            var result = converter.Convert(path);

            // Assert
            Assert.IsTrue(result == reversedPath);
        }
    }
}
