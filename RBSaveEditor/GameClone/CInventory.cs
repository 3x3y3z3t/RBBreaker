/*  GameClone/CInventory.cs
 *  Version 1.0 (2025.06.02)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.GameClone
{
    public class CInventory : ISaveable, IDeepCloneable
    {
        public List<CItem> Items => m_Items;


        public void Load(SaveFileReader _reader)
        {
            m_Width = _reader.ReadInt32();
            m_Height = _reader.ReadInt32();

            {
                int count = _reader.ReadInt32();
                m_Items = new(count);
                for (int i = 0; i < count; ++i)
                {
                    CItem? item = CItem.CreateAndLoad(_reader);
                    if (item == null)
                    {
                        throw new ArgumentException("Couldn't create item.");
                    }

                    m_Items.Add(item);
                    //AddInternal(ref item);
                }
            }
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_Width);
            _writer.WriteInt32(m_Height);

            _writer.WriteInt32(m_Items.Count);
            foreach (CItem item in m_Items)
            {
                item.Save(_writer);
            }
        }

        public IDeepCloneable DeepClone()
        {
            var copy = (CInventory)MemberwiseClone();
            copy.m_Items = m_Items.DeepClone();

            return copy;
        }

        public override string ToString()
        {
            string str = "CInventory (Storage): " + m_Width + " x " + m_Height;
            return str;
        }


        private int m_Width = 0;
        private int m_Height = 0;

        private List<CItem> m_Items = null!;





    }



}
