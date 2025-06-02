/*  GameClone/CMapProgress.cs
 *  Version 1.0 (2025.06.02)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CMapProgress : ISaveable, IDeepCloneable
    {
        public void Load(SaveFileReader _reader)
        {
            m_MapProtoName = _reader.ReadString();

            m_Unlocked = _reader.ReadBool();
            m_SawUnlockMapHubVisuals = _reader.ReadBool();
            m_Won = _reader.ReadBool();
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteString(m_MapProtoName);

            _writer.WriteBool(m_Unlocked);
            _writer.WriteBool(m_SawUnlockMapHubVisuals);
            _writer.WriteBool(m_Won);
        }

        public IDeepCloneable DeepClone()
        {
            var copy = (CMapProgress)MemberwiseClone();
            return copy;
        }


        private string m_MapProtoName = string.Empty;

        private bool m_Unlocked = false;
        private bool m_SawUnlockMapHubVisuals = false;
        private bool m_Won = false;
    }

}
