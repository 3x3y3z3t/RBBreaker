using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RBWatcher.RealityBreak
{
    internal class CGame
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Struct
        {
            [FieldOffset(0x68)]
            public bool Initialized;

            [FieldOffset(0x80)]
            public CPlayer.Pointer PlayerPtr;
        }

        public bool Initialized { get; set; }
        public CPlayer Player { get; set; }


        public CGame()
        {
            Struct? cgame = RBObjectReader.Read<Struct>(IntPtr.Zero);
            if (cgame == null)
            {
                throw new Exception("Could not read CGame.");
            }

            Initialized = cgame.Value.Initialized;
            Player = new(cgame.Value.PlayerPtr);
        }








    }

}
