/*  GameClone/IDeepCloneable.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor
{
    public interface IDeepCloneable
    {
        public IDeepCloneable DeepClone();
    }

    public static class DeepCloneUtils
    {
        public static List<T> DeepClone<T>(this List<T> _original) where T : IDeepCloneable
        {
            List<T> result = new(_original.Count);
            foreach (var item in _original)
            {
                result.Add((T)item.DeepClone());
            }
            return result;
        }

        public static HashSet<T> DeepClone<T>(this HashSet<T> _original) where T : IDeepCloneable
        {
            HashSet<T> result = new(_original.Count);
            foreach (var item in _original)
            {
                result.Add((T)item.DeepClone());
            }
            return result;
        }

        public static Dictionary<TKey, TValue> DeepClone<TKey, TValue>(this Dictionary<TKey, TValue> _original) where TKey : notnull where TValue : IDeepCloneable
        {
            Dictionary<TKey, TValue> result = new(_original.Count, _original.Comparer);
            foreach (var pair in _original)
            {
                result.Add(pair.Key, pair.Value);
            }
            return result;
        }
    }


}
