/*  GameClone/CItemAffix.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    // In actual game code, this class derives from CBaseAffix.
    public class CItemAffix : ISaveable
    {
        public eItemAffixGroup AffixGroup => m_AffixGroup;

        public eItemAffixType AffixType { get => m_AffixType; set => m_AffixType = value; }
        public float AffixValue { get => m_AffixValue; set => m_AffixValue = value; }
        public eAffixOperator AffixOperator { get => m_AffixOperator; set => m_AffixOperator = value; }

        public bool IsSpecial { get => m_IsSpecial; set => m_IsSpecial = value; }
        public bool IsCrafted { get => m_IsCrafted; set => m_IsCrafted = value; }
        public int NumTimeBoosted { get => m_NumTimesBoosted; set => m_NumTimesBoosted = value; }


        public void Load(SaveFileReader _reader)
        {
            m_AffixType = _reader.ReadEnum<eItemAffixType>();
            m_AffixGroup = m_AffixType.GetAffixGroup();
            m_AffixContext = _reader.ReadString();
            if (m_AffixContext == string.Empty)
                m_AffixContext = null;
            m_AffixValue = _reader.ReadFloat();
            m_AffixOperator = _reader.ReadEnum<eAffixOperator>();

            m_IsSpecial = _reader.ReadBool();
            m_IsCrafted = _reader.ReadBool();

            m_NumTimesBoosted = _reader.ReadInt32();
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteEnum(m_AffixType);
            if (m_AffixContext == null)
            {
                _writer.WriteString(string.Empty);
            } else
            {
                _writer.WriteString(m_AffixContext);
            }
            _writer.WriteFloat(m_AffixValue);
            _writer.WriteEnum(m_AffixOperator);

            _writer.WriteBool(m_IsSpecial);
            _writer.WriteBool(m_IsCrafted);

            _writer.WriteInt32(m_NumTimesBoosted);
        }

        public override string ToString()
        {
            string str = "";
            str += m_AffixOperator.GetAffixOperatorSymbol();
            switch (m_AffixType)
            {
                case eItemAffixType.CriticalDamage:
                case eItemAffixType.MissionTargetDamage:
                    str += (m_AffixValue * 100.0f).ToString("0") + "%";
                    break;
                default:
                    str += m_AffixValue;
                    break;
            }
            str += " " + m_AffixType.ToString();

            return str;
        }


        private eItemAffixType m_AffixType = eItemAffixType.Power;
        private string? m_AffixContext = null;
        private float m_AffixValue = 0.0f;
        private eAffixOperator m_AffixOperator = eAffixOperator.Add;

        private bool m_IsSpecial = false;
        private bool m_IsCrafted = false;
        private int m_NumTimesBoosted = 0;


        private eItemAffixGroup m_AffixGroup = eItemAffixGroup.Primary;
    }

}
