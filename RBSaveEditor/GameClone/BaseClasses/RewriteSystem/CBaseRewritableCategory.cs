/*  GameClone/BaseClasses/RewriteSystem/CBaseRewritableCategory.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone.BaseClasses.RewriteSystem
{
    public abstract class CBaseRewritableCategory : ISaveable, IDeepCloneable
    {
        public virtual void Load(SaveFileReader _reader)
        {
            m_CategoryIndex = _reader.ReadInt32();

            {
                int count = _reader.ReadInt32();
                m_RewritableEntries = new(count);
                for (int i = 0; i < count; ++i)
                {
                    CBaseRewritableEntry entry = CreateRewritableEntry();
                    entry.Load(_reader);
                    m_RewritableEntries.Add(entry);
                }
            }

            m_EntriesStartingOffset = _reader.ReadInt32();

            m_CumulativeRewrites = _reader.ReadBool();
        }

        public virtual void Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_CategoryIndex);

            _writer.WriteInt32(m_RewritableEntries.Count);
            foreach(CBaseRewritableEntry entry in m_RewritableEntries)
            {
                entry.Save(_writer);
            }

            _writer.WriteInt32(m_EntriesStartingOffset);

            _writer.WriteBool(m_CumulativeRewrites);
        }

        public virtual IDeepCloneable DeepClone()
        {
            var copy = (CBaseRewritableCategory)MemberwiseClone();

            copy.m_RewritableEntries = m_RewritableEntries.DeepClone();
            return copy;
        }


        public abstract CBaseRewritableEntry CreateRewritableEntry();


        private int m_CategoryIndex = -1;

        private List<CBaseRewritableEntry> m_RewritableEntries = null!;

        private int m_EntriesStartingOffset = -1;

        private bool m_CumulativeRewrites = false;
    }

}
