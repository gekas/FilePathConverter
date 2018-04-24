using System;
using ConsoleUtil.Converters;

namespace ConsoleUtil
{
    internal class DirectoryProcessorFactory
    {
        private readonly Func<Type, object> _fabricMethod;

        public DirectoryProcessorFactory(Func<Type, object> fabricMethod)
        {
            _fabricMethod = fabricMethod;
        }

        /// <summary>
        /// Factory method creates <see cref="DirectoryProcessor"/> corresponding to <see cref="ConverterType"/>
        /// </summary>
        /// <param name="type">Converter type.</param>
        /// <returns></returns>
        public DirectoryProcessor Create(ConverterType type)
        {
            DirectoryProcessor processor = null;
            switch (type)
            {
                case (ConverterType.All):
                    processor = (DirectoryProcessor)_fabricMethod(typeof(AllPathProcessor));
                    break;
                case (ConverterType.Cpp):
                    processor = (DirectoryProcessor)_fabricMethod(typeof(CppPathProcessor));
                    break;
                case (ConverterType.Reversed1):
                    processor = (DirectoryProcessor)_fabricMethod(typeof(Reversed1PathProcessor));
                    break;
                case (ConverterType.Reversed2):
                    processor = (DirectoryProcessor)_fabricMethod(typeof(Reversed2PathProcessor));
                    break;
            }

            return processor;
        }
    }
}
