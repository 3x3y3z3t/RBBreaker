/*  GameClone/CUnlockedAptitudeInfo.cs
 *  Version 1.0 (2025.05.29)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CUnlockedAptitudeInfo
    {
        public string AptitudeName => m_AptitudeName;
        public int HighestChallengeLevel => m_HighestChallengeLevel;


        public CUnlockedAptitudeInfo()
        { }

        public CUnlockedAptitudeInfo(CUnlockedAptitudeInfo _other)
        {
            m_AptitudeName = _other.AptitudeName;
            m_HighestChallengeLevel = _other.HighestChallengeLevel;
        }


        public static CUnlockedAptitudeInfo Read(SaveFileReader _reader)
        {
            return new()
            {
                m_AptitudeName = _reader.ReadString(),
                m_HighestChallengeLevel = _reader.ReadInt32(),
            };
        }

        public static void Write(SaveFileWriter _writer, object _value)
        {
            CUnlockedAptitudeInfo? value = _value as CUnlockedAptitudeInfo;
            if (value == null)
                throw new ArgumentNullException(nameof(_value), " Parameter must be of type '" + typeof(CUnlockedAptitudeInfo) + "'.");

            _writer.WriteString(value.m_AptitudeName);
            _writer.WriteInt32(value.m_HighestChallengeLevel);
        }


        private string m_AptitudeName = null!;
        private int m_HighestChallengeLevel = 0;
    }

}
