/*  ModInfo.cs
 *  Version 1.4 (2025.04.17)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.Security.Cryptography;
using System.Text;

namespace ParallelReality
{
    public class ModFileInfo
    {
        public string Path { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Fullname { get; private set; } = string.Empty;
        public bool IsApplied { get; set; } = false;


        public ModFileInfo(string _fullname)
        {
            Fullname = _fullname;

            int pos = _fullname.LastIndexOf(System.IO.Path.PathSeparator);
            if (pos == -1)
            {
                Name = _fullname;
            }
            else
            {
                Path = _fullname[0..pos];
                Name = _fullname[(pos + 1)..];
            }
        }
    }

    public class ModInfoSimple
    {
        public int LoadedOrder = -1;
        public string Hash = string.Empty;
        public string Name = string.Empty;
    }

    public class ModInfo
    {
        public string Name { get; private set; }
        public string Author { get; private set; } = string.Empty;
        public string ModVersion { get; private set; } = string.Empty;
        public string GameVersion { get; private set; } = string.Empty;
        public string Url => m_Url;

        public byte[] Hash => m_Hash;

        public int ModId { get; private set; } = -1;
        public int LoadedOrder { get; set; } = -1;
        public string ModDir { get; private set; }
        public string ReadmeFileFullname { get; private set; } = string.Empty;
        public List<string> ModFiles { get; private set; }
        //public List<ModFileInfo> ModFiles { get; private set; }



        public ModInfo(string _path, int _index)
        {
            ModDir = _path;
            ModId = _index;

            DirectoryInfo dirInfo = new(_path);

            IEnumerable<string> files = Directory.EnumerateFiles(_path);
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file).ToLower();
                if (filename.Contains("readme"))
                {
                    ReadmeFileFullname = _path + "/" + filename;
                    ParseReadmeFile(ReadmeFileFullname);
                    break;
                }
            }

            if (string.IsNullOrEmpty(Name))
            {
                Name = dirInfo.Name;
            }


            List<string> list = GetFiles(_path);
            int len = ModManager.STR_FOLDER_NAME_DATA.Length + 1;

            ModFiles = new(list.Count);
            foreach (string name in list)
            {
                int pos = name.IndexOf(ModManager.STR_FOLDER_NAME_DATA);
                if (pos == -1)
                    continue;

                ModFiles.Add(name[(pos + len)..]);
            }

            string str = Name + Author + ModVersion + GameVersion;
            m_Hash = SHA256.HashData(Encoding.UTF8.GetBytes(str));
        }


        //public string GetModifiedFileNameShort(string _fullname)
        //{
        //    int pos = _fullname.IndexOf("Reality Break_Data");
        //    if (pos == -1)
        //        return string.Empty;


        //    return _fullname[(pos + len)..];
        //}


        public HashSet<int> GetOrComputeListOfModsBeingOverriden(Dictionary<string, List<int>> _fileToModsMap)
        {
            if (m_OverriddenMods != null)
                return m_OverriddenMods;

            m_OverriddenMods = new();

            foreach (var file in ModFiles)
            {
                if (!_fileToModsMap.TryGetValue(file, out var modIds))
                    continue;

                foreach (var id in modIds)
                {
                    if (id != ModId)
                        m_OverriddenMods.Add(id);
                }
            }

            return m_OverriddenMods;
        }


        public bool HasCollision(ModInfo _otherMod)
        {
            foreach (string file in ModFiles)
            {
                if (_otherMod.ModFiles.Contains(file))
                    return true;
            }

            return false;
        }

        private void ParseReadmeFile(string _fullname)
        {
            string[] lines = File.ReadAllLines(_fullname);
            foreach (string line in lines)
            {
                if (line.StartsWith("Author:") || line.StartsWith("Authors:"))
                {
                    Author = GetValueFromLine(line);
                }
                else if (line.StartsWith("Name:"))
                {
                    Name = GetValueFromLine(line);
                }
                else if (line.StartsWith("ModVersion:"))
                {
                    ModVersion = GetValueFromLine(line);
                }
                else if (line.StartsWith("GameVersion:"))
                {
                    GameVersion = GetValueFromLine(line);
                }
                else if (line.StartsWith("URL:"))
                {
                    m_Url = GetValueFromLine(line);
                }

                

            }





        }

        public string GetShortHash()
        {
            string str = string.Format("{0:x2}{1:x2}{2:x2}{3:x2}",
                m_Hash[0],
                m_Hash[1],
                m_Hash[2],
                m_Hash[3]);

            return str;
        }

        private string GetValueFromLine(string _line)
        {
            int pos = _line.IndexOf(':');
            if (pos == -1)
                return string.Empty;

            return _line[(pos + 1)..].Trim();
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


        private string m_Url = string.Empty;


        private HashSet<int>? m_OverriddenMods = null;

        private readonly byte[] m_Hash;
    }

}
