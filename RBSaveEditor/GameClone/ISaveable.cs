/*  ISaveable.cs
 *  Version 1.0 (2025.05.29)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public interface ISaveable
    {
        public void Load(SaveFileReader _reader);
        public void Save(SaveFileWriter _writer);
    }

}
