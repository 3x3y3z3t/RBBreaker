/*  GameClone/BaseClasses/CElement.cs
 *  Version 1.0 (2025.06.02)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public abstract class CElement : IDeepCloneable
    {
        //public abstract CElement Create();
        public abstract IDeepCloneable DeepClone();
    }

}
