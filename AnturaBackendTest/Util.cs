using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnturaBackendTest
{
    public class ArgsValidationReturnType
    {
        public bool valid { get; init; }
        public string errorMessage { get; init; }
    }

    public class Util
    {
        // Since the instructions did not specify how the occurrences of the file name should be counted
        // I decided to only count each instance of a repeating string once.
        // Example:
        // file_file = 1 time
        // file_file_file = 1 time
        // file_file_file_file = 2 times
        public int CountOccurenceOfName(IEnumerable<string> lines, string target)
        {
            return lines.Sum(line => new Regex(Regex.Escape(target)).Matches(line).Count);
        }

        public ArgsValidationReturnType ValidateArgs(string[] args)
        {
            if (args.Length < 1)
            {
                return new ArgsValidationReturnType
                {
                    valid = false,
                    errorMessage = "You need to supply a file name. Example usage: <file_name.txt>"
                };
            }

            if (args.Length > 1)
            {
                return new ArgsValidationReturnType
                {
                    valid = false,
                    errorMessage = "Only one argument should be supplied. Example usage: <file_name.txt>"
                };
            }

            var filePath = args[0];

            if (!File.Exists(filePath))
            {
                return new ArgsValidationReturnType
                {
                    valid = false,
                    errorMessage = "No file found with that name."
                };
            }

            var endIndex = filePath.IndexOf('.');

            if (endIndex == -1)
            {
                return new ArgsValidationReturnType
                {
                    valid = false,
                    errorMessage = "Incorrect format of supplied file. Example usage: <file_name.txt>"
                };
            }

            return new ArgsValidationReturnType
            {
                valid = true,
                errorMessage = string.Empty
            };
        }
    }
}