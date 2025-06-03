/*  GameClone/BaseClasses/RewriteSystem/CBaseRewritableCategoryItem.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.RewriteSystem;

namespace RBSaveEditor.GameClone.BaseClasses.RewriteSystem
{
    public abstract class CBaseRewritableCategoryItem : CBaseRewritableCategory
    {
        public eItemRewriteType ItemRewriteType => m_ItemRewriteType;


        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_ItemRewriteType = _reader.ReadEnum<eItemRewriteType>();
            m_ItemRarity = _reader.ReadEnum<eRarity>();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteEnum(m_ItemRewriteType);
            _writer.WriteEnum(m_ItemRarity);
        }

        public override IDeepCloneable DeepClone()
        {
            var copy = (CBaseRewritableCategoryItem)base.DeepClone();
            return copy;
        }


        public static CBaseRewritableCategory? Create(eItemRewriteType _rewriteType, eRarity _rarity)
        {
            CBaseRewritableCategoryItem category;
            switch (_rewriteType)
            {
                case eItemRewriteType.AffixType:
                    category = new CRewritableCategoryItemAffixType();
                    break;
                case eItemRewriteType.AffixValue:
                    category = new CRewritableCategoryItemAffixValue();
                    break;
                case eItemRewriteType.Rarity:
                    category = new CRewritableCategoryItemRarity();
                    break;
                case eItemRewriteType.RequiredShipClass:
                    category = new CRewritableCategoryItemRequiredShipClass();
                    break;
                case eItemRewriteType.RequiredLevel:
                    category = new CRewritableCategoryItemRequiredLevel();
                    break;
                case eItemRewriteType.UniqueModifierValue:
                    category = new CRewritableCategoryItemUniqueModifierValue();
                    break;
                case eItemRewriteType.StatCostValue:
                    category = new CRewritableCategoryItemStatCostValue();
                    break;
                case eItemRewriteType.UniqueModifierStatType:
                    category = new CRewritableCategoryItemUniqueModifierStat();
                    break;
                case eItemRewriteType.Drawback:
                    category = new CRewritableCategoryItemDrawback();
                    break;
                case eItemRewriteType.ItemUsable:
                    category = new CRewritableCategoryItemUsable();
                    break;
                case eItemRewriteType.EquipmentType:
                    category = new CRewritableCategoryItemEquipmentType();
                    break;
                case eItemRewriteType.AffixOperator:
                    category = new CRewritableCategoryItemAffixOperator();
                    break;
                case eItemRewriteType.Crafting:
                    category = new CRewritableCategoryItemCrafting();
                    break;

                default:
                    Console.WriteLine("(NYI) Invalid Item Rewrite Type '" + _rewriteType + "'.");
                    return null;
            }

            category.m_ItemRewriteType = _rewriteType;
            category.m_ItemRarity = _rarity;
            return category;
        }


        private eItemRewriteType m_ItemRewriteType = eItemRewriteType.AffixType;
        private eRarity m_ItemRarity = eRarity.Junk;
    }

}
