using System;
using System.IO;

namespace AnturaBackendTest
{
    class Program
    {
        private static string[] args;
        private Util _util;

        Program(string[] args)
        {
            Program.args = args;
            _util = new Util();
        }

        private void Run()
        {
            var argsValidationData = _util.ValidateArgs(args);
            if (!argsValidationData.valid)
            {
                Console.Write(argsValidationData.errorMessage);
                return;
            }

            var filePath = args[0];
            var fileName = Path.GetFileName(filePath);

            var endIndex = fileName.IndexOf('.');
            var target = fileName[..endIndex];

            var lines = File.ReadAllLines(filePath);
            var counter = _util.CountOccurenceOfName(lines, target);

            Console.WriteLine($"Found {counter} instances of {target}");
        }

        static void Main(string[] args)
        {
            Program program = new(args);
            program.Run();
        }
    }
}