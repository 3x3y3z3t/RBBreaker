/*  GameClone/CMetagame.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.Diagnostics.Contracts;
using System.Text;

namespace RBSaveEditor.GameClone
{
    public class CMetagame : IDeepCloneable
    {


        public static char FunnyChar { get => s_FunnyChar; set => s_FunnyChar = value; }


        public int VersionNumber => m_VersionNumber;
        public DateTime SaveDateTime => m_SaveDateTime;

        public bool Unlocked => m_Unlocked;
        public uint TalentPoints { get => m_TalentPoints; set => m_TalentPoints = value; }
        public Dictionary<string, uint> TalentLevels => m_TalentLevels;
        public Dictionary<int, uint> TierLevels => m_TierLevels;

        public HashSet<eMetagameNPEStep> CompletedMetagameNPESteps => m_CompletedMetagameNPESteps;
        public HashSet<int> CompletedTimelines => m_CompletedTimelines;
        public uint NumTimesRealityBroken { get => m_NumTimesRealityBroken; set => m_NumTimesRealityBroken = value; }

        public Dictionary<string, bool[,,]> DialogueSeenInfosMap => m_DialogueSeenInfosMap;

        public HashSet<string>? EverEncounteredEnemies => m_EverEncounteredEnemies;
        public HashSet<eUnlockableCriterion> CriteriaEverFulfilled => m_CriteriaEverFulfilled;

        public HashSet<CUnlockedAptitudeInfo> UnlockedAptitudeInfos => m_UnlockedAptitudeInfos;
        public HashSet<eMetagameFlags> AdditionalSetFlags => m_AdditionalSetFlags;

        public bool CallAttentionToEntityRewritingTalent { get => m_CallAttentionToEntityRewritingTalent; set => m_CallAttentionToEntityRewritingTalent = value; }
        public bool CallAttentionToDialogueRewritingTalent { get => m_CallAttentionToDialogueRewritingTalent; set => m_CallAttentionToDialogueRewritingTalent = value; }
        public HashSet<string> CallAttentionToNewAptitudes => m_CallAttentionToNewAptitudes;

        //public bool ProcessedDemoImport { get => m_ProcessedDemoImport; set => m_ProcessedDemoImport = value; }
        //public uint DemoMigratedRP { get => m_DemoMigratedRP; set => m_DemoMigratedRP = value; }
        //public HashSet<CUnlockedAptitudeInfo> DemoMigratedAptitudes => m_DemoMigratedAptitudes;

        //public string OverlordEntityProtoName { get => m_OverlordEntityProtoName; set => m_OverlordEntityProtoName = value; }
        //public double OverlordHealthFraction { get => m_OverlordHealthFraction; set => m_OverlordHealthFraction = value; }
        //public int OverlordLevel { get => m_OverlordLevel; set => m_OverlordLevel = value; }

        //public CNPE NPE => m_NPE;


        public bool Load(SaveFileReader _reader)
        {
            m_VersionNumber = _reader.ReadInt32();
            m_SaveDateTime = DateTime.FromBinary(_reader.ReadInt64());

            m_Unlocked = _reader.ReadBool();
            m_TalentPoints = _reader.ReadUInt32();
            m_TalentLevels = _reader.ReadDictionary<string, uint>();
            m_TierLevels = _reader.ReadDictionary<int, uint>();

            m_CompletedMetagameNPESteps = _reader.ReadHashSet<eMetagameNPEStep>();
            m_CompletedTimelines = _reader.ReadHashSet<int>();
            m_NumTimesRealityBroken = _reader.ReadUInt32();

            {
                int count = _reader.ReadInt32();
                m_DialogueSeenInfosMap = new(count);
                for (int i = 0; i < count; ++i)
                {
                    string key = _reader.ReadString();
                    bool[,,] array = Local_Read3DBoolArrayWrittenAsString(_reader);

                    m_DialogueSeenInfosMap.Add(key, array);
                }
            }

            {
                bool hasValue = _reader.ReadBool();
                if (hasValue)
                {
                    m_EverEncounteredEnemies = _reader.ReadHashSet<string>();
                }
            }
            m_CriteriaEverFulfilled = _reader.ReadHashSet<eUnlockableCriterion>();

            m_UnlockedAptitudeInfos = _reader.ReadHashSet<CUnlockedAptitudeInfo>();
            m_AdditionalSetFlags = _reader.ReadHashSet<eMetagameFlags>();

            m_CallAttentionToEntityRewritingTalent = _reader.ReadBool();
            m_CallAttentionToDialogueRewritingTalent = _reader.ReadBool();
            m_CallAttentionToNewAptitudes = _reader.ReadHashSet<string>();

            m_ProcessedDemoImport = _reader.ReadBool();
            m_DemoMigratedRP = _reader.ReadUInt32();
            m_DemoMigratedAptitudes = _reader.ReadHashSet<CUnlockedAptitudeInfo>();

            m_OverlordEntityProtoName = _reader.ReadString();
            m_OverlordHealthFraction = _reader.ReadDouble();
            m_OverlordLevel = _reader.ReadInt32();

            m_NPE = new();
            _ = m_NPE.Load(_reader);

            m_RemainingBytes = _reader.ReadToEnd();

            return true;


            static bool[,,] Local_Read3DBoolArrayWrittenAsString(SaveFileReader _reader)
            {
                int lengthX = _reader.ReadInt32();
                int lengthY = _reader.ReadInt32();
                int lengthZ = _reader.ReadInt32();

                string arrayStr = _reader.ReadString();
                int index = 0;

                bool[,,] array = new bool[lengthX, lengthY, lengthZ];
                for (int x = 0; x < lengthX; ++x)
                {
                    for (int y = 0; y < lengthY; ++y)
                    {
                        for (int z = 0; z < lengthZ; ++z)
                        {
                            if (arrayStr[index] != '1')
                                ++UnusedBytes;

                            array[x, y, z] = arrayStr[index++] == '1';
                        }
                    }
                }

                return array;
            }
        }

        public static int UnusedBytes = 0;

        public bool Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_VersionNumber);
            _writer.WriteInt64(m_SaveDateTime.ToBinary());

            _writer.WriteBool(m_Unlocked);
            _writer.WriteUInt32(m_TalentPoints);
            _writer.WriteDictionary(m_TalentLevels);
            _writer.WriteDictionary(m_TierLevels);

            _writer.WriteHashSet(m_CompletedMetagameNPESteps);
            _writer.WriteHashSet(m_CompletedTimelines);
            _writer.WriteUInt32(m_NumTimesRealityBroken);

            {
                _writer.WriteInt32(m_DialogueSeenInfosMap.Count);
                foreach (var pair in m_DialogueSeenInfosMap)
                {
                    _writer.WriteString(pair.Key);
                    Local_Write3DBoolArrayAsString(_writer, pair.Value);
                }
            }

            {
                _writer.WriteBool(m_EverEncounteredEnemies != null);
                if (m_EverEncounteredEnemies != null)
                {
                    _writer.WriteHashSet(m_EverEncounteredEnemies);
                }
            }
            _writer.WriteHashSet(m_CriteriaEverFulfilled);

            _writer.WriteHashSet(m_UnlockedAptitudeInfos);
            _writer.WriteHashSet(m_AdditionalSetFlags);

            _writer.WriteBool(m_CallAttentionToEntityRewritingTalent);
            _writer.WriteBool(m_CallAttentionToDialogueRewritingTalent);
            _writer.WriteHashSet(m_CallAttentionToNewAptitudes);

            _writer.WriteBool(m_ProcessedDemoImport);
            _writer.WriteUInt32(m_DemoMigratedRP);
            _writer.WriteHashSet(m_DemoMigratedAptitudes);

            _writer.WriteString(m_OverlordEntityProtoName);
            _writer.WriteDouble(m_OverlordHealthFraction);
            _writer.WriteInt32(m_OverlordLevel);

            m_NPE.Save(_writer);

            _writer.WriteBytes(m_RemainingBytes);

            return true;


            static void Local_Write3DBoolArrayAsString(SaveFileWriter _writer, bool[,,] _array)
            {
                int lengthX = _array.GetLength(0);
                int lengthY = _array.GetLength(1);
                int lengthZ = _array.GetLength(2);

                _writer.WriteInt32(lengthX);
                _writer.WriteInt32(lengthY);
                _writer.WriteInt32(lengthZ);

                StringBuilder sb = new(lengthX * lengthY * lengthZ);
                byte b = 0;
                int index = 0;
                for (int x = 0; x < lengthX; ++x)
                {
                    for (int y = 0; y < lengthY; ++y)
                    {
                        for (int z = 0; z < lengthZ; ++z)
                        {
                            //b |= Convert.ToByte(_array[x, y, z]);
                            //if (index <= 7)
                            //{
                            //    b <<= 1;
                            //    ++index;
                            //}
                            //else
                            //{
                            //    _writer.WriteByte(b);
                            //    index = 0;
                            //}

                            sb.Append(_array[x, y, z] ? '1' : s_FunnyChar);
                        }
                    }
                }
                _writer.WriteString(sb.ToString());
            }
        }

        public IDeepCloneable DeepClone()
        {
            var copy = (CMetagame)MemberwiseClone();

            copy.m_UnlockedAptitudeInfos = m_UnlockedAptitudeInfos.DeepClone();
            copy.m_DemoMigratedAptitudes = m_DemoMigratedAptitudes.DeepClone();
            copy.m_NPE = (CNPE)m_NPE.DeepClone();

            return copy;
        }


        private int m_VersionNumber = CGame.CURRENT_VERSION_NUMBER;
        private DateTime m_SaveDateTime;

        private bool m_Unlocked = false;
        private uint m_TalentPoints = 0U;
        private Dictionary<string, uint> m_TalentLevels = null!;
        private Dictionary<int, uint> m_TierLevels = null!;
        private HashSet<eMetagameNPEStep> m_CompletedMetagameNPESteps = null!;
        private HashSet<int> m_CompletedTimelines = null!;
        private uint m_NumTimesRealityBroken = 0U;
        private Dictionary<string, bool[,,]> m_DialogueSeenInfosMap = null!;

        private HashSet<string>? m_EverEncounteredEnemies = null;
        private HashSet<eUnlockableCriterion> m_CriteriaEverFulfilled = null!;

        private HashSet<CUnlockedAptitudeInfo> m_UnlockedAptitudeInfos = null!;
        private HashSet<eMetagameFlags> m_AdditionalSetFlags = null!;

        private bool m_CallAttentionToEntityRewritingTalent = false;
        private bool m_CallAttentionToDialogueRewritingTalent = false;
        private HashSet<string> m_CallAttentionToNewAptitudes = null!;

        private bool m_ProcessedDemoImport = false;
        private uint m_DemoMigratedRP = 0U;
        private HashSet<CUnlockedAptitudeInfo> m_DemoMigratedAptitudes = null!;

        private string m_OverlordEntityProtoName = string.Empty;
        private double m_OverlordHealthFraction = 0.0;
        private int m_OverlordLevel = 0;

        private CNPE m_NPE = null!;

        private static char s_FunnyChar = '0';


        private byte[] m_RemainingBytes = null!;
    }

}
