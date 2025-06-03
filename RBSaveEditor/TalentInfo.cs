/*  TalentInfo.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RBSaveEditor
{
    public class TalentInfo
    {
        public readonly string ProtoName = null!;
        public readonly string ProtoNameShort = null!;
        public readonly int Tier = -1;

        public readonly string FriendlyName = string.Empty;
        public readonly string Description = string.Empty;


        public TalentInfo(string _protoNameShort, int _tier = -1, string _friendlyName = "", string _description = "")
        {
            ProtoName = c_Prefix + _protoNameShort;
            ProtoNameShort = _protoNameShort;
            Tier = _tier;

            FriendlyName = _friendlyName;
            Description = _description;
        }


        public static string GetFullProtoName(string _protoNameShort)
        {
            if (_protoNameShort.StartsWith(c_Prefix))
                return _protoNameShort;

            return "Metagame-Talent-" + _protoNameShort;
        }

        private const string c_Prefix = "Metagame-Talent-";
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
            s_TalentInfos = new()
            {
                { "DialogueRewriting", new("DialogueRewriting", -1, "Booming Reformulation", "[Story Talent] Unlock the ability to Rewrite Story dialogue.") },
                { "StabilizeFateCore", new("StabilizeFateCore", 0, "Stabilize Fate Core", "Stabilize Fate Core (first talent ever).") },
            };

            string str = "Metagame-Talent-FateCoreCheaperRewrites, 0, ,\r\nMetagame-Talent-FocusAbilityGeneratesPulses, 0, ,\r\n\r\nMetagame-Talent-UnlockExpertGoals, 0, Expertise, Enable Expert Goal for mission.\r\nMetagame-Talent-IncreasedExpertGoalTimer, 0, Expedience, Expert Goal time limit +5s per level.\r\nMetagame-Talent-IncreasedExpertGoalReward, 0, ,\r\n\r\nMetagame-Talent-UnlockSkillRewriting, 1, Vibrant Reverie, Unlock Rewriting Character Skill.\r\nMetagame-Talent-CreditArmor1-Sub2, 1, , \r\n\r\nMetagame-Talent-DissolveItems, 0, Dissolve Items, Enable Dissolve Items (sell item on the fly).\r\n\r\nMetagame-Talent-ShipHealth1, 0, Subatomic Fortification, Increase Health by 200 per level.\r\nMetagame-Talent-ShipHealthRegen1, 0, Invigoration, Increase Health Regen by 4 per level.\r\n\r\nMetagame-Talent-LootFilter, 0, Discernment Aura, Enable Loot Filter.\r\n\r\nMetagame-Talent-FateCap1, 0, Fate Pulse Specialization, +2 Fate Pulse cap per level.\r\n\r\nMetagame-Talent-MoreRealityPointsFromLevel1, 0, Galactic Attunement, Base Fate Point = Character Level / 4.\r\n\r\nMetagame-Talent-UnlockDifficultyLevels, 0, Omen, Unlock Omen (global difficulty).";
        }


        public static void Init()
        {
            const string resourceName = "talent_name.csv";
            var assembly = Assembly.GetExecutingAssembly();

            string str = string.Empty;
            using (Stream? stream = assembly.GetManifestResourceStream("RBSaveEditor." + resourceName))
            {
                if (stream == null)
                {
                    Console.WriteLine("Embed Resource '" + resourceName + "' not found. This is not an error, but Talents list will show raw string instead." +
                        "\n    (Though please let me know if you see this message.)");
                    return;
                }

                using StreamReader reader = new(stream);
                str = reader.ReadToEnd();
            }

            if (str == string.Empty)
                return;

            // =====;
            string[] lines = str.Split("\r\n");
            foreach (string line in lines)
            {
                if (line == string.Empty)
                    continue;

                string[] parts = line.Split(',');

                TalentInfo info;
                try
                {
                    info = new(parts[0], int.Parse(parts[1]), parts[2], parts[3]);
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
