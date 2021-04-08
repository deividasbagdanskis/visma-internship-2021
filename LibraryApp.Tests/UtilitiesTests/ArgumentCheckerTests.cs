using LibraryApp.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApp.Tests.UtilitiesTests
{
    public class ArgumentCheckerTests
    {
        private IArgumentChecker _argumentChecker;

        public ArgumentCheckerTests()
        {
            _argumentChecker = new ArgumentChecker();
        }

        [Fact]
        public void CheckIfAllArgsExists_True()
        {
            IDictionary<string, string> passedArgs = new Dictionary<string, string>();
            passedArgs.Add("name", "1984");
            passedArgs.Add("author", "George Orwell");

            string[] requiredArgs = new string[] { "name", "author" };

            bool result = _argumentChecker.CheckIfAllArgsExists(passedArgs, requiredArgs);

            Assert.True(result);
        }

        [Fact]
        public void CheckIfAllArgsExists_False()
        {
            IDictionary<string, string> passedArgs = new Dictionary<string, string>();
            passedArgs.Add("name", "1984");
            passedArgs.Add("author", "George Orwell");

            string[] requiredArgs = new string[] { "name", "author", "ISBN" };

            bool result = _argumentChecker.CheckIfAllArgsExists(passedArgs, requiredArgs);

            Assert.False(result);
        }
    }
}
