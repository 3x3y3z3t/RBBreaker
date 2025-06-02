/*  GameClone/Items/CItemSet.cs
 *  Version 1.0 (2025.06.03)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.GameClone.Items
{
    public class CItemSet : ISaveable
    {
        public string Proto_Name => m_Proto_Name;
        public bool Proto_IsValid => m_Proto_IsValid;


        public CItemSet(string _protoName)
        {
            m_Proto_Name = _protoName;
            m_Proto_IsValid = IsProtoNameAssociateWithAnySet(_protoName);
        }


        public void Load(SaveFileReader _reader)
        {
            {
                int count = _reader.ReadInt32();
                m_Entries = new(count);
                for (int i = 0; i < count; ++i)
                {
                    CItemSetEntry entry = new(i);
                    entry.Load(_reader);
                    m_Entries.Add(entry);
                }
            }

            m_Completed = _reader.ReadBool();
            m_Favorite = _reader.ReadBool();
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_Entries.Count);
            foreach (CItemSetEntry entry in m_Entries)
            {
                entry.Save(_writer);
            }

            _writer.WriteBool(m_Completed);
            _writer.WriteBool(m_Favorite);
        }


        public static bool IsProtoNameAssociateWithAnySet(string _protoName) => s_ItemSetProtosClone.ContainsKey(_protoName);


        private static Dictionary<string, int> s_ItemSetProtosClone = new()
        {
            { "ItemSet-1", 5 },
            { "ItemSet-2", 6 },
            { "ItemSet-3", 4 },
            { "ItemSet-4", 5 },
            { "ItemSet-5", 5 },
            { "ItemSet-6", 4 },
            { "ItemSet-7", 3 },
            { "ItemSet-8", 4 },
            { "ItemSet-9", 4 },
        };


        private string m_Proto_Name = string.Empty;
        private bool m_Proto_IsValid = false;

        private List<CItemSetEntry> m_Entries = null!;

        private bool m_Completed = false;
        private bool m_Favorite = false;



    }

    public class CItemSetEntry : ISaveable
    {
        public CItemSetEntry(int _index)
        {
            m_Index = _index;
        }


        public void Load(SaveFileReader _reader)
        {
            m_Acquired = _reader.ReadBool();
            m_HighestRarity = _reader.ReadEnum<eRarity>();
            if (_reader.ReadBool())
            {
                CItem? item = CItem.CreateAndLoad(_reader);
                if (item == null)
                {
                    throw new ArgumentException("Could not create and load item.");
                }

                m_Item = item;
            }
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteBool(m_Acquired);
            _writer.WriteEnum(m_HighestRarity);
            _writer.WriteBool(m_Item != null);
            if (m_Item != null)
            {
                m_Item.Save(_writer);
            }
        }


        private bool m_Acquired = false;
        private eRarity m_HighestRarity = eRarity.Junk;
        private CItem? m_Item = null;
        private int m_Index = -1;
    }


}
