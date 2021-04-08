using System.Collections.Generic;

namespace LibraryApp.Utilities
{
    public interface IArgumentParser
    {
        IDictionary<string, string> ParseArgsArrayIntoArgsDictionary(string[] args);
    }
}