/*  GameClone/CItemEquipment.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.GameClone
{
    public class CItemEquipment : CItem
    {
        //public string RareNameAdjective => s_RareNameAdjectives[m_RandomizedRareNameAdjectiveIndex];
        //public string RareNameNoun => s_RareNameNouns[m_RandomizedRareNameBaseNounIndex];


        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_RandomizedRareNameAdjectiveIndex = _reader.ReadInt32();
            m_RandomizedRareNameBaseNounIndex = _reader.ReadInt32();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteInt32(m_RandomizedRareNameAdjectiveIndex);
            _writer.WriteInt32(m_RandomizedRareNameBaseNounIndex);
        }

        public override IDeepCloneable DeepClone()
        {
            return base.DeepClone();
        }


        public override string GetFullDisplayName()
        {
            if (m_EquipmentType == eEquipmentType.FATECore)
            {
                return "Fate Core";
            }

            if (m_Rarity == eRarity.Rare)
            {
                return GetRareItemDisplayName();
            }

            return base.GetFullDisplayName();
        }


        private string GetRareItemDisplayName()
            => s_RareNameAdjectives[m_RandomizedRareNameAdjectiveIndex] + " " + s_RareNameNouns[m_RandomizedRareNameBaseNounIndex];


        private static readonly string[] s_RareNameAdjectives =
        {
            "Unscrupulous",
            "Black Market",
            "Confident",
            "Break",
            "Over",
            "Tritanium",
            "Force",
            "Blast",
            "Second",
            "Final",
            "Fragment",
            "Opaque",
            "Cataclysm",
            "Rune",
            "Ether",
            "Shadow",
            "Ranked",
            "Spiral",
            "Space",
            "Nebula",
            "Galaxy",
        };

        private static readonly string[] s_RareNameNouns =
        {
            "Container",
            "Carry",
            "Binding",
            "Frame",
            "Element",
            "Wrap",
            "Braid",
            "Funnel",
            "Mechanism",
            "Volt",
            "Laminate",
            "Rivet",
            "Aegis",
            "Emblem",
            "Object",
            "Star",
            "Nucleus",
            "Core",
        };


        private int m_RandomizedRareNameAdjectiveIndex;
        private int m_RandomizedRareNameBaseNounIndex;
    }

}
