/*  GameClone/BaseClasses/CReward.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone.BaseClasses
{
    public abstract class CReward : ISaveable, IDeepCloneable
    {
        public virtual void Load(SaveFileReader _reader)
        {
            m_MissionReward = _reader.ReadBool();
        }

        public virtual void Save(SaveFileWriter _writer)
        {
            _writer.WriteBool(m_MissionReward);
        }

        public virtual IDeepCloneable DeepClone()
        {
            var copy = (CReward)MemberwiseClone();
            return copy;
        }


        public static CReward? Create(string _typeStr, CPlayer _missionOwner)
        {
            switch (_typeStr)
            {
                case "CRewardChasisProto": return new CRewardChasis();

                    // TODO: more reward type;


                default:
                    Console.WriteLine("Not supported CRewardProto type '" + _typeStr + "'.");
                    return null;
            }
        }


        private bool m_MissionReward = false;
    }

}
