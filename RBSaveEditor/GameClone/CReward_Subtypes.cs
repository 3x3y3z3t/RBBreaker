/*  GameClone/CReward_Subtypes.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.GameClone
{
    public class CRewardChasis : CReward
    {
        public override void Load(SaveFileReader _reader)
        {
            CItem? item = CItem.CreateAndLoad(_reader);
            if (item == null)
                throw new ArgumentException("Could not create item for CRewardChasis.");

            m_Item = item;
            m_AutoAddToInventory = _reader.ReadBool();
            base.Load(_reader);
        }

        public override void Save(SaveFileWriter _writer)
        {
            m_Item.Save(_writer);
            _writer.WriteBool(m_AutoAddToInventory);
            base.Save(_writer);
        }

        public override IDeepCloneable DeepClone()
        {
            var copy = (CRewardChasis)base.DeepClone();

            copy.m_Item = (CItem)m_Item.DeepClone();

            return copy;
        }


        private CItem m_Item = null!;
        private bool m_AutoAddToInventory = false;
    }













}
