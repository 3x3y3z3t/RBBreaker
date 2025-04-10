/*  ParallelReality
 *      This is a GUI mod loader for Reality Break.
 *  
 *  Program.cs
 *  Version 1.0 (2025.04.10)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace ParallelReality
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
