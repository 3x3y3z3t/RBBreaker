using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using RBWatcher.RealityBreak;

namespace RBWatcher
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!RBObjectReader.Init())
            {
                return;
            }

            CGame game;

            while (true)
            {
                Console.Write("Command: ");
                string? command = Console.ReadLine();
                if (command == null)
                    continue;

                if (command == "read")
                {
                    game = new CGame();

                    //Console.WriteLine("===== TEST =====");
                    Console.WriteLine("Pilot Name: " + game.Player.PilotName);
                    Console.WriteLine("Cycle: " + game.Player.TotalNumBreak);
                    Console.WriteLine("Current Map Name: " + game.Player.MissionManager.Mission.CurrentMapProtoName);
                    Console.WriteLine();
                }



            }













            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }


    }

    public abstract class RBObject
    {

    }

    public class ProcessHandle : IDisposable
    {
        public bool Ready { get; private set; } = false;


        public ProcessHandle(Process _process)
        {
            const int PROCESS_VM_READ = 0x0010;
            const int PROCESS_QUERY_INFORMATION = 0x0400;


            m_Handle = OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, false, new IntPtr(_process.Id));
            if (m_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not open process.");
            }

            Ready = true;
        }


        public void Dispose()
        {
            bool success = CloseHandle(m_Handle);
            if (!success)
            {
                throw new InvalidOperationException("Could not close process handle.");
            }
        }


        public long? ReadInt64At(IntPtr _address)
        {
            byte[] buffer = new byte[8];
            IntPtr count = IntPtr.Zero;

            bool success = ReadProcessMemory(m_Handle, _address, buffer, 8, ref count);

            if (!success)
                return null;

            return BitConverter.ToInt64(buffer);
        }

        public T? ReadObject<T>(IntPtr _address) where T : struct
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
















        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int _dwDesiredAddress, bool _bInheritHandle, IntPtr _dwProcessId);


        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr _hObject);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr _hProcess, IntPtr _lpBaseAddress, byte[] _lpBuffer, int _nSize, ref IntPtr _lpNumberOfBytesRead);



        private IntPtr m_Handle;
    }




}
