/*  GameClone/CItemAffix.cs
 *  Version 1.0 (2025.05.30)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    // In actual game code, this class derives from CBaseAffix.
    public class CItemAffix : ISaveable
    {
        public void Load(SaveFileReader _reader)
        {
            m_AffixType = _reader.ReadEnum<eItemAffixType>();
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
            switch (m_AffixOperator)
            {
                case eAffixOperator.Add:
                    str += "+";
                    break;
                case eAffixOperator.Multiply:
                    str += "x";
                    break;
                default:
                    str += "[?]";
                    break;
            }
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
    }

}
