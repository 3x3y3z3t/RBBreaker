/*  GameClone/BaseClasses/CMissionTarget.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone.BaseClasses
{
    public abstract class CMissionTarget : ISaveable, IDeepCloneable
    {
        public abstract void Load(SaveFileReader _reader);

        public abstract void Save(SaveFileWriter _writer);

        public virtual IDeepCloneable DeepClone() => (CMissionTarget)MemberwiseClone();


        public static CMissionTarget? CreateAndLoad(SaveFileReader _reader, eMissionTargetType _missionTargetType)
        {
            CMissionTarget? target;
            switch (_missionTargetType)
            {
                case eMissionTargetType.Character:
                    target = new CMissionTargetCharacter();
                    break;
                case eMissionTargetType.Structure:
                    target = new CMissionTargetStructure();
                    break;
                case eMissionTargetType.Item:
                    target = new CMissionTargetItem();
                    break;

                default:
                    Console.WriteLine("Unsupported Mission Target Type '" + _missionTargetType + "'.");
                    return null;
            }

            target.m_MissionTargetType = _missionTargetType;
            target.Load(_reader);
            return target;
        }


        protected eMissionTargetType m_MissionTargetType = eMissionTargetType.Character;
    }

}
