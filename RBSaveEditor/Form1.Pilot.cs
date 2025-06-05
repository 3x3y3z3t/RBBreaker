/*  Form1.Pilot.cs
 *  Version 1.0 (2025.06.03)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBSaveEditor.GameClone;
using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor
{
    public partial class Form1
    {

        #region Pilot Event Handlers
        private void btn_LoadPilot_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog()
            {
                AddExtension = true,
                Filter = "RB Save files|" + Constants.PilotSaveFilenameWildcard,
                InitialDirectory = s_SaveFilePath,
                Multiselect = false,
            };

            DialogResult result = dialog.ShowDialog(this);
            switch (result)
            {
                case DialogResult.OK:
                    string fullname = dialog.FileName;
                    m_LoadedSaveFilename = Path.GetFileName(fullname);
                    LoadPilot(m_LoadedSaveFilename);
                    PopulatePilot();
                    return;
                case DialogResult.Cancel:
                    return;
                default:
                    return;
            }
        }

        private void btn_SavePilot_Click(object sender, EventArgs e)
        {
            if (m_LoadedPilot == null)
            {
                Console.WriteLine("LoadedGame is null (this should not happen).");
                return;
            }

            SavePilot();
        }




        private void num_Lvl_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedPilot == null)
                return;

            CProgressionElementPlayer? progression = m_LoadedPilot.Player.ProgressionElement;
            if (progression == null)
                return;

            progression.Level = (int)num_Lvl.Value;

            lbl_NextLvlXp.Text = "/ " + progression.GetXPToNextLevel((int)num_Lvl.Value).ToString("#,###");
        }

        private void num_Xp_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedPilot == null)
                return;

            CProgressionElementPlayer? progression = m_LoadedPilot.Player.ProgressionElement;
            if (progression == null)
                return;

            progression.Experience = (double)num_Xp.Value;
        }

        private void num_Credits_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedPilot == null)
                return;

            foreach (var currency in m_LoadedPilot.Player.Currencies)
            {
                switch (currency.CurrencyType)
                {
                    case eCurrencyType.Credits:
                        currency.Amount = (double)num_Credits.Value;
                        return;
                }
            }
        }

        private void num_Fate_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedPilot == null)
                return;

            foreach (var currency in m_LoadedPilot.Player.Currencies)
            {
                switch (currency.CurrencyType)
                {
                    case eCurrencyType.Fate:
                        currency.Amount = (double)num_Credits.Value;
                        return;
                }
            }
        }







        private void btn_Pilot_ItemImport_Click(object sender, EventArgs e)
        {
            if (m_LoadedPilot == null)
                return;

            if (m_SelectedItemIndex == -1)
            {
                Console.WriteLine("No item selected.");
                return;
            }

            if (tb_Pilot_ItemCode.Text == string.Empty)
            {
                return;
            }

            //string input = tb_Pilot_ItemCode.Text.Replace("\r", "").Replace("\n", "");
            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(tb_Pilot_ItemCode.Text);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("Inputted string is not a valid Base64 string: " + _ex);
                return;
            }

            CItem? item = CItem.ImportFromByteArray(bytes);
            if (item == null)
            {
                Console.WriteLine("Couldn't create item from inputted string.");
                return;
            }

            int index = m_SelectedItemIndex;
            var inventoryItems = m_LoadedPilot.Player.Inventory.Items;

            int rootInvIndex = inventoryItems[index].RootInventoryIndex;
            item.RootInventoryIndex = rootInvIndex;
            inventoryItems[index] = item;

            PopulateInventory();
            list_StorageInventory.Items[index].Focused = true;
            list_StorageInventory.Items[index].Selected = true;
        }

        private void btn_Pilot_ItemExport_Click(object sender, EventArgs e)
        {
            if (uc_ItemCard.SelectedItem == null)
            {
                tb_Pilot_ItemCode.Text = string.Empty;
                return;
            }

            byte[] arr = CItem.ExportToByteArray(uc_ItemCard.SelectedItem);

            string str = Convert.ToBase64String(arr);
            Console.WriteLine("Item exported to Base64 string.");


            tb_Pilot_ItemCode.Text = str;
        }






        #endregion






        private void lbl_SelectedItem_Clear()
        {

        }

        private void lbl_SelectedItem_Populate(CItem _item)
        {
            uc_ItemCard.PopulateWithItem(_item);
            //uc_ItemCard.Item_Title = _item.ItemType.ToString();


            switch (_item.ItemType)
            {
                case eItemType.Equipment:
                    lbl_SelectedItem_Populate_Equipment((CItemEquipment)_item);
                    return;

            }





        }

        private void lbl_SelectedItem_Populate_Equipment(CItemEquipment _item)
        {

            string affixes = "";
            foreach (var affix in _item.Affixes)
            {
                affixes += affix.ToString() + "\n";
            }

            affixes += "\n" + _item.GetUniqueModifierString();


        }

        private void list_StorageInventory_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs _evt)
        {
            if (!_evt.IsSelected || m_LoadedPilot == null)
            {
                uc_ItemCard.ClearItemCard();
                lbl_SelectedItem_Clear();
                m_SelectedItemIndex = -1;
                return;
            }

            var inventoryItems = m_LoadedPilot.Player.Inventory.Items;
            if (_evt.ItemIndex > inventoryItems.Count)
            {
                Console.WriteLine("list_StorageInventory_Click(): Selected index out of range (this should not happen).");
                uc_ItemCard.ClearItemCard();
                lbl_SelectedItem_Clear();
                return;
            }

            m_SelectedItemIndex = _evt.ItemIndex;
            lbl_SelectedItem_Populate(inventoryItems[_evt.ItemIndex]);
        }









        private void LoadPilot(string _filename)
        {
            m_LoadedSaveFilename = _filename;
            string fullname = s_SaveFilePath + "/" + _filename;

            SaveFileReader reader;
            try
            {
                reader = new(fullname);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("LoadPilot(): Could not read pilot save file: " + _ex);
                return;
            }

            m_LoadedPilot = new();
            if (!m_LoadedPilot.LoadProfile(reader))
            {
                m_LoadedPilot = null;
                m_ModdedPilot = null;
                // TODO: buttons;

                Console.WriteLine("LoadPilot(): Could not read pilot from file.");
                return;
            }

            // TODO: clone game;

        }

        private void SavePilot()
        {
            if (m_LoadedPilot == null)
                return;

            string orgFileFullname = s_SaveFilePath + "/" + m_LoadedSaveFilename;
            string bakFileFullname = orgFileFullname + ".bak";
            File.Copy(orgFileFullname, bakFileFullname, true);
            Console.WriteLine("Metagame save file backed up as '" + m_LoadedSaveFilename + ".bak'.");

            SaveFileWriter writer;
            try
            {
                writer = new();
            }
            catch (Exception _ex)
            {
                Console.WriteLine("SavePilot(): Could not open pilot save file to write: " + _ex);
                return;
            }

            m_LoadedPilot.SaveProfile(writer);
            writer.SaveFile(orgFileFullname);

            Console.WriteLine("Pilot file '" + m_LoadedSaveFilename + "' saved.");
        }


        private void PopulatePilot()
        {
            if (m_LoadedPilot == null)
            {
                ClearPilot();
                return;
            }

            CGame game = m_LoadedPilot;

            string pilotSaveFileStr = "Save Version " + game.VersionNumber + " (" + game.SaveDateTime.ToString("yyyy/MM/dd HH:mm:ss") + ")";
            lbl_Pilot_SaveFile.Text = pilotSaveFileStr;
            lbl_Pilot_SaveFile.ForeColor = Color.Green;

            CProgressionElementPlayer? progression = m_LoadedPilot.Player.ProgressionElement;
            if (progression == null)
                return;

            num_Lvl.Value = progression.Level;
            num_Xp.Value = (decimal)progression.Experience;
            lbl_NextLvlXp.Text = "/ " + progression.GetXPToNextLevel().ToString("#,###");





            PopulateInventory();



            // chosen aptitude

            // stash upgrade level

            var player = game.Player;

            // ===== currency =====;
            foreach (var currency in player.Currencies)
            {
                switch (currency.CurrencyType)
                {
                    case eCurrencyType.Credits:
                        num_Credits.Value = (decimal)currency.Amount;
                        break;
                    case eCurrencyType.Fate:
                        num_Fate.Value = (decimal)currency.Amount;
                        break;
                }
            }







            ActiveControl = null;
        }

        private void PopulateInventory()
        {
            if (m_LoadedPilot == null)
                return;

            list_StorageInventory.SuspendLayout();
            list_StorageInventory.Items.Clear();

            var items = m_LoadedPilot.Player.Inventory.Items;
            items.Sort((_item0, _item1) => { return _item0.RootInventoryIndex - _item1.RootInventoryIndex; });

            List<ListViewItem> lst = new(items.Count);
            foreach (var item in items)
            {
                ListViewItem lvi = new()
                {
                    Text = item.GetFullDisplayName(),
                    ToolTipText = item.GetFullDisplayName(),
                };

                lst.Add(lvi);
            }

            list_StorageInventory.Items.AddRange(lst.ToArray());

            list_StorageInventory.ResumeLayout();
        }






        private string m_LoadedSaveFilename = string.Empty;

        private CGame? m_LoadedPilot;
        private CGame? m_ModdedPilot;

        private int m_SelectedItemIndex = -1;
    }

}
