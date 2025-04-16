/*  Config.cs
 *  Version 1.0 (2025.04.13)
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
    internal static class Config
    {
        public const string STR_CFG_FILE_NAME = "config.ini";

        public static string BaseGameDir = string.Empty;


        /// <summary>
        ///     Read the specified config file, or create a new (empty) file if it does not exist.<br />
        ///     Config file should be an <c>ini</c> file, or must be in <c>ini</c> format.
        /// </summary>
        /// <param name="_fullname">The full qualified name of the config file to read.</param>
        /// <returns><c>true</c> if the file is read or if a new file is created. Otherwise <c>false</c>.</returns>
        public static bool ReadOrCreateNewConfigFile(string _fullname = STR_CFG_FILE_NAME)
        {
            s_ConfigFileFullname = _fullname;

            if (!File.Exists(_fullname))
            {
                try
                {
                    File.CreateText(_fullname).Close();
                }
                catch (Exception)
                {
                    return false;
                }

                Console.WriteLine("Config file not found, a new file has been created.");
                return true;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(_fullname);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("Couldn't open config file to read: " + _ex);
                return false;
            }

            foreach (string line in lines)
            {
                int commentPos = line.IndexOf("#");
                if (commentPos == 0)
                    continue;
                if (commentPos == -1)
                    commentPos = line.Length;

                int pos = line.IndexOf('=');
                if (pos == -1)
                    continue;

                if (commentPos < pos)
                    continue;

                // ==========
                string key = line[0..pos];
                string value = line[(pos + 1)..commentPos];

                if (key == "BaseGameDir")
                {
                    BaseGameDir = value;
                }





            }

            return true;
        }

        public static bool SaveConfigFile(string? _fullname = null)
        {
            if (_fullname == null)
                _fullname = s_ConfigFileFullname;

            if (_fullname == null)
                return false;

            List<string> list = new()
            {
                "BaseGameDir=" + BaseGameDir
            };

            try
            {
                File.WriteAllLines(_fullname, list);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("Couldn't open config file to save: " + _ex);
                return false;
            }

            return true;
        }



        private static string? s_ConfigFileFullname = null;
    }

}
