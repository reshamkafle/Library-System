using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Constant
{
    public class AuthorizationConstants
    {
        public static class Roles
        {
            public const string ADMINISTRATORS = "Administrators";
            public const string User = "User";

        }

        // TODO: Don't use this in production
        public const string DEFAULT_PASSWORD = "Pass@word1";
    }
}
