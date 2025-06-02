/*  GameClone/CNPE.cs
 *  Version 1.0 (2025.05.29)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CNPE
    {
        public CNPE()
        {
            m_CompletedSteps = new();
        }

        public CNPE(CNPE _other)
        {
            m_CompletedSteps = new(_other.m_CompletedSteps);
        }


        public bool Load(SaveFileReader _reader)
        {
            m_CompletedSteps.Clear();
            int count = _reader.ReadInt32();
            for (int i = 0; i < count; ++i)
            {
                m_CompletedSteps.Add(_reader.ReadEnum<eNPEStep>());
            }

            return true;
        }

        public bool Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_CompletedSteps.Count);
            for (int i = 0; i < m_CompletedSteps.Count; ++i)
            {
                _writer.WriteEnum(m_CompletedSteps[i]);
            }

            return true;
        }


        private readonly List<eNPEStep> m_CompletedSteps = null!;
    }
}
