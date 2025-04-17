/*  ModManager.cs
 *  Version 1.6 (2025.04.17)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

//#define DEV

using ExShared;

namespace ParallelReality
{
    public enum ModManagerState : byte
    {
        Idle,
        CheckingForMods,
        BackingUpBaseGame,
        RestoringBaseGame,
        ApplyingMods,
    }

    public class ModManager
    {
        public const string STR_FOLDER_NAME_DATA = "Reality Break_Data";
        public const string STR_FOLDER_NAME_STREAMING_ASSETS = "StreamingAssets";
        public const string STR_PATH_DATA_STREAMING_ASSETS = STR_FOLDER_NAME_DATA + "/" + STR_FOLDER_NAME_STREAMING_ASSETS;
        public const string STR_MOD_NAME_BASE_GAME = "Base Game";

        public static bool VerboseLog = true;


        public string BaseGameDir => m_BaseGameDir;

        public string ModDir => m_ModDir;

        public ModManagerState State { get => m_State; }

        //public bool IsApplyInProgress { get; private set; } = false;
        public bool IsGameModded { get => File.Exists(m_ModdedFlagFile); }


        public List<ModInfo> FoundMods { get; private set; }
        public List<int> SelectedMods { get; private set; }
        public Dictionary<string, List<int>> FilesToModMap => m_FileToModsMap;


        //public ModManager()
        //{
        //    FoundMods = new();
        //    SelectedMods = new();

        //    m_FileToModsMap = new();
        //}

        public ModManager(string _baseGameDir)
        {
            m_BaseGameDir = _baseGameDir;
            m_ModdedFlagFile = m_BaseGameDir + "/modded.txt";
            m_ModDir = m_BaseGameDir + "/Mods";

            FoundMods = new();
            SelectedMods = new();

            m_FileToModsMap = new();
        }


        public ModInfo? GetModWithId(int _id)
        {
            foreach (var mod in FoundMods)
            {
                if (mod.ModId == _id)
                    return mod;
            }

            return null;
        }

        public ModInfo? GetModWithShortHash(string _shortHash)
        {
            foreach (var mod in FoundMods)
            {
                if (mod.GetShortHash() == _shortHash)
                    return mod;
            }

            return null;
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
            m_State = ModManagerState.CheckingForMods;

            Directory.CreateDirectory(ModDir);

            FoundMods.Clear();

            int index = 0;
            IEnumerable<string> dirs = Directory.EnumerateDirectories(ModDir);
            foreach (var dir in dirs)
            {
                string? folderName = Path.GetFileName(dir);
                if (folderName == null || folderName.StartsWith(STR_MOD_NAME_BASE_GAME))
                    continue;

                ModInfo mod = new(dir, index);
                FoundMods.Add(mod);
                ++index;
            }

            Console.WriteLine("Found total " + FoundMods.Count + " mods.");

            string msg = "";
            string cap = "-----------------------------------------------------------\r\n";
            if (FoundMods.Count > 0 && VerboseLog)
            {
                msg = cap + "|   Id |   Hash   | Name                           | Load |\r\n" + cap;
            }

            List<ModInfoSimple> loadedModsPrecheck = CheckForAppliedMod_Precheck();

            foreach (var mod in FoundMods)
            {
                foreach (var file in mod.ModFiles)
                {
                    if (!m_FileToModsMap.ContainsKey(file))
                    {
                        m_FileToModsMap[file] = new List<int>();
                    }

                    m_FileToModsMap[file].Add(mod.ModId);
                }

                string hash = mod.GetShortHash();
                string loadedOrder = "-";

                foreach (var precheck in loadedModsPrecheck)
                {
                    if (hash == precheck.Hash)
                    {
                        mod.LoadedOrder = precheck.LoadedOrder;
                        loadedOrder = precheck.LoadedOrder.ToString();
                        break;
                    }
                }

                msg += string.Format("| {0,4} | {1} | {2} | {3,4} |\r\n",
                    mod.ModId, mod.GetShortHash(), Utils.ClipString(mod.Name, 30),
                    loadedOrder);
            }

            if (FoundMods.Count > 0 && VerboseLog)
            {
                Console.Write(msg + cap);
                Console.WriteLine("                                                    ↑ " + loadedModsPrecheck.Count + " mods loaded");
            }

            m_State = ModManagerState.Idle;
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

            m_State = ModManagerState.BackingUpBaseGame;

            string srcDir = BaseGameDir + "/" + STR_PATH_DATA_STREAMING_ASSETS;
            string dstDir = ModDir + "/Base Game/" + STR_PATH_DATA_STREAMING_ASSETS;
            CopyFiles(srcDir, dstDir);

            Console.WriteLine("Base Game data backed up.");

            m_State = ModManagerState.Idle;
        }

        public void RestoreBaseGame(Action<string> _callbackUpdateStatus)
        {
            string srcDir = ModDir + "/Base Game/" + STR_PATH_DATA_STREAMING_ASSETS;
            string dstDir = BaseGameDir + "/" + STR_PATH_DATA_STREAMING_ASSETS;

            m_State = ModManagerState.RestoringBaseGame;

            DeleteAppliedModsFiles(_callbackUpdateStatus);

            _callbackUpdateStatus("Restoring Base Game files..");
#if DEV
            Thread.Sleep(1500);
#else
            CopyFiles(srcDir, dstDir, true);
#endif

            if (File.Exists(m_ModdedFlagFile))
            {
                File.Delete(m_ModdedFlagFile);
            }

            foreach (var mod in FoundMods)
            {
                mod.LoadedOrder = -1;
            }

            Console.WriteLine("Base Game data restored.");

            m_State = ModManagerState.Idle;
        }

        public void ApplyMods(List<int> _modIndices, Action<string> _callbackUpdateStatus, CancellationToken _cancellationToken)
        {
            m_State = ModManagerState.ApplyingMods;

            //IsApplyInProgress = true;

            CopiesStatus.TotalFiles = 0;
            for (int i = 0; i < _modIndices.Count; ++i)
            {
                ModInfo mod = FoundMods[_modIndices[i]];
                CopiesStatus.TotalFiles += mod.ModFiles.Count;
            }

            Console.WriteLine("Applying " + _modIndices.Count + " mods (total " + CopiesStatus.TotalFiles + " files)..");

            string[] names = new string[_modIndices.Count];
            for (int i = 0; i < _modIndices.Count; ++i)
            {
                ModInfo mod = FoundMods[_modIndices[i]];

                try
                {
                    ApplyMod(mod, _callbackUpdateStatus, _cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    m_State = ModManagerState.Idle;
                    //IsApplyInProgress = false;
                    CopiesStatus.TotalFiles = 0;
                    CopiesStatus.CopiedFiles = 0;

                    Console.WriteLine("    User cancelled.");
                    throw;
                }

                mod.LoadedOrder = i;

                names[i] = i + "\t" + mod.GetShortHash() + "\t" + mod.Name;
                Console.WriteLine("    Applied mod '" + mod.Name + " (Id = " + mod.ModId + ").");
            }

            File.WriteAllLines(m_ModdedFlagFile, names.ToArray());

            m_State = ModManagerState.Idle;
            //IsApplyInProgress = false;
            CopiesStatus.TotalFiles = 0;
            CopiesStatus.CopiedFiles = 0;
        }

        private void ApplyMod(ModInfo _mod, Action<string> _callbackUpdateStatus, CancellationToken _cancellationToken)
        {
            string modPath = _mod.ModDir + "/" + STR_FOLDER_NAME_DATA + "/";
            string basePath = BaseGameDir + "/" + STR_FOLDER_NAME_DATA + "/";
            foreach (string name in _mod.ModFiles)
            {
                _cancellationToken.ThrowIfCancellationRequested();

                string srcName = modPath + name;
                string dstName = basePath + name;

#if DEV
                Thread.Sleep(500);
                ++CopiesStatus.CopiedFiles;
#else
                if (File.Exists(srcName))
                {
                    File.Copy(srcName, dstName, true);
                    ++CopiesStatus.CopiedFiles;
                }
                else
                {
                    Console.WriteLine("File '" + srcName + "' no longer exist.");
                }
#endif

                string text = "Applying mods: " + CopiesStatus.CopiedFiles + "/" + CopiesStatus.TotalFiles + " files copied.";
                _callbackUpdateStatus(text);
            }
        }

        private void DeleteAppliedModsFiles(Action<string> _callbackUpdateStatus)
        {
            //_callbackUpdateStatus("Deleting Modded files..");

            foreach (var mod in FoundMods)
            {
                if (mod.LoadedOrder == -1)
                    continue;

                CopiesStatus.TotalFiles += mod.ModFiles.Count;
            }

            foreach (var mod in FoundMods)
            {
                if (mod.LoadedOrder == -1)
                    continue;

                string modPath = mod.ModDir + "/" + STR_FOLDER_NAME_DATA + "/";
                foreach (var file in mod.ModFiles)
                {
                    string srcName = modPath + file;

#if DEV
                    Thread.Sleep(500);
                    ++CopiesStatus.CopiedFiles;
#else
                    if (File.Exists(srcName))
                    {
                        File.Delete(srcName);
                        ++CopiesStatus.CopiedFiles;
                    }
                    else
                    {
                        Console.WriteLine("File '" + srcName + "' no longer exist.");
                    }
#endif

                    string text = "Deleting Modded files: " + CopiesStatus.CopiedFiles + "/" + CopiesStatus.TotalFiles + " files deleted.";
                    _callbackUpdateStatus(text);
                }
            }

            CopiesStatus.TotalFiles = 0;
            CopiesStatus.CopiedFiles = 0;
        }

        private List<ModInfoSimple> CheckForAppliedMod_Precheck()
        {
            List<ModInfoSimple> list = new();
            if (!IsGameModded)
            {
                foreach (var mod in FoundMods)
                {
                    mod.LoadedOrder = -1;
                }

                return list;
            }

            string[] lines = File.ReadAllLines(m_ModdedFlagFile);
            foreach (string line in lines)
            {
                int pos = line.IndexOf('\t');
                if (pos == -1 || pos + 9 > line.Length)
                    continue;

                if (!int.TryParse(line[0..pos], out int order))
                    continue;

                string hash = line[(pos + 1)..(pos + 9)];

                string name = line[(pos + 1)..];

                list.Add(new()
                {
                    LoadedOrder = order,
                    Hash = hash,
                    Name = name,
                });
            }

            return list;
        }


        private static List<string> GetFiles(string _path)
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

        private static void CopyFiles(string _srcDir, string _dstDir, bool _overwrite = false)
        {
            _ = Directory.CreateDirectory(_dstDir);

            string[] files = Directory.GetFiles(_srcDir);
            foreach (string file in files)
            {
                string dstFilename = _dstDir + "/" + Path.GetFileName(file);
                File.Copy(file, dstFilename, _overwrite);
            }

            string[] dirs = Directory.GetDirectories(_srcDir);
            foreach (string dir in dirs)
            {
                string dstDirName = _dstDir + "/" + Path.GetFileName(dir);
                CopyFiles(dir, dstDirName, _overwrite);
            }
        }


        

        private  string m_BaseGameDir = string.Empty;
        private  string m_ModDir = string.Empty;

        private  string m_ModdedFlagFile = string.Empty;

        private ModManagerState m_State = ModManagerState.Idle;


        private readonly Dictionary<string, List<int>> m_FileToModsMap;
    }

}
