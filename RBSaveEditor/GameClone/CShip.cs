/*  GameClone/CShip.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CShip : CEntity
    {
        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            //foreach (CEquippedInfo cequippedInfo in this.mEquipment)
            //{
            //    foreach (IRewritableCategory rewritableCategory in cequippedInfo.Item.RewritableCategories)
            //    {
            //        CRewritableCategoryItemRarity crewritableCategoryItemRarity = rewritableCategory as CRewritableCategoryItemRarity;
            //        if (crewritableCategoryItemRarity != null && cequippedInfo.Item.Proto.RaritySpecialInfos.Count > crewritableCategoryItemRarity.RewritableEntries.Count)
            //        {
            //            crewritableCategoryItemRarity.IncreaseEntriesTo(cequippedInfo.Item.Proto.RaritySpecialInfos.Count, cequippedInfo.Item, this.Player);
            //            CGame.Instance.SaveProfile(false, -1);
            //            // ^ AFAIK this call will return immediately due to IsSaveLoadInProgress is true;
            //        }
            //    }
            //}
        }






    }

}
