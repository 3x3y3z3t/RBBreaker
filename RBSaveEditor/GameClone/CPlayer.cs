/*  GameClone/CPlayer.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBSaveEditor.ES2Clone;

namespace RBSaveEditor.GameClone
{
    public class CPlayer : CEntity
    {
        private struct CSkillSlotInfo
        {
            public int TierIndex { get; set; }
            public int SkillIndex { get; set; }
        }

        private class CAnalysisInfo : ISaveable
        {
            private enum eState { None, AnalysisInProgress, AnalysisReadyToComplete, FullyAnalyzed }


            public void Load(SaveFileReader _reader)
            {
                m_StartTime = _reader.ReadDateTime();
                m_EndTime = _reader.ReadDateTime();
                m_State = _reader.ReadEnum<eState>();

                string analyzableTypeStr = _reader.ReadString();
                Type? type = Type.GetType(analyzableTypeStr);
                if (type == null)
                {
                    throw new ArgumentException("Invalid Analyzable Type '" + analyzableTypeStr + "'.");
                }
                if (Activator.CreateInstance(type) is not IAnalyzable analyzable)
                {
                    throw new ArgumentException("Type '" + analyzableTypeStr + "' is not an IAnalyzable.");
                }
                m_Analyzable = analyzable;
                //_ = _reader.ReadEnum<eItemType>();
                m_Analyzable.Load(_reader);
            }

            public void Save(SaveFileWriter _writer)
            {
                _writer.WriteDateTime(m_StartTime);
                _writer.WriteDateTime(m_EndTime);
                _writer.WriteEnum(m_State);

                _writer.WriteString(m_Analyzable.GetType().ToString());
                m_Analyzable.Save(_writer);
            }


            private DateTime m_StartTime = DateTime.MinValue;
            private DateTime m_EndTime = DateTime.MinValue;
            private eState m_State = eState.None;

            private IAnalyzable m_Analyzable = null!;
        }


        public CProgressionElementPlayer? ProgressionElement => m_ProgressionElement as CProgressionElementPlayer;

        //private string m_ChosenAptitude = string.Empty;

        //private int m_StashUpgradeLevel = 0;
        public List<CCurrency> Currencies => m_Currencies;


        public CPlayer(bool _isHumanPlayer)
        {
            m_Proto_Name = "CPlayerProto";
            m_IsHumanPlayer = _isHumanPlayer;
        }


        // =====

        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_ChosenAptitude = _reader.ReadString();

            m_StashUpgradeLevel = _reader.ReadInt32();
            {
                int count = _reader.ReadInt32();
                m_Currencies = new(count);
                for (int i = 0; i < count; ++i)
                {
                    CCurrency currency = new();
                    currency.Load(_reader);
                    m_Currencies.Add(currency);
                    // REDUNDANT: In original game code the list is cleared, but then a call to GetCurrency (on the empty list) is made;
                }
            }


            Console.WriteLine("> CPlayer.Load " + _reader.ToString());
            m_RemainingBytes = _reader.ReadToEnd();

            return;

            LoadSkillTree(_reader);

            m_Ship = new();
            m_Ship.Load(_reader);

            m_PilotName = _reader.ReadString();
            m_PilotVariant = _reader.ReadInt32();

            if (m_IsHumanPlayer)
            {
                LoadInfoSpecificToHumanPlayer(_reader);
            }

            {
                int count = _reader.ReadInt32();
                m_AnalysisInfos = new(count);
                for (int i = 0; i < count; ++i)
                {
                    CAnalysisInfo info = new();
                    info.Load(_reader);
                    m_AnalysisInfos.Add(info);
                    //if (version >= 81)
                    //{
                    //    this.FixupAnalysisPostLoad(canalysisInfo);
                    //}
                }
            }

            m_DifficultyIndex = _reader.ReadInt32();
            m_LowestDifficultyIndex = _reader.ReadInt32();
            m_IsIronmanModeEnabled = _reader.ReadBool();
            m_IsIronmanModeLost = _reader.ReadBool();
            m_MaxAchievedMissionChallengeLevelIndex = _reader.ReadInt32();

            m_IsErasedFromReality = _reader.ReadBool();
            m_FractureMissionElapsedTime = _reader.ReadFloat();
            m_FractureMultiplier = _reader.ReadFloat();

            m_ElapsedTimeTotal = _reader.ReadFloat();
            m_ElapsedTimeCurrent = _reader.ReadFloat();

            m_TotalNumBreaks = _reader.ReadUInt32();
            m_NumExpertGoalsCompleted = _reader.ReadUInt32();

            m_LootFilterDisallowedRarities = _reader.ReadHashSet<eRarity>();
            {
                int count = _reader.ReadInt32();
                m_LootFilterDisallowedEquipmentTypes = new(count);
                for (int i = 0; i < count; ++i)
                {
                    eEquipmentType type = _reader.ReadEnum<eEquipmentType>();
                    eSpecializationSubtype subtype = _reader.ReadEnum<eSpecializationSubtype>();
                    m_LootFilterDisallowedEquipmentTypes.Add(new ValueTuple<eEquipmentType, eSpecializationSubtype>(type, subtype));
                }
            }
            {
                int count = _reader.ReadInt32();
                m_LootFilterRequiredAffixes = new(count);
                for (int i = 0; i < count; ++i)
                {
                    eItemAffixType type = _reader.ReadEnum<eItemAffixType>();
                    string name = _reader.ReadString();
                    m_LootFilterRequiredAffixes.Add(new ValueTuple<eItemAffixType, string>(type, name));
                }
            }
            m_LootFilterRequireRewritable = _reader.ReadBool();
            m_LootFilterCodexBypassEnabled = _reader.ReadBool();
            m_LootFilterRarityRewriteBypassEnabled = _reader.ReadBool();
            m_LootFilterAffixTypeRewriteBypassEnabled = _reader.ReadBool();
            m_LootFilterUseAndOperatorForAffixTypes_NYI = _reader.ReadBool();

            m_ExpertGoalTrackers = _reader.ReadDictionary<eExpertGoalTracker, int>();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteString(m_ChosenAptitude);

            _writer.WriteInt32(m_StashUpgradeLevel);
            _writer.WriteInt32(m_Currencies.Count);
            foreach (CCurrency currency in m_Currencies)
            {
                currency.Save(_writer);
            }

            Console.WriteLine("> CPlayer.Save " + _writer.ToString());

            _writer.WriteBytes(m_RemainingBytes);
            return;

            SaveSkillTree(_writer);

            m_Ship.Save(_writer);

            _writer.WriteString(m_PilotName);
            _writer.WriteInt32(m_PilotVariant);

            if (m_IsHumanPlayer)
            {
                SaveInfoSpecificToHumanPlayer(_writer);
            }

            _writer.WriteInt32(m_AnalysisInfos.Count);
            foreach (CAnalysisInfo info in m_AnalysisInfos)
            {
                info.Save(_writer);
            }

            _writer.WriteInt32(m_DifficultyIndex);
            _writer.WriteInt32(m_LowestDifficultyIndex);
            _writer.WriteBool(m_IsIronmanModeEnabled);
            _writer.WriteBool(m_IsIronmanModeLost);
            _writer.WriteInt32(m_MaxAchievedMissionChallengeLevelIndex);

            _writer.WriteBool(m_IsErasedFromReality);
            _writer.WriteFloat(m_FractureMissionElapsedTime);
            _writer.WriteFloat(m_FractureMultiplier);

            _writer.WriteFloat(m_ElapsedTimeTotal);
            _writer.WriteFloat(m_ElapsedTimeCurrent);

            _writer.WriteUInt32(m_TotalNumBreaks);
            _writer.WriteUInt32(m_NumExpertGoalsCompleted);

            _writer.WriteHashSet(m_LootFilterDisallowedRarities);
            _writer.WriteInt32(m_LootFilterDisallowedEquipmentTypes.Count);
            foreach (ValueTuple<eEquipmentType, eSpecializationSubtype> tuple in m_LootFilterDisallowedEquipmentTypes)
            {
                _writer.WriteEnum(tuple.Item1);
                _writer.WriteEnum(tuple.Item2);
            }
            _writer.WriteInt32(m_LootFilterRequiredAffixes.Count);
            foreach (ValueTuple<eItemAffixType, string> tuple in m_LootFilterRequiredAffixes)
            {
                _writer.WriteEnum(tuple.Item1);
                _writer.WriteString(tuple.Item2);
            }
            _writer.WriteBool(m_LootFilterRequireRewritable);
            _writer.WriteBool(m_LootFilterCodexBypassEnabled);
            _writer.WriteBool(m_LootFilterRarityRewriteBypassEnabled);
            _writer.WriteBool(m_LootFilterAffixTypeRewriteBypassEnabled);
            _writer.WriteBool(m_LootFilterUseAndOperatorForAffixTypes_NYI);

            _writer.WriteDictionary(m_ExpertGoalTrackers);
        }


        private void LoadSkillTree(SaveFileReader _reader)
        {
            int count = _reader.ReadInt32();
            m_Skills = new(count);
            for (int i = 0; i < count; ++i)
            {
                CSkill? skill = CSkill.CreateAndLoad(this, _reader);

                if (skill == null)
                {
                    Console.WriteLine("Skill is null (this should not happen).");
                    continue;
                }

                //CSkillProto? proto = skill.Proto;
                //if (proto != null && proto.Deprecated)
                //    continue;
                if (skill.Proto_Name != string.Empty && skill.Proto_Deprecated)
                    continue;

                //int skillIndex = GetSkillIndex(skill.Proto);
                int skillIndex = GetSkillIndex(skill.Proto_Name);
                if (skillIndex >= 0)
                {
                    m_Skills[skillIndex] = skill;
                }
                else
                {
                    m_Skills.Add(skill);
                }
            }
        }

        private void SaveSkillTree(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_Skills.Count);
            foreach (CSkill skill in m_Skills)
            {
                skill.Save(_writer);
            }
        }

        private void LoadInfoSpecificToHumanPlayer(SaveFileReader _reader)
        {
            m_MissionManager = new(this);
            m_MissionManager.Load(_reader);

            m_LastStationMapProtoName = _reader.ReadString();

            {
                int count = _reader.ReadInt32();

                for (int i = 0; i < count; ++i)
                {
                    int slotIndex = _reader.ReadInt32();
                    int tierIndex = _reader.ReadInt32();
                    int skillIndex = _reader.ReadInt32();

                    //CSkillTier tier = this.GetPlayerClass().GetTier(tierIndex);
                    //CSkill skill = this.GetSkill((tier != null) ? tier.GetSkillProto(skillIndex) : null);
                    //if (((skill != null) ? skill.Level : 0) <= 0)
                    //{
                    //    tierIndex = -1;
                    //    skillIndex = -1;
                    //}

                    CSkillSlotInfo slotInfo = new()
                    {
                        TierIndex = tierIndex,
                        SkillIndex = skillIndex,
                    };
                    if (m_SkillSlotInfos.ContainsKey(slotIndex))
                    {
                        m_SkillSlotInfos[slotIndex] = slotInfo;
                    }
                    else
                    {
                        m_SkillSlotInfos.Add(slotIndex, slotInfo);
                    }
                }
            }

            //double metagameModifiedStatValue = this.GetMetagameModifiedStatValue<CStatPlayerShipLoadoutStatBoostProto>();
            //m_LoadoutManager = new();
            //m_LoadoutManager.Load(_reader);
            //m_LoadoutManager.PostLoadItemFixup(m_Ship, metagameModifiedStatValue);

            m_FractureMissionDeferredLaunchMode = _reader.ReadString();
        }

        private void SaveInfoSpecificToHumanPlayer(SaveFileWriter _writer)
        {
            m_MissionManager.Save(_writer);

            _writer.WriteString(m_LastStationMapProtoName);

            _writer.WriteInt32(m_SkillSlotInfos.Count);
            foreach (KeyValuePair<int, CSkillSlotInfo> pair in m_SkillSlotInfos)
            {
                _writer.WriteInt32(pair.Key);
                _writer.WriteInt32(pair.Value.TierIndex);
                _writer.WriteInt32(pair.Value.SkillIndex);
            }

            //m_LoadoutManager.Save(_writer);

            _writer.WriteString(m_FractureMissionDeferredLaunchMode);
        }

        private int GetSkillIndex(string _protoName)
        {
            if (_protoName == string.Empty)
                return -1;

            for (int i = 0; i < m_Skills.Count; ++i)
            {
                if (m_Skills[i].Proto_Name == _protoName)
                    return i;
            }

            return -1;
        }







        private string m_ChosenAptitude = string.Empty;

        private int m_StashUpgradeLevel = 0;
        private List<CCurrency> m_Currencies = null!;

        private List<CSkill> m_Skills = null!;

        private CShip m_Ship = null!;

        private string m_PilotName = string.Empty;
        private int m_PilotVariant = -1;

        // Human Player specific
        private CMissionManager m_MissionManager = null!;

        private string m_LastStationMapProtoName = string.Empty;

        private Dictionary<int, CSkillSlotInfo> m_SkillSlotInfos = null!;

        //private CLoadoutManager m_LoadoutManager = null!;

        private string m_FractureMissionDeferredLaunchMode = string.Empty;
        // Human Player specific end

        private List<CAnalysisInfo> m_AnalysisInfos = null!;

        private int m_DifficultyIndex = -1;
        private int m_LowestDifficultyIndex = -1;
        private bool m_IsIronmanModeEnabled = false;
        private bool m_IsIronmanModeLost = false;
        private int m_MaxAchievedMissionChallengeLevelIndex = -1;

        private bool m_IsErasedFromReality = false;
        private float m_FractureMissionElapsedTime = 0.0f;
        private float m_FractureMultiplier = 0.0f;

        private float m_ElapsedTimeTotal = 0.0f;
        private float m_ElapsedTimeCurrent = 0.0f;

        private uint m_TotalNumBreaks = 0U;
        private uint m_NumExpertGoalsCompleted = 0U;

        private HashSet<eRarity> m_LootFilterDisallowedRarities = null!;
        private HashSet<ValueTuple<eEquipmentType, eSpecializationSubtype>> m_LootFilterDisallowedEquipmentTypes = null!;
        private HashSet<ValueTuple<eItemAffixType, string>> m_LootFilterRequiredAffixes = null!;

        private bool m_LootFilterRequireRewritable = false;
        private bool m_LootFilterCodexBypassEnabled = false;
        private bool m_LootFilterRarityRewriteBypassEnabled = false;
        private bool m_LootFilterAffixTypeRewriteBypassEnabled = false;
        private bool m_LootFilterUseAndOperatorForAffixTypes_NYI = false;

        private Dictionary<eExpertGoalTracker, int> m_ExpertGoalTrackers = null!;


        private bool m_IsHumanPlayer = true;


        private byte[] m_RemainingBytes = null!;
    }

}
