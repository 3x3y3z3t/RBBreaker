/*  GameClone/BaseClasses/CEntity.cs
 *  Version 1.0 (2025.06.02)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CEntity : ISaveable, IDeepCloneable
    {
        public CInventory Inventory => m_Inventory;


        public virtual void Load(SaveFileReader _reader)
        {
            m_ProgressionElement = CProgressionElement.Create(m_Proto_Name);
            m_ProgressionElement.Load(_reader);

            int count = _reader.ReadInt32();
            m_EquippedInfos = new(count);
            for (int i = 0; i < count; ++i)
            {
                CEquippedInfo eqInfo = new();
                eqInfo.Load(_reader);
                m_EquippedInfos.Add(eqInfo);
            }

            m_Inventory = new();
            m_Inventory.Load(_reader);

            m_MissionTargetId = _reader.ReadInt32();
        }

        public virtual void Save(SaveFileWriter _writer)
        {
            m_ProgressionElement.Save(_writer);

            _writer.WriteInt32(m_EquippedInfos.Count);
            foreach (var eqInfo in m_EquippedInfos)
            {
                eqInfo.Save(_writer);
            }

            m_Inventory.Save(_writer);

            _writer.WriteInt32(m_MissionTargetId);
        }

        public IDeepCloneable DeepClone()
        {
            var copy = (CEntity)MemberwiseClone();
            copy.m_ProgressionElement = (CProgressionElement)m_ProgressionElement.DeepClone();
            copy.m_EquippedInfos = m_EquippedInfos.DeepClone();
            copy.m_Inventory = (CInventory)m_Inventory.DeepClone();

            return copy;
        }


        protected string m_Proto_Name = string.Empty;

        protected CProgressionElement m_ProgressionElement = null!;

        private List<CEquippedInfo> m_EquippedInfos = null!;

        private CInventory m_Inventory = null!;

        private int m_MissionTargetId = 0;
    }

}
