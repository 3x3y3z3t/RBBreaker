/*  GameClone/RewriteSystem/CRewritableCategoryItem_Subtypes.cs
 *  Version 2 (2025.06.23)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using RBSaveEditor.GameClone.BaseClasses;
using RBSaveEditor.GameClone.BaseClasses.RewriteSystem;

namespace RBSaveEditor.GameClone.RewriteSystem
{
    public class CRewritableCategoryItemAffixType : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemAffixType();
        }
    }

    public class CRewritableCategoryItemAffixValue : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemAffixValue();
        }
    }

    public class CRewritableCategoryItemRarity : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemRarity();
        }
    }

    public class CRewritableCategoryItemRequiredShipClass : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryRequiredShipClass();
        }
    }

    public class CRewritableCategoryItemRequiredLevel : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryRequiredLevel();
        }
    }

    public class CRewritableCategoryItemUniqueModifierValue : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemUniqueModifierValue();
        }
    }

    public class CRewritableCategoryItemStatCostValue : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            throw new NotImplementedException();
        }
    }

    public class CRewritableCategoryItemUniqueModifierStat : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemUniqueModifierStat();
        }
    }

    public class CRewritableCategoryItemDrawback : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryDrawback();
        }
    }

    public class CRewritableCategoryItemUsable : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemUsable();
        }
    }

    public class CRewritableCategoryItemEquipmentType : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemEquipmentType();
        }
    }

    public class CRewritableCategoryItemAffixOperator : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryItemAffixOperator();
        }
    }

    public class CRewritableCategoryItemCrafting : CBaseRewritableCategoryItem
    {
        public override CBaseRewritableEntry CreateRewritableEntry()
        {
            return new CRewritableEntryRequiredLevel();
        }


        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_CraftingBudget = _reader.ReadInt32();

            int count = _reader.ReadInt32();
            m_FragmentItems = new(count);
            for (int i = 0; i < count; ++i)
            {
                CItem? item = CItem.CreateAndLoad(_reader);
                if (item == null)
                {
                    Console.WriteLine("CRewritableCategoryItemCrafting.Load: Couldn't create Fragment item is null. " + _reader.ToString());
                    throw new Exception("CRewritableCategoryItemCrafting.Load: Couldn't create Fragment item is null. " + _reader.ToString());
                }
                m_FragmentItems.Add(item);
            }

            m_FragmentsApplied = _reader.ReadBool();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteInt32(m_CraftingBudget);

            _writer.WriteInt32(m_FragmentItems.Count);
            foreach (CItem item in m_FragmentItems)
            {
                item.Save(_writer);
            }
            _writer.WriteBool(m_FragmentsApplied);
        }


        private int m_CraftingBudget = 0;

        private List<CItem> m_FragmentItems = null!;
        private bool m_FragmentsApplied = false;
    }

}
