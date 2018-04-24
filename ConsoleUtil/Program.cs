//  Cделать консольную утилиту, которая:
//    1. Принимает следующие параметры: стартовая папка, названия действия, путь к файлу с результатами(по умолчанию results.txt).
//    2. Проходит по всем вложенным папкам и для каждого файла, который находятся в папке в зависимости от параметра "название действия":
//        a.all - для всех файлов запоминает путь к файлу, относительно параметра "стартовая папка".
//        b.срр - для всех файлов с расширением срр(*.срр) запоминает путь к файлу, относительно параметра "стартовая папка", и добавляет к нему строку " /".
//        c.reversed1 - для всех файлов запоминает путь к файлу, относительно параметра "стартовая папка", при этом строка пути должна быть преобразована таким образом, чтобы она читалась от имени файла до родительского каталога(например, f\bla\ra\t.dat -> t.dat\ra\bla\f).
//        d.reversed2 - для всех файлов запоминает путь к файлу, относительно параметра "стартовая папка", при этом строка пути должна быть перевернута(например, f\bla\ra\t.dat -> tad.t\ar\alb\f).
//    3. Все полученные данные должны быть записаны в результирующий текстовый файл, каждая строка которого содержит обработанное имя файла.

//  Пример использования: _toolname.exe c:\some-dir all
//  Реализовать с ООП подходом обработчик в виде отдельных классов, которые объединены общим интерфейсом.
//  Использовать можно любые библиотеки (nuget).
//  Написать необходимые unit tests. Использовать Dependency injection (с использованием любого DI Container). Проход по папкам должен выполняться асинхронно(async/await).

using Autofac;
using ConsoleUtil.DI;
using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConsoleUtil.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ConsoleUtil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = ParseArguments(args);
            var bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();

            var converter = (new DirectoryProcessorFactory(bootstrapper.Container.Resolve)).Create(options.CommandType);
            try
            {
                converter.ProcessFiles(options.StartDir, options.FilePath, SearchOption.AllDirectories)
                    .Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static ProgramOptions ParseArguments(string[] args)
        {
            if(args.Length < 2) throw new ArgumentException("Invalid number of arguments.");

            var options = new ProgramOptions
            {
                StartDir = args[0].Trim()
            };

            var resultFilePath = args[2].Trim();
            resultFilePath = string.IsNullOrEmpty(resultFilePath) ? $"{options.StartDir}/Results.txt" : resultFilePath;
            options.FilePath = resultFilePath;

            var command = args[1].Trim().ToLower();
            if (command == "cpp")
                options.CommandType = ConverterType.Cpp;
            else if (command == "reversed1")
                options.CommandType = ConverterType.Reversed1;
            else if (command == "reversed2")
                options.CommandType = ConverterType.Reversed2;
            else if (command == "all")
                options.CommandType = ConverterType.All;
            else
                throw new ArgumentException("Unrecognized command specified.");

            return options;
        }

        private class ProgramOptions
        {
            public string StartDir { get; set; }
            public string FilePath { get; set; }
            public ConverterType CommandType { get; set; }
        }
    }
}
