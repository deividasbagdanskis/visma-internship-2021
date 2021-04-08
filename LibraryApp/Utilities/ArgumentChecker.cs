using System.Collections.Generic;

namespace LibraryApp.Utilities
{
    public class ArgumentChecker : IArgumentChecker
    {
        public bool CheckIfAllArgsExists(IDictionary<string, string> passedArgs, string[] requiredArgs)
        {
            IList<bool> results = new List<bool>();

            for (int i = 0; i < requiredArgs.Length; i++)
            {
                bool requiredArgExists = passedArgs.ContainsKey(requiredArgs[i]) &&
                    !string.IsNullOrWhiteSpace(passedArgs[requiredArgs[i]]);

                if (requiredArgExists)
                    results.Add(requiredArgExists);
            }

            return results.Count == requiredArgs.Length;
        }
    }
}
