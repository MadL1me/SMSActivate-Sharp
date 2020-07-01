using System;
using System.Collections.Generic;
using System.Text;

namespace SMSActivate
{
    public static class ExtensionMethods
    {
        public static string BooleanToString(this bool value) => (value) ? "0" : "1";
    }
}
