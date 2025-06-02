using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSaveEditor
{
    public class TalentInfo
    {
        public string ProtoName = null!;
        public string ProtoNameShort = null!;
        public int Tier = -1;

        public string FriendlyName = string.Empty;
        public string Description = string.Empty;
    }

    public static class TalentManager
    {
        private sealed class TalentInfoComparer : IEqualityComparer<TalentInfo>
        {
            public bool Equals(TalentInfo? _x, TalentInfo? _y)
            {
                if (_x == null || _y == null)
                    return false;

                return _x.ProtoName.Equals(_y.ProtoName);
            }

            public int GetHashCode([DisallowNull] TalentInfo _obj) => _obj.ProtoName.GetHashCode();
        }


        static TalentManager()
        {
            s_TalentInfos = new();
        }


        public static void Init()
        {
            if (!File.Exists("talent_name.csv"))
            {
                Console.WriteLine("File 'talent_name.csv' does not exist. This is not an error, but Talents list will show raw string instead.");
                return;
            }
            string[] lines = File.ReadAllLines("talent_name.csv");

            foreach (string line in lines)
            {
                if (line == string.Empty)
                    continue;

                string[] parts = line.Split(',');

                TalentInfo info;
                try
                {
                    info = new()
                    {
                        ProtoName = parts[0],
                        ProtoNameShort = "..." + parts[0][16..],
                        Tier = int.Parse(parts[1]),
                        FriendlyName = parts[2],
                        Description = parts[3],
                    };
                }
                catch (Exception _ex)
                {
                    Console.WriteLine("TalentManager.Init(): " + _ex);
                    continue;
                }

                if (s_TalentInfos.ContainsKey(info.ProtoName))
                {
                    Console.WriteLine("Talent '" + parts[0] + "' has already been loaded.");
                    continue;
                }

                s_TalentInfos[info.ProtoName] = info;
            }
        }

        public static bool TryGetValue(string _key, [NotNullWhen(true)] out TalentInfo? _talentInfo)
            => s_TalentInfos.TryGetValue(_key, out _talentInfo);


        public static readonly Dictionary<string, TalentInfo> s_TalentInfos;
    }

}
