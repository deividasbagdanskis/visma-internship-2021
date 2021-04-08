using System.Collections.Generic;

namespace LibraryApp.Utilities
{
    public class ArgumentParser : IArgumentParser
    {
        public IDictionary<string, string> ParseArgsArrayIntoArgsDictionary(string[] args)
        {
            IDictionary<string, string> arguments = new Dictionary<string, string>();

            for (int i = 1; i < args.Length; i++)
            {
                string[] kvp = args[i].Split('=');
                arguments.Add(kvp[0].Split("--")[1], kvp[1]);
            }

            return arguments;
        }
    }
}
