using LibraryApp.Utilities;
using System.Collections.Generic;
using Xunit;

namespace LibraryApp.Tests.UtilitiesTests
{
    public class ArgumentParserTests
    {
        private IArgumentParser _argumentParser;

        public ArgumentParserTests()
        {
            _argumentParser = new ArgumentParser();
        }

        [Fact]
        public void ParseArgsArrayIntoArgsDictionary_3_Arguments_Pass()
        {
            string[] args = new string[] { "add", "--author=George Orwell", "--ISBN=978-0151010264" };

            IDictionary<string, string> parsedArguments = _argumentParser.ParseArgsArrayIntoArgsDictionary(args);

            Assert.Equal(args.Length - 1, parsedArguments.Count);
        }
    }
}
