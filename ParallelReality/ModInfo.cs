/*  ModInfo.cs
 *  Version 1.2 (2025.04.13)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ParallelReality
{
    internal class ModInfo
    {
        public string Name { get; private set; }
        public string Author { get; private set; } = string.Empty;
        public string ModVersion { get; private set; } = string.Empty;
        public string GameVersion { get; private set; } = string.Empty;
        public byte[] Hash { get => m_Hash; }

        public int Index { get; private set; } = -1;
        public int LoadedOrder { get; set; } = -1;
        public string ModDir { get; private set; }
        public string ReadmeFileFullname { get; private set; } = string.Empty;
        public List<string> ModifiedFiles { get; private set; }



        public ModInfo(string _path, int _index)
        {
            ModDir = _path;
            Index = _index;

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
            int len = ModManager.STR_DATA_FOLDER_NAME.Length + 1;

            ModifiedFiles = new(list.Count);
            foreach (string name in list)
            {
                int pos = name.IndexOf(ModManager.STR_DATA_FOLDER_NAME);
                if (pos == -1)
                    continue;

                ModifiedFiles.Add(name[(pos + len)..]);
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

        public bool HasCollision(ModInfo _otherMod)
        {
            foreach (string file in ModifiedFiles)
            {
                if (_otherMod.ModifiedFiles.Contains(file))
                    return true;
            }

            return false;
        }

        public void ParseReadmeFile(string _fullname)
        {
            string[] lines = File.ReadAllLines(_fullname);
            foreach (string line in lines)
            {
                if (line.StartsWith("Author") || line.StartsWith("Authors"))
                {
                    Author = GetValueFromLine(line);
                }
                else if (line.StartsWith("Name"))
                {
                    Name = GetValueFromLine(line);
                }
                else if (line.StartsWith("ModVersion"))
                {
                    ModVersion = GetValueFromLine(line);
                }
                else if (line.StartsWith("GameVersion"))
                {
                    GameVersion = GetValueFromLine(line);
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

        private string GetAuthorName(string _modDir)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_modDir);
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file).ToLower();
                if (!filename.Contains("readme"))
                    continue;

                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    if (!line.StartsWith("Author") && !line.StartsWith("Authors"))
                        continue;

                    int pos = line.IndexOf(':');
                    if (pos == -1)
                        continue;

                    return line[(pos + 1)..].Trim();
                }
            }

            return string.Empty;
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

        private readonly byte[] m_Hash;
    }

}
