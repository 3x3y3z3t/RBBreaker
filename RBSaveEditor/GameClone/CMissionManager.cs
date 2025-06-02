/*  GameClone/CMissionManager.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.Proto;

namespace RBSaveEditor.GameClone
{
    public class CMissionManager : ISaveable
    {
        public CMissionManager(CPlayer _ownerPlayer)
        {
            m_OwnerPlayer = _ownerPlayer;
        }




        public void Load(SaveFileReader _reader)
        {
            {
                int count = _reader.ReadInt32();
                m_AvailableMissions = new(count);
                for (int i = 0; i < count; ++i)
                {
                    string name = _reader.ReadString();
                    //CMissionProto? proto;
                    //if (name == string.Empty)
                    //{
                    //    proto = new();
                    //}
                    //else
                    //{
                    //    proto = CGameDB.GetProto<CMissionProto>(name);
                    //    if (proto == null)
                    //    {
                    //        throw new ArgumentException("No Mission Proto for name '" + name + "'.");
                    //    }
                    //}

                    CMission mission = new(name, m_OwnerPlayer);
                    mission.Load(_reader);
                    m_AvailableMissions.Add(mission);
                }
            }

            m_NumCompletedMissions = _reader.ReadInt32();
            {
                int index = _reader.ReadInt32();
                if (index >= 0)
                {
                    if (index >= m_AvailableMissions.Count)
                    {
                        throw new ArgumentException("CurrentStoryMission index (" + index + ") exceed number of available missions (" + m_AvailableMissions.Count + ").");
                    }

                    m_CurrentStoryMission = m_AvailableMissions[index];
                }
            }
            {
                int index = _reader.ReadInt32();
                if (index >= 0)
                {
                    if (index >= m_AvailableMissions.Count)
                    {
                        throw new ArgumentException("CurrentSideMission index (" + index + ") exceed number of available missions (" + m_AvailableMissions.Count + ").");
                    }

                    m_CurrentSideMission = m_AvailableMissions[index];
                }
            }

            m_DestroyedMissionTargetIds = _reader.ReadDictionary<int, int>();
            m_ScannedMissionTargetIds = _reader.ReadHashSet<int>();
            m_InteractedMissionTargetIds = _reader.ReadHashSet<int>();

            m_StoryTriggersFired = _reader.ReadHashSet<string>();
            m_PermanentStoryTriggersFired = _reader.ReadHashSet<string>();

            m_FracturedTimelines = _reader.ReadHashSet<int>();

            //if (version >= 104 && version < 110)
            //{
            //    _reader.Readint32();
            //}
        }

        public void Save(SaveFileWriter _writer)
        {
            int curStoryMissionIndex = -1;
            int curSideMissionIndex = -1;

            _writer.WriteInt32(m_AvailableMissions.Count);
            for (int i = 0; i < m_AvailableMissions.Count; ++i)
            {
                CMission mission = m_AvailableMissions[i];
                _writer.WriteString(mission.Proto_Name);
                mission.Save(_writer);

                if (mission == m_CurrentStoryMission)
                {
                    curStoryMissionIndex = i;
                }
                else if (mission == m_CurrentSideMission)
                {
                    curSideMissionIndex = i;
                }
            }

            _writer.WriteInt32(m_NumCompletedMissions);
            _writer.WriteInt32(curStoryMissionIndex);
            _writer.WriteInt32(curSideMissionIndex);

            _writer.WriteDictionary(m_DestroyedMissionTargetIds);
            _writer.WriteHashSet(m_ScannedMissionTargetIds);
            _writer.WriteHashSet(m_InteractedMissionTargetIds);

            _writer.WriteHashSet(m_StoryTriggersFired);
            _writer.WriteHashSet(m_PermanentStoryTriggersFired);
            _writer.WriteHashSet(m_FracturedTimelines);
        }


        private List<CMission> m_AvailableMissions = null!;

        private int m_NumCompletedMissions = 0;
        private CMission? m_CurrentStoryMission = null;
        private CMission? m_CurrentSideMission = null;

        private Dictionary<int, int> m_DestroyedMissionTargetIds = null!;
        private HashSet<int> m_ScannedMissionTargetIds = null!;
        private HashSet<int> m_InteractedMissionTargetIds = null!;

        private HashSet<string> m_StoryTriggersFired = null!;
        private HashSet<string> m_PermanentStoryTriggersFired = null!;

        private HashSet<int> m_FracturedTimelines = null!;


        private CPlayer m_OwnerPlayer = null!; //TODO: rename to m_OwnerPlayer;
    }

}
