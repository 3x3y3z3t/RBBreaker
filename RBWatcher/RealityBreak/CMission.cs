// ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RBWatcher.IL2CPP;
using static RBWatcher.IL2CPP.IL2CPPString;

namespace RBWatcher.RealityBreak
{
    internal class CMission
    {
        public struct Pointer { public IntPtr Ptr; }

        [StructLayout(LayoutKind.Explicit)]
        public struct Struct
        {
            [FieldOffset(0x30)]
            public IL2CPPStringPtr CurrentMapProtoName;

        }


        public string CurrentMapProtoName { get => m_CurrentMapProtoName; private set => m_CurrentMapProtoName = value; }


        public CMission(Pointer _ptr)
        {
            Struct? cstruct = RBObjectReader.Read<Struct>(_ptr.Ptr);
            if (cstruct == null)
            {
                throw new Exception("Could not read CMission.");
            }

            IL2CPPString il2cppString = new(cstruct.Value.CurrentMapProtoName);
            m_CurrentMapProtoName = il2cppString.DotNetString;


        }



        private string m_CurrentMapProtoName;
    }

}
