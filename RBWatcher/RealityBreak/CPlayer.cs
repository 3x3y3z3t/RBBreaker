// ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RBWatcher.IL2CPP;
using static RBWatcher.IL2CPP.IL2CPPString;
using static RBWatcher.RealityBreak.CGame;

namespace RBWatcher.RealityBreak
{
    internal class CPlayer
    {
        public struct Pointer
        {
            public IntPtr Ptr;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Struct
        {
            [FieldOffset(0x390)]
            public CMissionManager.Pointer MissionManagerPtr;

            [FieldOffset(0x380)]
            public IL2CPPStringPtr PilotName;

            [FieldOffset(0x3fc)]
            public uint TotalNumBreak;
        }

        public string PilotName { get => m_PilotName; set { m_PilotName = value; } }
        public uint TotalNumBreak { get => m_TotalNumBreak; set {  m_TotalNumBreak = value; } }

        public CMissionManager MissionManager { get; private set; }


        public CPlayer(Pointer _ptr)
        {
            Struct? cplayer = RBObjectReader.Read<Struct>(_ptr.Ptr);
            if (cplayer == null)
            {
                throw new Exception("Could not read CPlayer.");
            }

            IL2CPPString il2cppString = new(cplayer.Value.PilotName);
            m_PilotName = il2cppString.DotNetString;

            m_TotalNumBreak = cplayer.Value.TotalNumBreak;

            MissionManager = new(cplayer.Value.MissionManagerPtr);
        }



        private string m_PilotName;
        private uint m_TotalNumBreak;
    }

}
