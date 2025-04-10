/*  ModManager.cs
 *  Version 1.1 (2025.04.11)
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
                m_ModdedFlagFile = m_BaseGameDir + "/modded.txt";

                ModDir = value + "/Mods";
            }
        }

        public string ModDir { get; private set; } = string.Empty;

        public bool IsGameModded { get => File.Exists(m_ModdedFlagFile); }


        public List<ModInfo> FoundMods { get; private set; }
        public List<int> SelectedMods { get; private set; }


        public ModManager()
        {
            FoundMods = new();
            SelectedMods = new();
        }


        public List<ModInfo> GetLoadedMods()
        {
            List<ModInfo> list = new();
            foreach (ModInfo mod in FoundMods)
            {
                if (mod.LoadedOrder != -1)
                    list.Add(mod);
            }

            list.Sort((ModInfo _a, ModInfo _b) => { return _a.LoadedOrder.CompareTo(_b.LoadedOrder); });
            return list;
        }

        public void CheckForDownloadedMods()
        {
            Directory.CreateDirectory(ModDir);

            FoundMods.Clear();

            int index = 0;
            IEnumerable<string> dirs = Directory.EnumerateDirectories(ModDir);
            foreach (var dir in dirs)
            {
                if (dir.EndsWith("Base Game"))
                    continue;

                ModInfo mod = new(dir, index);
                FoundMods.Add(mod);
                ++index;
            }

            Console.WriteLine("Found total " + FoundMods.Count + " mods.");
        }

        public void CheckForAppliedMods()
        {
            if (!IsGameModded)
            {
                foreach (var mod in FoundMods)
                {
                    mod.LoadedOrder = -1;
                }

                return;
            }

            List<ModInfo> loadedMods = new();
            string[] lines = File.ReadAllLines(m_ModdedFlagFile);
            foreach (string line in lines)
            {
                int pos = line.IndexOf('/');
                if (pos == -1) continue;

                if (!int.TryParse(line[0..pos], out int order))
                    continue;

                string name = line[(pos + 1)..];

                // ==========
                foreach (var mod in FoundMods)
                {
                    if (mod.Name == name)
                    {
                        mod.LoadedOrder = order;
                        loadedMods.Add(mod);
                        break;
                    }
                }

            }

            Console.WriteLine("    In which, " + loadedMods.Count + " mods are already loaded.");
            foreach (var mod in loadedMods)
            {
                Console.WriteLine("        '" + mod.Name + "' (Id = " + mod.Index + ")");
            }
        }

        public void BackupBaseGame(bool _force = false)
        {
            if (Directory.Exists(ModDir + "/Base Game") && !_force)
                return;

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

            foreach (var mod in FoundMods)
            {
                mod.LoadedOrder = -1;
            }

            Console.WriteLine("Base Game data restored.");
        }

        public void ApplyMods(List<int> _modIndices)
        {
            Console.WriteLine("Applying " + _modIndices.Count + " mods..");

            string[] names = new string[_modIndices.Count];
            for (int i = 0; i < _modIndices.Count; ++i)
            {
                ModInfo mod = FoundMods[_modIndices[i]];
                //ApplyMod(mod);
                names[i] = i + "/" + mod.Name;
                Console.WriteLine("    Applied mod '" + mod.Name + " (Id = " + mod.Index + ").");
            }

            File.WriteAllLines(m_ModdedFlagFile, names.ToArray());
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
