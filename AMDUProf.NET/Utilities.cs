using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;


namespace AMDuProf.NET
{
    unsafe class Utilities
    {
        /// <summary>
        /// Convert a char pointer to a string in Ansi format to make it readable.
        /// Source: https://stackoverflow.com/questions/9041094/char-to-a-string-in-c-sharp
        /// </summary>
        /// <param name="convertCharP"></param>
        /// <returns></returns>
        public static string CharPToString(char* convertCharP)
        {
            return Marshal.PtrToStringAnsi((IntPtr)convertCharP);
        }
    }
}
