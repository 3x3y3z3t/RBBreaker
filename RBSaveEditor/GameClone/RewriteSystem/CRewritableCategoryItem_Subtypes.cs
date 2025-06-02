/*  GameClone/RewriteSystem/CRewritableCategoryItem_Subtypes.cs
 *  Version 1.0 (2025.06.03)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

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
    }

}
