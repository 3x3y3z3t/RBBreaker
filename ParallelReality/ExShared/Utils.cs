/*  ExShared/Utils.cs
 *  Version 1.0 (2025.04.15)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExShared
{
    public static class Utils
    {
        public static string ClipString(string _original, int maxWidth)
        {
            if (maxWidth <= 0)
                return string.Empty;

            if (maxWidth <= 3)
                return _original[0..maxWidth].PadRight(maxWidth, ' ');

            if (_original.Length > maxWidth)
                return _original[0..(maxWidth - 3)] + "...";

            return _original.PadRight(maxWidth, ' ');
        }
    }

}
