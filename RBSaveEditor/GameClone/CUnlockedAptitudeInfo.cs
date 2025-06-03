/*  GameClone/CUnlockedAptitudeInfo.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CUnlockedAptitudeInfo : IDeepCloneable
    {
        public string AptitudeName => m_AptitudeName;
        public int HighestChallengeLevel => m_HighestChallengeLevel;


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

        public IDeepCloneable DeepClone()
        {
            var copy = (CUnlockedAptitudeInfo)MemberwiseClone();
            return copy;
        }


        private string m_AptitudeName = null!;
        private int m_HighestChallengeLevel = 0;
    }

}
