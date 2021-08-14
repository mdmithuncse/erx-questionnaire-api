using Common.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Extension
{
    public static class StringExtension
    {
        public static IList<string> GetListFromString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new List<string>();

            return input.Split(Constants.VALUE_DELIMITER).ToList();
        }
    }
}
