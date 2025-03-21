// ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static RBWatcher.RealityBreak.CPlayer;

namespace RBWatcher.RealityBreak
{
    internal class CMissionManager
    {
        public struct Pointer { public IntPtr Ptr; }

        [StructLayout(LayoutKind.Explicit)]
        public struct Struct
        {
            [FieldOffset(0x10)]
            public CMission.Pointer MissionManagerPtr;

        }


        public CMission Mission { get; set; }


        public CMissionManager(Pointer _ptr)
        {
            Struct? cmissionmanager = RBObjectReader.Read<Struct>(_ptr.Ptr);
            if (cmissionmanager == null)
            {
                throw new Exception("Could not read CMissionManager.");
            }

            Mission = new(cmissionmanager.Value.MissionManagerPtr);





        }
    }

}
