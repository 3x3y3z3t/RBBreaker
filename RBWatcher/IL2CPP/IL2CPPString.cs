// ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RBWatcher.RealityBreak;

namespace RBWatcher.IL2CPP
{
    internal class IL2CPPString
    {
        public struct IL2CPPStringPtr
        {
            public IntPtr Ptr;
        }

        //[StructLayout(LayoutKind.Explicit)]
        //public struct IL2CPPStringStruct
        //{
        //    [FieldOffset(0x10)]
        //    public int Length;

        //    [FieldOffset(0x14)]
        //    public IntPtr CharArrayPtr;
        //}


        public string DotNetString { get; set; }


        public IL2CPPString(IL2CPPStringPtr _ptr)
        {
            int? length = RBObjectReader.ReadInt32(_ptr.Ptr + 0x10);
            if (length == null)
            {
                throw new Exception("Could not read string length.");
            }

            byte[]? bytes = RBObjectReader.ReadArray(_ptr.Ptr + 0x14, length.Value * 2);
            if (bytes == null)
            {
                throw new Exception("Could not read the string's char array.");
            }

            DotNetString = Encoding.Unicode.GetString(bytes, 0, length.Value * 2);
        }
    }

}
