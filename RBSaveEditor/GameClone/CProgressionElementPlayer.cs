/*  GameClone/CProgressionElementPlayer.cs
 *  Version 1.0 (2025.06.01)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using RBSaveEditor.GameClone.Items;

namespace RBSaveEditor.GameClone
{
    public class CProgressionElementPlayer : CProgressionElement
    {
        public override void Load(SaveFileReader _reader)
        {
            {
                int count = _reader.ReadInt32();
                m_FulfilledCriteria = new(count);
                for (int i = 0; i < count; ++i)
                {
                    m_FulfilledCriteria.Add(_reader.ReadEnum<eUnlockableCriterion>());
                }
            }
            {
                int count = _reader.ReadInt32();
                m_FulfilledCriteriaAfterBreak = new(count);
                for (int i = 0; i < count; ++i)
                {
                    m_FulfilledCriteriaAfterBreak.Add(_reader.ReadEnum<eUnlockableCriterion>());
                }
            }

            {
                int count = _reader.ReadInt32();
                m_CompletedHorizonUpgrades = new(count);
                for (int i = 0; i < count; ++i)
                {
                    m_CompletedHorizonUpgrades.Add(_reader.ReadString());
                }
            }
            {
                int count = _reader.ReadInt32();
                m_HorizonUpgradeBuildInfos = new(count);
                for (int i = 0; i < count; ++i)
                {
                    CHorizonUpgradeBuildInfo buildInfo = new();
                    buildInfo.Load(_reader);
                    m_HorizonUpgradeBuildInfos.Add(buildInfo);
                }
            }

            m_UnlockedEntityProtoUpgrades = _reader.ReadList<string>();
            m_HasSeenStoryProtos = _reader.ReadList<string>();
            m_HasSeenBarkProtos = _reader.ReadHashSet<string>();

            {
                int count = _reader.ReadInt32();
                m_ItemSets = new(count);
                for (int i = 0; i < count; ++i)
                {
                    string name = _reader.ReadString();
                    CItemSet itemSet = new(name);
                    itemSet.Load(_reader);

                    if (itemSet.Proto_IsValid)
                    {
                        m_ItemSets.Add(itemSet);
                    }
                }
            }

            {
                int count = _reader.ReadInt32();
                m_SpecialistUpgrades = new(count);
                for (int i = 0; i < count; ++i)
                {
                    string name = _reader.ReadString();
                    CSpecialistUpgrade upgrade = new(name);
                    upgrade.Load(_reader);

                    if (upgrade.Proto_IsValid)
                    {
                        m_SpecialistUpgrades.Add(upgrade);
                    }
                }

            }

            m_TotalAcquiredSkillPoints = _reader.ReadInt32();
            m_TotalSpentSkillPoints = _reader.ReadInt32();

            base.Load(_reader);
        }

        public override void Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_FulfilledCriteria.Count);
            foreach (var criteria  in m_FulfilledCriteria)
            {
                _writer.WriteEnum(criteria);
            }
            _writer.WriteInt32(m_FulfilledCriteriaAfterBreak.Count);
            foreach (var criteria in m_FulfilledCriteriaAfterBreak)
            {
                _writer.WriteEnum(criteria);
            }

            _writer.WriteInt32(m_CompletedHorizonUpgrades.Count);
            foreach (var upgrade in m_CompletedHorizonUpgrades)
            {
                _writer.WriteString(upgrade);
            }
            _writer.WriteInt32(m_HorizonUpgradeBuildInfos.Count);
            foreach(var buildInfo in m_HorizonUpgradeBuildInfos)
            {
                buildInfo.Save(_writer);
            }

            _writer.WriteList(m_UnlockedEntityProtoUpgrades);
            _writer.WriteList(m_HasSeenStoryProtos);
            _writer.WriteHashSet(m_HasSeenBarkProtos);

            _writer.WriteInt32(m_ItemSets.Count);
            foreach (var itemSet in m_ItemSets)
            {
                _writer.WriteString(itemSet.Proto_Name);
                itemSet.Save(_writer);
            }

            _writer.WriteInt32(m_SpecialistUpgrades.Count);
            foreach (var upgrade in m_SpecialistUpgrades)
            {
                _writer.WriteString(upgrade.Proto_Name);
                upgrade.Save(_writer);
            }

            _writer.WriteInt32(m_TotalAcquiredSkillPoints);
            _writer.WriteInt32(m_TotalSpentSkillPoints);

            base.Save(_writer);
        }

        public /*override*/ double GetXPToNextLevel(int _level = -1)
        {
            if (_level < 0)
                _level = m_Level;
            // round( (1.5 ^ (lvl - 1)) * 100 / 10 ) * 10;
            return Math.Round((double)(100f * Math.Pow(1.5f, _level - 1) / 10f)) * 10.0;
        }




        private List<eUnlockableCriterion> m_FulfilledCriteria = null!;
        private List<eUnlockableCriterion> m_FulfilledCriteriaAfterBreak = null!;

        private List<string> m_CompletedHorizonUpgrades = null!;
        private List<CHorizonUpgradeBuildInfo> m_HorizonUpgradeBuildInfos = null!;

        private List<string> m_UnlockedEntityProtoUpgrades = null!;
        private List<string> m_HasSeenStoryProtos = null!;
        private HashSet<string> m_HasSeenBarkProtos = null!;

        private HashSet<CItemSet> m_ItemSets = null!;

        private HashSet<CSpecialistUpgrade> m_SpecialistUpgrades = null!;

        private int m_TotalAcquiredSkillPoints = 0;
        private int m_TotalSpentSkillPoints = 0;


    }

}
