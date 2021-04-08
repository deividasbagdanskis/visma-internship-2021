using System.Collections.Generic;

namespace LibraryApp.Utilities
{
    public interface IArgumentChecker
    {
        bool CheckIfAllArgsExists(IDictionary<string, string> passedArgs, string[] requiredArgs);
    }
}