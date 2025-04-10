/*  ModManager.cs
 *  Version 1.0 (2025.04.10)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelReality
{
    internal class ModManager
    {
        public const string STR_DATA_FOLDER_NAME = "Reality Break_Data";

        public string BaseGameDir
        {
            get => m_BaseGameDir;
            set
            {
                m_BaseGameDir = value;
                m_ModdedFlagFile = m_BaseGameDir + "/modded";
            }
        }

        public string ModDir { get; private set; } = string.Empty;



        public List<ModInfo> FoundMods { get; private set; }
        public List<int> SelectedMods { get; private set; }


        public ModManager()
        {
            FoundMods = new();
            SelectedMods = new();
        }

        public void PopulateModInfo()
        {
            ModDir = BaseGameDir + "/Mods";
            Directory.CreateDirectory(ModDir);



            IEnumerable<string> dirs = Directory.EnumerateDirectories(ModDir);

            foreach (var dir in dirs)
            {
                if (dir.EndsWith("Base Game"))
                    continue;

                ModInfo mod = new(dir);
                FoundMods.Add(mod);
            }




        }

        public void BackupBaseGame()
        {
            Directory.CreateDirectory(ModDir + "/Base Game");

            if (File.Exists(m_ModdedFlagFile))
            {
                Console.WriteLine("Game seems to be in modded state, you shouldn't backup a modded game.");
                return;
            }

            string part = "/Reality Break_Data/StreamingAssets";
            string srcDir = BaseGameDir + part;
            string dstDir = ModDir + "/Base Game" + part;
            CopyFiles(srcDir, dstDir);

            Console.WriteLine("Base Game data backed up.");
        }

        const string c_Path_StreamingAssets = "/Reality Break_Data/StreamingAssets";
        public void RestoreBaseGame()
        {
            string srcDir = ModDir + "/Base Game" + c_Path_StreamingAssets;
            string dstDir = BaseGameDir + c_Path_StreamingAssets;
            CopyFiles(srcDir, dstDir);

            if (File.Exists(m_ModdedFlagFile))
            {
                File.Delete(m_ModdedFlagFile);
            }

            Console.WriteLine("Base Game data restored.");
        }

        public void ApplyMods(List<int> _modIndices)
        {
            if (_modIndices.Count == 0)
            {
                Console.WriteLine("No mod selected.");
                return;
            }

            foreach (int index in _modIndices)
            {
                ModInfo mod = FoundMods[index];

                ApplyMod(mod);
                Console.WriteLine("Applied mod (" + index + ") " + mod.Name + ".");
            }

            File.Create(m_ModdedFlagFile).Close();
        }

        private void ApplyMod(ModInfo _mod)
        {
            string srcDir = _mod.ModDir + "/" + STR_DATA_FOLDER_NAME;
            string dstDir = BaseGameDir + "/" + STR_DATA_FOLDER_NAME;
            CopyFiles(srcDir, dstDir);



        }


        private List<string> GetFiles(string _path)
        {
            List<string> list = new();

            string[] files = Directory.GetFiles(_path);
            list.AddRange(files);

            string[] dirs = Directory.GetDirectories(_path);
            foreach (string dir in dirs)
            {
                List<string> subFiles = GetFiles(dir);
                list.AddRange(subFiles);
            }

            return list;
        }

        private void CopyFiles(string _srcDir, string _dstDir)
        {
            _ = Directory.CreateDirectory(_dstDir);

            string[] files = Directory.GetFiles(_srcDir);
            foreach (string file in files)
            {
                string dstFilename = _dstDir + "/" + Path.GetFileName(file);
                File.Copy(file, dstFilename, true);
            }

            string[] dirs = Directory.GetDirectories(_srcDir);
            foreach (string dir in dirs)
            {
                string dstDirName = _dstDir + "/" + Path.GetFileName(dir);
                CopyFiles(dir, dstDirName);
            }
        }



        private string m_BaseGameDir = string.Empty;

        private string m_ModdedFlagFile = string.Empty;

    }

}
