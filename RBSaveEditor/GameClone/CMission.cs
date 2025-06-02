/*  GameClone/CMission.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.GameClone
{
    public class CMission : ISaveable, IDeepCloneable
    {
        public string Proto_Name => m_Proto_Name;


        public CMission(string _protoName, CPlayer _ownerPlayer)
        {
            m_Proto_Name = _protoName;
            m_MissionOwner = _ownerPlayer;
        }


        public void Load(SaveFileReader _reader)
        {
            m_GameplayRandomSeed = _reader.ReadInt32();

            {
            string name = _reader.ReadString(); // TODO: why do we need to read proto name twice here? Proto name have been read in CMissionManager;
            m_Proto_Name = name;
            }
            {
                string name = _reader.ReadString();
                //this.MissionSpec = CGame.GameDB.GetProto<CMissionSpec>(name);
                m_MissionSpec_Name = name;
            }

            m_MissionState = _reader.ReadEnum<eMissionState>();
            // REDUNDANT: There is a call to SetState() here, which in turn calls CGame.SaveProfile, which never runs because a load is in progress;

            m_CurrentMapProtoName = _reader.ReadString();
            m_MainMapIndex = _reader.ReadInt32();

            m_Rarity = _reader.ReadEnum<eRarity>();
            m_MaxShipClass = _reader.ReadEnum<eShipClass>();

            {
                int count = _reader.ReadInt32();
                m_Rewards = new(count);
                for (int i = 0; i < count; ++i)
                {
                    string typeStr = _reader.ReadString();
                    CReward? reward = CReward.Create(typeStr, m_MissionOwner); // in actual game code the CReward is created from CRewardProto;
                    if (reward == null)
                    {
                        throw new ArgumentException("Couldn't create CReward from proto type name '" + typeStr + "'.");
                    }
                    reward.Load(_reader);
                    m_Rewards.Add(reward);
                }
            }

            m_AwardOnStartInfos = _reader.ReadBool();

            m_ChallengeLevelIndex = _reader.ReadInt32();

            if (_reader.ReadBool())
            {
                // missionTarget should or should not be null;
                eMissionTargetType targetType = _reader.ReadEnum<eMissionTargetType>();
                m_MissionTarget = CMissionTarget.CreateAndLoad(_reader, targetType);
                if (m_MissionTarget == null)
                {
                    throw new ArgumentException("Could not create MissionTarget with type '" + targetType + "'.");
                }
            }

            Console.WriteLine("> CMission.Load");
            //m_LaunchMode = _reader.ReadString();




            //if (version >= 89)
            //{
            //    this.LaunchMode = reader.Read<string>();
            //}
            //this.TimeLimitSeconds = reader.Read<float>();
            //this.MissionCompleted = reader.Read<bool>();
            //this.Failed = reader.Read<bool>();
            //if (version >= 94)
            //{
            //    this.FatestormAvailable = reader.Read<bool>();
            //    this.FatestormTimeRemaining = reader.Read<float>();
            //}
            //int num2 = reader.Read<int>();
            //for (int j = 0; j < num2; j++)
            //{
            //    eMissionRewriteType eMissionRewriteType = reader.Read<eMissionRewriteType>();
            //    IRewritableCategory rewritableCategory = CBaseRewritableCategoryMission.Create(eMissionRewriteType);
            //    if (rewritableCategory == null)
            //    {
            //        CLog.ErrorTime(string.Format("couldn't create category for {0}", eMissionRewriteType), Array.Empty<object>());
            //    }
            //    else
            //    {
            //        rewritableCategory.Load(reader, version);
            //        this.RewritableCategories.Add(rewritableCategory);
            //    }
            //}
            //this.ElapsedTime = reader.Read<float>();
            //this.ObjectiveElapsedTime = reader.Read<float>();
            //if (version >= 59)
            //{
            //    this.ObjectiveElapsedRealtime = reader.Read<float>();
            //}
            //else
            //{
            //    this.ObjectiveElapsedRealtime = this.ObjectiveElapsedTime;
            //}
            //if (version >= 60)
            //{
            //    this.ElapsedRealtime = reader.Read<float>();
            //}
            //else
            //{
            //    this.ElapsedRealtime = this.ElapsedTime;
            //}
            //if (version >= 36)
            //{
            //    this.NonStoryElapsedTime = reader.Read<float>();
            //}
            //if (version >= 39)
            //{
            //    this.NonStoryObjectiveElapsedTime = reader.Read<float>();
            //}
            //if (this.IsFlagSet(eMissionFlag.FractureMission))
            //{
            //    this.ElapsedTime = 0f;
            //    this.ObjectiveElapsedTime = 0f;
            //    this.ElapsedRealtime = 0f;
            //    this.ObjectiveElapsedRealtime = 0f;
            //    this.NonStoryElapsedTime = 0f;
            //    this.NonStoryObjectiveElapsedTime = 0f;
            //}
            //int num3 = reader.Read<int>();
            //this.mObjectiveProtoIndicesCompleted = new HashSet<int>();
            //for (int k = 0; k < num3; k++)
            //{
            //    this.mObjectiveProtoIndicesCompleted.Add(reader.Read<int>());
            //}
            //if (version >= 17)
            //{
            //    this.Affixes = new List<CMissionAffix>();
            //    int num4 = reader.Read<int>();
            //    for (int l = 0; l < num4; l++)
            //    {
            //        CMissionAffix cmissionAffix = new CMissionAffix();
            //        cmissionAffix.Load(reader, version);
            //        this.Affixes.Add(cmissionAffix);
            //    }
            //}
            //if (version >= 40)
            //{
            //    int num5 = 0;
            //    if (version < 46)
            //    {
            //        reader.ReadHashSet<int>();
            //        reader.ReadHashSet<int>();
            //    }
            //    if (version >= 47)
            //    {
            //        num5 = reader.Read<int>();
            //    }
            //    for (int m = 0; m < this.Proto.ExpertGoals.Count; m++)
            //    {
            //        CExpertGoal cexpertGoal = this.Proto.ExpertGoals[m].Create();
            //        if (m < num5 || version == 46)
            //        {
            //            cexpertGoal.State = reader.Read<CExpertGoal.eExpertGoalState>();
            //        }
            //        else
            //        {
            //            cexpertGoal.State = CExpertGoal.eExpertGoalState.Idle;
            //        }
            //        this.mExpertGoals.Add(cexpertGoal);
            //    }
            //}
            //this.SetupFlowBonus();
            //this.ReprocessObjectiveSkips();








            throw new NotImplementedException();
        }

        public void Save(SaveFileWriter _writer)
        {
            Console.WriteLine("> CMission.Save");
        }

        public IDeepCloneable DeepClone()
        {
            var copy = (CMission)MemberwiseClone();

            // TODO: copy non-primitive type;

            return copy;
        }


        private string m_Proto_Name = string.Empty;
        private string m_MissionSpec_Name = string.Empty;

        private int m_GameplayRandomSeed = 0;

        private eMissionState m_MissionState = eMissionState.Inactive;

        private string m_CurrentMapProtoName = string.Empty;
        private int m_MainMapIndex = -1;

        private eRarity m_Rarity = eRarity.Junk;
        private eShipClass m_MaxShipClass = eShipClass.Fighter;

        private List<CReward> m_Rewards = null!;

        private bool m_AwardOnStartInfos = false;

        private int m_ChallengeLevelIndex = -1;

        private CMissionTarget? m_MissionTarget = null;




        private CPlayer m_MissionOwner = null!; //TODO: rename to m_OwnerPlayer;

    }

}
