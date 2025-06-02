/*  GameClone/CEquippedInfo.cs
 *  Version 1.0 (2025.06.02)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.GameClone
{
    public class CEquippedInfo : ISaveable, IDeepCloneable
    {
        public void Load(SaveFileReader _reader)
        {
            m_EquipmentType = _reader.ReadEnum<eEquipmentType>();
            m_SlotIndex = _reader.ReadInt32();

            if (CItem.CreateAndLoad(_reader) is not CItemEquipment item)
            {
                throw new ArgumentException("Could not create and load item.");
            }

            m_Item = item;
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteEnum(m_EquipmentType);
            _writer.WriteInt32(m_SlotIndex);

            m_Item.Save(_writer);
        }

        public IDeepCloneable DeepClone()
        {
            var copy = (CEquippedInfo)MemberwiseClone();
            copy.m_Item = (CItemEquipment)m_Item.DeepClone();

            return copy;
        }


        private eEquipmentType m_EquipmentType = eEquipmentType.None;
        private int m_SlotIndex = -1;

        private CItemEquipment m_Item = null!;

        //public bool m_AdvancedManeuver = false;
    }

}
