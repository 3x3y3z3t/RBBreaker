/*  GameClone/BaseClasses/CItem.cs
 *  Version 2 (2025.06.05)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.Text;
using RBSaveEditor.GameClone.BaseClasses.RewriteSystem;

namespace RBSaveEditor.GameClone.BaseClasses
{
    public class CItem : ISaveable, IDeepCloneable
    {
        public eItemType ItemType => m_Proto_ItemType;

        public eEquipmentType EquipmentType => m_EquipmentType;
        //public eSpecializationSubtype SpecializationSubType => m_SpecializationSubtype;
        public eRarity Rarity { get => m_Rarity; set => m_Rarity = value; }


        public int ItemLevel { get => m_ItemLevel; set => m_ItemLevel = value; }


        public string SpecializationName => m_Specialization_Name;



        public List<CItemAffix> Affixes => m_Affixes;

        public float UniqueModifier => m_UniqueModifier;
        public string UniqueModifierStatTypeStr => m_UniqueModifierStatTypeStr;



        public int RootInventoryIndex { get => m_RootInventorySlotIndex; set => m_RootInventorySlotIndex = value; }

        public int StackCount { get => m_StackCount; set => m_StackCount = value; }



        public CItem()
        { }

        public CItem(CItem _other)
        {
            // TODO: clone CItem










        }


        public virtual void Load(SaveFileReader _reader)
        {
            m_Proto_Name = _reader.ReadString();
            //m_Proto = CGameDB.GetProto<CItemProto>(m_Proto_Name);

            m_EquipmentType = _reader.ReadEnum<eEquipmentType>();
            m_SpecializationSubtype = _reader.ReadEnum<eSpecializationSubtype>();

            m_Rarity = _reader.ReadEnum<eRarity>();
            m_RequiredShipClass = _reader.ReadEnum<eShipClass>();
            m_RequiredLevel = _reader.ReadInt32();

            m_ItemLevel = _reader.ReadInt32();
            m_CoreMultiplier = _reader.ReadFloat();

            {
                int count = _reader.ReadInt32();
                m_Affixes = new(count);
                for (int i = 0; i < count; ++i)
                {
                    CItemAffix affix = new();
                    affix.Load(_reader);
                    m_Affixes.Add(affix);
                }
            }

            m_UniqueModifier = _reader.ReadFloat();
            {
                string typeString = _reader.ReadString();
                if (typeString == Constants.NullStr)
                {
                    m_UniqueModifierStatTypeStr = string.Empty;
                }
                else
                {
                    //Type? type = Type.GetType(typeString);
                    //if (type == null)
                    //{
                    //    throw new ArgumentException("Invalid type '" + typeString + "'.");
                    //}
                    //m_UniqueModifierStatType = type;
                    m_UniqueModifierStatTypeStr = typeString;
                }
            }

            m_CurrentDurability = _reader.ReadFloat();

            m_Specialization_Name = _reader.ReadString();
            m_DefinedItem_Name = _reader.ReadString();

            m_Price = _reader.ReadDouble();
            m_ForSale = _reader.ReadBool();

            m_Scrap = _reader.ReadBool();
            m_Favorite = _reader.ReadBool();

            m_AnalysisProgressFraction = _reader.ReadFloat();
            m_NumTimesAnalyzed = _reader.ReadInt32();
            m_BeingAnalyzedByEntityProtoName = _reader.ReadString();

            m_PreservedFavored = _reader.ReadBool();

            {
                int count = _reader.ReadInt32();
                m_RewritableCategories = new(count);
                for (int i = 0; i < count; ++i)
                {
                    eItemRewriteType rewriteType = _reader.ReadEnum<eItemRewriteType>();
                    CBaseRewritableCategory? category = CBaseRewritableCategoryItem.Create(rewriteType, m_Rarity);
                    if (category == null)
                    {
                        throw new ArgumentException("Couldn't create Rewritable Category Item for type '" + rewriteType + "'.");
                    }

                    category.Load(_reader);
                    m_RewritableCategories.Add(category);
                }
            }

            m_RootInventorySlotIndex = _reader.ReadInt32();

            if (_reader.ReadBool())
            {
                m_MissionTriggerWhenLooted = _reader.ReadString();
            }

            m_StackCount = _reader.ReadInt32();
            m_GameplayRandomSeed = _reader.ReadInt32();

            m_Entity_TeamId = _reader.ReadInt32();
        }

        public virtual void Save(SaveFileWriter _writer)
        {
            _writer.WriteEnum(m_Proto_ItemType); // read in CItem.CreateAndLoad();
            _writer.WriteString(m_Proto_Name);

            _writer.WriteEnum(m_EquipmentType);
            _writer.WriteEnum(m_SpecializationSubtype);
            _writer.WriteEnum(m_Rarity);

            _writer.WriteEnum(m_RequiredShipClass);
            _writer.WriteInt32(m_RequiredLevel);

            _writer.WriteInt32(m_ItemLevel);
            _writer.WriteFloat(m_CoreMultiplier);

            _writer.WriteInt32(m_Affixes.Count);
            foreach (CItemAffix affix in m_Affixes)
            {
                affix.Save(_writer);
            }

            _writer.WriteFloat(m_UniqueModifier);
            if (m_UniqueModifierStatTypeStr == string.Empty)
            {
                _writer.WriteString(Constants.NullStr);
            }
            else
            {
                _writer.WriteString(m_UniqueModifierStatTypeStr.ToString());
            }

            _writer.WriteFloat(m_CurrentDurability);

            _writer.WriteString(m_Specialization_Name);
            _writer.WriteString(m_DefinedItem_Name);

            _writer.WriteDouble(m_Price);
            _writer.WriteBool(m_ForSale);

            _writer.WriteBool(m_Scrap);
            _writer.WriteBool(m_Favorite);

            _writer.WriteFloat(m_AnalysisProgressFraction);
            _writer.WriteInt32(m_NumTimesAnalyzed);
            _writer.WriteString(m_BeingAnalyzedByEntityProtoName);

            _writer.WriteBool(m_PreservedFavored);

            _writer.WriteInt32(m_RewritableCategories.Count);
            foreach (CBaseRewritableCategory category in m_RewritableCategories)
            {
                if (category is not CBaseRewritableCategoryItem categoryItem)
                {
                    throw new ArgumentException("Rewritable Category is not a CBaseRewritableCategoryItem.");
                }

                _writer.WriteEnum(categoryItem.ItemRewriteType);
                category.Save(_writer);
                //categoryItem.Save(_writer);
            }

            _writer.WriteInt32(m_RootInventorySlotIndex);

            _writer.WriteBool(m_MissionTriggerWhenLooted != string.Empty);
            if (m_MissionTriggerWhenLooted != string.Empty)
            {
                _writer.WriteString(m_MissionTriggerWhenLooted);
            }

            _writer.WriteInt32(m_StackCount);
            _writer.WriteInt32(m_GameplayRandomSeed);

            _writer.WriteInt32(m_Entity_TeamId);
        }

        public virtual IDeepCloneable DeepClone()
        {
            var copy = (CItem)MemberwiseClone();

            // TODO: clone deepcloneable items;
            // m_Affixes
            // m_RewritableCategories;

            return copy;
        }

        public override string ToString()
        {
            string str = "CItem: " + m_Proto_ItemType.ToString();
            switch (m_Proto_ItemType)
            {
                case eItemType.Equipment:
                    break;
                case eItemType.Resource:
                    str += " - " + m_Specialization_Name + " (x" + m_StackCount + ")";
                    break;
                case eItemType.Fragment:
                    break;
                case eItemType.Chassis:
                    break;
            }

            return str;
        }

        public string GetUniqueModifierString()
        {
            string str = m_UniqueModifier + " ";
            if (m_UniqueModifierStatTypeStr == string.Empty)
                str += "null";
            else
                str += m_UniqueModifierStatTypeStr;
            str += " (Unique)";

            return str;
        }


        public virtual string GetFullDisplayName()
        {
            switch (m_Proto_ItemType)
            {
                case eItemType.Equipment:
                    return "Item: " + m_EquipmentType.ToString();
                case eItemType.Resource:
                    return m_Specialization_Name.ToString();
                case eItemType.Chassis:
                    return "[Chasis]";
                default:
                    return "[Unknown Item]";
            }
        }

        public string GetStatString_Fate()
        {
            List<string> lst = GetAffixDisplayStrings(eItemAffixGroup.Fate);

            string result = "";
            foreach (string str in lst)
            {
                result += str + "\n";
            }

            return result;
        }




        public List<string> GetAffixDisplayStrings(eItemAffixGroup _affixGroup)
        {
            if (m_Affixes.Count == 0)
                return new();


            List<string> lst = new();
            foreach (var affix in m_Affixes)
            {
                if (affix.AffixGroup != _affixGroup)
                    continue;

                lst.Add(affix.ToString());





            }

            return lst;
        }








        public static CItem? CreateAndLoad(SaveFileReader _reader)
        {
            CItem? item;

            eItemType itemType = _reader.ReadEnum<eItemType>();
            switch (itemType)
            {
                case eItemType.Equipment:
                    item = new CItemEquipment();
                    break;

                case eItemType.Resource:
                case eItemType.Fragment:
                case eItemType.Chassis:
                    item = new CItem();
                    break;

                default:
                    Console.WriteLine("Unknown item type '" + itemType + "'.");
                    return null;
            }

            if (item != null)
            {
                /*  In actual game code, ItemType is part of Proto object
                 *  and is set in item.Load().
                 *      item.Load();
                 *          m_Proto = CGameDB.GetProto<CItemProto>(m_Proto_Name);
                 *                    ^ ItemType is pre-set in Proto object and stored in CGameDB
                 *  
                 *  Since we are not cloning the proto system, we have to set ItemType manually
                 *  from value loaded in save file.
                 */
                item.m_Proto_ItemType = itemType;

                item.Load(_reader);
            }

            return item;
        }

        public static CItem? ImportFromByteArray(byte[] _bytes)
        {
            SaveFileReader reader;
            try
            {
                reader = new(_bytes);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("CItem.ImportFromByteArray(): Couldn't create reader from bytes array: " + _ex);
                return null;
            }

            CItem? item = CreateAndLoad(reader);
            reader.Close();

            return item;
        }

        public static byte[] ExportToByteArray(CItem _item)
        {
            SaveFileWriter writer = new();
            _item.Save(writer);

            byte[] arr = writer.SaveToBytesArray();
            writer.Close();

            return arr;
        }


        private eItemType m_Proto_ItemType = eItemType.Equipment;
        private string m_Proto_Name = string.Empty;

        protected eEquipmentType m_EquipmentType = eEquipmentType.None;
        private eSpecializationSubtype m_SpecializationSubtype = eSpecializationSubtype.Main;
        protected eRarity m_Rarity = eRarity.Junk;

        private eShipClass m_RequiredShipClass = eShipClass.Fighter;
        private int m_RequiredLevel = 0;

        private int m_ItemLevel = 0;
        private float m_CoreMultiplier = 0.0f;

        private List<CItemAffix> m_Affixes = null!;

        private float m_UniqueModifier = 0.0f;
        // actually the game stores this as a Type;
        private string m_UniqueModifierStatTypeStr = null;

        private float m_CurrentDurability = 0.0f;

        // actually the game stores this as an index in a list of CItemSpecialization in CItemProto;
        private string m_Specialization_Name = string.Empty;
        // actually the game stores this as an index in a list of CDefinedItem in CItemProto;
        private string m_DefinedItem_Name = string.Empty;

        private double m_Price = 0.0;
        private bool m_ForSale = false;
        // private int m_CraftingCost = 0; // no longer saved since version 108;

        private bool m_Scrap = false;
        private bool m_Favorite = false;

        private float m_AnalysisProgressFraction = 0.0f;
        private int m_NumTimesAnalyzed = 0;
        private string m_BeingAnalyzedByEntityProtoName = string.Empty;

        private bool m_PreservedFavored = false;

        private List<CBaseRewritableCategory> m_RewritableCategories = null!;

        private int m_RootInventorySlotIndex = -1;

        private string m_MissionTriggerWhenLooted = string.Empty;

        private int m_StackCount = 0;
        private int m_GameplayRandomSeed = 0;

        private int m_Entity_TeamId = 0;


    }

}


// TODO:
// CItemProto.GetSpecializationIndex probably slow compile?
