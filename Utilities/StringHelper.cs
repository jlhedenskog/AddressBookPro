using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book_Pro.Utilities
{
    public static class StringHelper
    {
        //"this" allows you to use this method as an extension. I.e. longString.CapString()
        public static string CapString(this string st, int size)
        {
            //The Substring function breaks if the length of the string is less than the substring
            if(st.Length > size)
            {
                return st.Substring(0, size) + "...";
            } else
            {
                return st;
            }
        }
    }
}
