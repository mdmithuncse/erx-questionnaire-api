using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Constants
{
    public class Constants
    {
        public const char CODE_DELIMITER = '|';
        public const char VALUE_DELIMITER = ',';

        public struct AuthorizePolicy
        {
            public const string CLIENT_KEY = "ClientKey";
            public const string ADMIN_KEY = "AdminKey";
        }
    }
}
