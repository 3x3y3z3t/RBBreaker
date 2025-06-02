/*  GameClone/CSpecialistUpgrade.cs
 *  Version 1.0 (2025.06.03)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CSpecialistUpgrade : ISaveable
    {
        public string Proto_Name => m_Proto_Name;
        public bool Proto_IsValid => m_Proto_IsValid;

        public CSpecialistUpgrade(string _protoName)
        {
            m_Proto_Name = _protoName;
            m_Proto_IsValid = IsProtoNameAssociateWithAnyUpgrade(_protoName);
        }


        public void Load(SaveFileReader _reader)
        {
            int count = _reader.ReadInt32();
            m_PurchasedInfos = new(count);
            for (int i = 0; i < count; i++)
            {
                CSpecialistUpgradePurchasedInfo info = new();
                info.Load(_reader);
                m_PurchasedInfos.Add(info);
            }
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_PurchasedInfos.Count);
            foreach (CSpecialistUpgradePurchasedInfo info in m_PurchasedInfos)
            {
                info.Save(_writer);
            }
        }

        public override string ToString()
        {
            string str = "CSpecialistUpgrade \"" + m_Proto_Name + "\"";
            if (!m_Proto_IsValid)
            {
                str += " (invalid)";
                return str;
            }

            str += " [";
            foreach (var info in m_PurchasedInfos)
            {
                if (info.Purchased)
                    str += "1 ";
                else
                    str += "0 ";
            }
            str = str.Remove(str.Length - 1, 1);

            str += "]";

            return str;
        }




        public static bool IsProtoNameAssociateWithAnyUpgrade(string _protoName) => s_SpecialistUpgradeProtosClone.ContainsKey(_protoName);


        private static Dictionary<string, int> s_SpecialistUpgradeProtosClone = new()
        {
            { "DroneSlots", 3 },
            { "RepairDuration", 2 },
            { "RepairStrength", 4 },
            { "YieldQuality", 2 },
            { "ResourceStackSize", 3 },
            { "Countermeasures", 2 },
            { "ShipPower", 4 },
        };




        private string m_Proto_Name = string.Empty;
        private bool m_Proto_IsValid = false;

        private List<CSpecialistUpgradePurchasedInfo> m_PurchasedInfos = null!;

    }

    public class CSpecialistUpgradePurchasedInfo : ISaveable
    {
        public bool Purchased => m_Purchased;


        public void Load(SaveFileReader _reader)
        {
            m_Purchased = _reader.ReadBool();
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteBool(m_Purchased);
        }

        public override string ToString()
        {
            return "CSpecialistUpgradePurchasedInfo (" + m_Purchased + ")";
        }


        private bool m_Purchased = false;
    }

}
