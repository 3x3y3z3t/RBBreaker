// ;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static RBWatcher.RealityBreak.CGame;

namespace RBWatcher
{
    internal class RBObjectReader
    {
        private const int Offs_CGameTypeInfo = 0x3431cc8;



        public static bool Init()
        {
            Process[] processes = Process.GetProcessesByName("Reality Break");

            if (processes.Length == 0)
            {
                Console.WriteLine("Game not found");
                return false;
            }

            Process gameProcess = processes[0];
            var modules = gameProcess.Modules;

            IntPtr gameAssemblyBaseAddr = IntPtr.Zero;
            foreach (ProcessModule module in modules)
            {
                if (module.ModuleName == "GameAssembly.dll")
                {
                    gameAssemblyBaseAddr = module.BaseAddress;
                    break;
                }
            }

            if (gameAssemblyBaseAddr == IntPtr.Zero)
            {
                Console.WriteLine("GameAssembly.dll is not loaded.");
                return false;
            }

            try
            {
                s_Instance = new(gameProcess);
                s_Instance.m_GameAssemblyBaseAddress = gameAssemblyBaseAddr;
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
                return false;
            }

            return true;
        }

        public static T? Read<T>(IntPtr _address) where T : struct
        {
            if (s_Instance == null)
            {
                throw new InvalidOperationException("RBObjectReader is not initialized (did you forget to call RBObjectReader.Init()?");
            }

            if (typeof(Struct) == typeof(T))
            {
                long? cgameTypeInfo = s_Instance.ReadInt64At(s_Instance.m_GameAssemblyBaseAddress + Offs_CGameTypeInfo);
                if (cgameTypeInfo == null)
                {
                    return null;
                }

                long? cgameStaticFields = s_Instance.ReadInt64At(new IntPtr(cgameTypeInfo.Value + 0xb8));
                if (cgameStaticFields == null)
                {
                    return null;
                }

                long? cgameInstance = s_Instance.ReadInt64At(new IntPtr(cgameStaticFields.Value + 0x00));
                if (cgameInstance == null)
                {
                    return null;
                }

                return s_Instance.ReadObject<T>(new IntPtr(cgameInstance.Value));
            }

            return s_Instance.ReadObject<T>(_address);
        }

        public static byte[]? ReadArray(IntPtr _address, int _length)
        {
            if (s_Instance == null)
            {
                throw new InvalidOperationException("RBObjectReader is not initialized (did you forget to call RBObjectReader.Init()?");
            }

            return s_Instance.ReadBytes(_address, _length);
        }

        public static int? ReadInt32(IntPtr _address)
        {
            if (s_Instance == null)
            {
                throw new InvalidOperationException("RBObjectReader is not initialized (did you forget to call RBObjectReader.Init()?");
            }

            var bytes = s_Instance.ReadBytes(_address, 4);
            if (bytes == null)
                return null;

            return BitConverter.ToInt32(bytes);
        }


        private RBObjectReader(Process _process)
        {
            const int PROCESS_VM_READ = 0x0010;
            const int PROCESS_QUERY_INFORMATION = 0x0400;

            m_Handle = OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, false, new IntPtr(_process.Id));
            if (m_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not open handle.");
            }

            m_Ready = true;
        }

        ~RBObjectReader()
        {
            bool success = CloseHandle(m_Handle);
            if (!success)
            {
                throw new InvalidOperationException("Could not close process handle.");
            }
        }


        private long? ReadInt64At(IntPtr _address)
        {
            byte[] buffer = new byte[8];
            IntPtr count = IntPtr.Zero;

            bool success = ReadProcessMemory(m_Handle, _address, buffer, 8, ref count);

            if (!success)
                return null;

            return BitConverter.ToInt64(buffer);
        }

        private T? ReadObject<T>(IntPtr _address) where T : struct
        {
            int objSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[objSize];
            IntPtr count = IntPtr.Zero;

            bool success = ReadProcessMemory(m_Handle, _address, buffer, objSize, ref count);
            if (!success)
                return null;

            GCHandle gc = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            T obj = Marshal.PtrToStructure<T>(gc.AddrOfPinnedObject());
            gc.Free();

            return obj;
        }

        private byte[]? ReadBytes(IntPtr _address, int _length)
        {
            byte[] buffer = new byte[_length];
            IntPtr count = IntPtr.Zero;

            bool success = ReadProcessMemory(m_Handle, _address, buffer, _length, ref count);
            if (!success)
                return null;

            return buffer;
        }


        private static RBObjectReader? s_Instance;

        private readonly bool m_Ready;
        private readonly IntPtr m_Handle = IntPtr.Zero;
        private IntPtr m_GameAssemblyBaseAddress = IntPtr.Zero;


        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int _dwDesiredAddress, bool _bInheritHandle, IntPtr _dwProcessId);


        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr _hObject);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr _hProcess, IntPtr _lpBaseAddress, byte[] _lpBuffer, int _nSize, ref IntPtr _lpNumberOfBytesRead);

    }

}
