/*  GameClone/BaseClasses/RewriteSystem/CBaseRewritableEntry.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone.BaseClasses.RewriteSystem
{
    public abstract class CBaseRewritableEntry : ISaveable, IDeepCloneable
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

        public IDeepCloneable DeepClone()
        {
            var copy = (CBaseRewritableEntry)MemberwiseClone();
            return copy;
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
