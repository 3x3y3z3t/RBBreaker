/*  GameClone/CHorizonUpgradeBuildInfo.cs
 *  Version 1.0 (2025.06.01)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CHorizonUpgradeBuildInfo : ISaveable
    {
        public void Load(SaveFileReader _reader)
        {
            m_UpgradeProtoName = _reader.ReadString();
            m_StartTime = _reader.ReadDateTime();
            m_EndTime = _reader.ReadDateTime();
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteString(m_UpgradeProtoName);
            _writer.WriteDateTime(m_StartTime);
            _writer.WriteDateTime(m_EndTime);
        }


        private string m_UpgradeProtoName = string.Empty;
        private DateTime m_StartTime = DateTime.MinValue;
        private DateTime m_EndTime = DateTime.MinValue;
    }

}
