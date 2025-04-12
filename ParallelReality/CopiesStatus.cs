/*  CopiesStatus.cs
 *  Version 1.0 (2025.04.12)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelReality
{
    internal static class CopiesStatus
    {
        public static int TotalFiles;
        public static int CopiedFiles;


        public static void Clear()
        {
            TotalFiles = 0;
            CopiedFiles = 0;
        }



    }

}
