/*  GameClone/BaseClasses/RewriteSystem/CBaseRewritableEntry.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone.BaseClasses.RewriteSystem
{
    public abstract class CBaseRewritableEntry : ISaveable, IDeepCopyable
    {
        public virtual void Load(SaveFileReader _reader)
        {
            m_Cost = CCurrency.CreateAndLoad(_reader);

            m_IsActive = _reader.ReadBool();
            m_IsAvailable = _reader.ReadBool();
            m_IsRewritten = _reader.ReadBool();

            m_IsNumericEntry = _reader.ReadBool();
            m_NumericEntryMin = _reader.ReadInt32();
            m_NumericEntryMax = _reader.ReadInt32();
        }

        public virtual void Save(SaveFileWriter _writer)
        {
            m_Cost.Save(_writer);

            _writer.WriteBool(m_IsActive);
            _writer.WriteBool(m_IsAvailable);
            _writer.WriteBool(m_IsRewritten);

            _writer.WriteBool(m_IsNumericEntry);
            _writer.WriteInt32(m_NumericEntryMin);
            _writer.WriteInt32(m_NumericEntryMax);
        }

        public virtual void DeepCopyInto(IDeepCopyable _other)
        {
            if (_other is not CBaseRewritableEntry target)
            {
                throw new ArgumentException("_other is not a CBaseRewritableEntry.");
            }

            target.m_Cost = new(m_Cost);

            target.m_IsActive = m_IsActive;
            target.m_IsAvailable = m_IsAvailable;
            target.m_IsRewritten = m_IsRewritten;

            target.m_IsNumericEntry = m_IsNumericEntry;
            target.m_NumericEntryMin = m_NumericEntryMin;
            target.m_NumericEntryMax = m_NumericEntryMax;
        }


        private CCurrency m_Cost = null!;

        private bool m_IsActive = false;
        private bool m_IsAvailable = false;
        private bool m_IsRewritten = false;

        private bool m_IsNumericEntry = false;
        private int m_NumericEntryMin = 0;
        private int m_NumericEntryMax = 0;
    }

}
