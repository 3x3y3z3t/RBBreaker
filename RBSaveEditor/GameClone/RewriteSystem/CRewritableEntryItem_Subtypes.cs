/*  GameClone/RewriteSystem/CRewritableEntryItem_Subtypes.cs
 *  Version 1.0 (2025.06.03)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using RBSaveEditor.GameClone.BaseClasses.RewriteSystem;

namespace RBSaveEditor.GameClone.RewriteSystem
{
    public class CRewritableEntryItemAffixType : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_AffixType = _reader.ReadEnum<eItemAffixType>();
            m_AffixContext = _reader.ReadString();
            m_AffixValue = _reader.ReadFloat();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteEnum(m_AffixType);
            _writer.WriteString(m_AffixContext);
            _writer.WriteFloat(m_AffixValue);
        }


        private eItemAffixType m_AffixType = eItemAffixType.Power;
        private string m_AffixContext = string.Empty;
        private float m_AffixValue = 0.0f;
    }

    public class CRewritableEntryItemAffixValue : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_AffixType = _reader.ReadEnum<eItemAffixType>();
            m_AffixContext = _reader.ReadString();
            m_AffixValue = _reader.ReadFloat();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteEnum(m_AffixType);
            _writer.WriteString(m_AffixContext);
            _writer.WriteFloat(m_AffixValue);
        }


        private eItemAffixType m_AffixType = eItemAffixType.Power;
        private string m_AffixContext = string.Empty;
        private float m_AffixValue = 0.0f;
    }

    public class CRewritableEntryItemRarity : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_Rarity = _reader.ReadEnum<eRarity>();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            _writer.WriteEnum(m_Rarity);
        }


        private eRarity m_Rarity = eRarity.Junk;
    }

    public class CRewritableEntryShipClass : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_ShipClass = _reader.ReadEnum<eShipClass>();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            _writer.WriteEnum(m_ShipClass);
        }


        private eShipClass m_ShipClass = eShipClass.Fighter;
    }

    public class CRewritableEntryRequiredShipClass : CRewritableEntryShipClass
    { }

    public class CRewritableEntryRequiredLevel : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_RequiredLevel = _reader.ReadInt32();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            _writer.WriteInt32(m_RequiredLevel);
        }


        private int m_RequiredLevel = 0;
    }

    public class CRewritableEntryItemUniqueModifierValue : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_UniqueModifierValue = _reader.ReadFloat();
            m_PercentageBased = _reader.ReadBool();
            m_QuantizedUnique = _reader.ReadBool();

            m_RewriteEntryUniqueModifierFormatLocKey = _reader.ReadString();
            m_UniqueModifierStatTypeStr = _reader.ReadString();
            m_UniqueModifierIncreasesStat = _reader.ReadBool();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteFloat(m_UniqueModifierValue);
            _writer.WriteBool(m_PercentageBased);
            _writer.WriteBool(m_QuantizedUnique);

            _writer.WriteString(m_RewriteEntryUniqueModifierFormatLocKey);
            if (m_UniqueModifierStatTypeStr == string.Empty)
            {
                _writer.WriteString(Constants.NullStr);
            } else
            {
                _writer.WriteString(m_UniqueModifierStatTypeStr);
            }
            _writer.WriteBool(m_UniqueModifierIncreasesStat);
        }


        private float m_UniqueModifierValue = 0.0f;
        private bool m_PercentageBased = false;
        private bool m_QuantizedUnique = false;

        public string m_RewriteEntryUniqueModifierFormatLocKey = string.Empty;
        // In actual game code this is a field of type Type
        public string m_UniqueModifierStatTypeStr = string.Empty;
        public bool m_UniqueModifierIncreasesStat = false;
    }

    public class CRewritableEntryItemStatCostValue : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_StatCostValue = _reader.ReadDouble();
            m_StatCostTypeStr = _reader.ReadString();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            _writer.WriteDouble(m_StatCostValue);
            _writer.WriteString(m_StatCostTypeStr);
        }


        public double m_StatCostValue = 0.0;
        public string m_StatCostTypeStr = string.Empty;
    }

    public class CRewritableEntryItemUniqueModifierStat : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_UniqueModifierStatTypeStr = _reader.ReadString();
            m_UniqueModifierValue = _reader.ReadFloat();
            m_PercentageBased = _reader.ReadBool();
            m_QuantizedUnique = _reader.ReadBool();
            m_UniqueModifierIncreasesStat = _reader.ReadBool();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            if (m_UniqueModifierStatTypeStr == string.Empty)
            {
                _writer.WriteString(Constants.NullStr);
            }
            else
            {
                _writer.WriteString(m_UniqueModifierStatTypeStr);
            }
            _writer.WriteFloat(m_UniqueModifierValue);
            _writer.WriteBool(m_PercentageBased);
            _writer.WriteBool(m_QuantizedUnique);
            _writer.WriteBool(m_UniqueModifierIncreasesStat);
        }


        private string m_UniqueModifierStatTypeStr = string.Empty;
        private float m_UniqueModifierValue = 0.0f;
        private bool m_PercentageBased = false;
        private bool m_QuantizedUnique = false;
        private bool m_UniqueModifierIncreasesStat = false;
    }

    public class CRewritableEntryDrawback : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_DrawbackStatus = _reader.ReadEnum<eDrawbackStatus>();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            _writer.WriteEnum(m_DrawbackStatus);
        }


        private eDrawbackStatus m_DrawbackStatus = eDrawbackStatus.Enabled;
    }

    public class CRewritableEntryItemUsable : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_Usable = _reader.ReadBool();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            _writer.WriteBool(m_Usable);
        }


        private bool m_Usable = false;
    }

    public class CRewritableEntryItemEquipmentType : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);
            m_EquipmentType = _reader.ReadEnum<eEquipmentType>();
            m_SpecializationSubtype = _reader.ReadEnum<eSpecializationSubtype>();
            m_IsEnergyWeapon = _reader.ReadBool();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);
            _writer.WriteEnum(m_EquipmentType);
            _writer.WriteEnum(m_SpecializationSubtype);
            _writer.WriteBool(m_IsEnergyWeapon);
        }


        private eEquipmentType m_EquipmentType = eEquipmentType.None;
        private eSpecializationSubtype m_SpecializationSubtype = eSpecializationSubtype.Main;
        private bool m_IsEnergyWeapon = false;
    }

    public class CRewritableEntryItemAffixOperator : CBaseRewritableEntry
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_AffixOperator = _reader.ReadEnum<eAffixOperator>();
            m_AffixType = _reader.ReadEnum<eItemAffixType>();
            m_AffixContext = _reader.ReadString();
            m_AffixValue = _reader.ReadFloat();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteEnum(m_AffixOperator);
            _writer.WriteEnum(m_AffixType);
            _writer.WriteString(m_AffixContext);
            _writer.WriteFloat(m_AffixValue);
        }


        private eAffixOperator m_AffixOperator = eAffixOperator.Add;
        private eItemAffixType m_AffixType = eItemAffixType.Power;
        private string m_AffixContext = string.Empty;
        private float m_AffixValue = 0.0f;
    }



}
