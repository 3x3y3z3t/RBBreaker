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
            if (m_LoadedGame == null)
            {
                Console.WriteLine("LoadedGame is null (this should not happen).");
                return;
            }

            SavePilot();
        }




        private void num_Lvl_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedGame == null)
                return;

            CProgressionElementPlayer? progression = m_LoadedGame.Player.ProgressionElement;
            if (progression == null)
                return;

            progression.Level = (int)num_Lvl.Value;

            lbl_NextLvlXp.Text = "/ " + progression.GetXPToNextLevel((int)num_Lvl.Value).ToString("#,###");
        }

        private void num_Xp_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedGame == null)
                return;

            CProgressionElementPlayer? progression = m_LoadedGame.Player.ProgressionElement;
            if (progression == null)
                return;

            progression.Experience = (double)num_Xp.Value;
        }

        private void num_Credits_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedGame == null)
                return;

            foreach (var currency in m_LoadedGame.Player.Currencies)
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
            if (m_LoadedGame == null)
                return;

            foreach (var currency in m_LoadedGame.Player.Currencies)
            {
                switch (currency.CurrencyType)
                {
                    case eCurrencyType.Fate:
                        currency.Amount = (double)num_Credits.Value;
                        return;
                }
            }
        }
        #endregion






        private void lbl_SelectedItem_Clear()
        {
            lbl_SelectedItem_Type.Text = Constants.EmptyLabel;
            lbl_SelectedItem_SpecializationName.Text = Constants.EmptyLabel;
            lbl_SelectedItem_Dbg.Text = Constants.EmptyLabel;

            lbl_SelectedItem_Affixes.Text = Constants.EmptyLabel;
        }

        private void lbl_SelectedItem_Populate(CItem _item)
        {
            switch (_item.ItemType)
            {
                case eItemType.Equipment:
                    lbl_SelectedItem_Populate_Equipment((CItemEquipment)_item);
                    return;

            }

            lbl_SelectedItem_Type.Text = _item.ItemType.ToString();
            lbl_SelectedItem_SpecializationName.Text = _item.SpecializationName.ToString();
            lbl_SelectedItem_Dbg.Text = _item.ToString();

            lbl_SelectedItem_Affixes.Text = Constants.EmptyLabel;




        }

        private void lbl_SelectedItem_Populate_Equipment(CItemEquipment _item)
        {
            lbl_SelectedItem_Type.Text = _item.ItemType.ToString() + " (" + _item.EquipmentType.ToString() + ")";
            lbl_SelectedItem_SpecializationName.Text = _item.Rarity.ToString() + " " + _item.SpecializationName.ToString();
            lbl_SelectedItem_Dbg.Text = _item.ToString();

            string affixes = "";
            foreach (var affix in _item.Affixes)
            {
                affixes += affix.ToString() + "\n";
            }

            affixes += "\n" + _item.GetUniqueModifierString();

            lbl_SelectedItem_Affixes.Text = affixes;

        }

        private void list_StorageInventory_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs _evt)
        {
            if (!_evt.IsSelected || m_LoadedGame == null)
            {
                lbl_SelectedItem_Clear();
                return;
            }

            var inventoryItems = m_LoadedGame.Player.Inventory.Items;
            if (_evt.ItemIndex > inventoryItems.Count)
            {
                Console.WriteLine("list_StorageInventory_Click(): Selected index out of range (this should not happen).");
                lbl_SelectedItem_Clear();
                return;
            }

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

            m_LoadedGame = new();
            if (!m_LoadedGame.LoadProfile(reader))
            {
                m_LoadedGame = null;
                m_ModdedGame = null;
                // TODO: buttons;

                Console.WriteLine("LoadPilot(): Could not read pilot from file.");
                return;
            }

            // TODO: clone game;

        }

        private void SavePilot()
        {
            if (m_LoadedGame == null)
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

            m_LoadedGame.SaveProfile(writer);
            writer.SaveFile(orgFileFullname);

            Console.WriteLine("Pilot file '" + m_LoadedSaveFilename + "' saved.");
        }


        private void PopulatePilot()
        {
            if (m_LoadedGame == null)
            {
                ClearPilot();
                return;
            }

            CGame game = m_LoadedGame;

            string pilotSaveFileStr = "Save Version " + game.VersionNumber + " (" + game.SaveDateTime.ToString("yyyy/MM/dd HH:mm:ss") + ")";
            lbl_Pilot_SaveFile.Text = pilotSaveFileStr;
            lbl_Pilot_SaveFile.ForeColor = Color.Green;

            CProgressionElementPlayer? progression = m_LoadedGame.Player.ProgressionElement;
            if (progression == null)
                return;

            num_Lvl.Value = progression.Level;
            num_Xp.Value = (decimal)progression.Experience;
            lbl_NextLvlXp.Text = "/ " + progression.GetXPToNextLevel().ToString("#,###");








            {
                list_StorageInventory.SuspendLayout();
                list_StorageInventory.Items.Clear();

                var items = game.Player.Inventory.Items;
                items.Sort((_item0, _item1) => { return _item0.RootInventoryIndex - _item1.RootInventoryIndex; });

                List<ListViewItem> lst = new(items.Count);
                foreach (var item in items)
                {
                    ListViewItem lvi = new()
                    {
                        Text = item.ItemType.ToString(),
                        ToolTipText = item.ToString(),
                    };

                    lst.Add(lvi);
                }

                list_StorageInventory.Items.AddRange(lst.ToArray());

                list_StorageInventory.ResumeLayout();
            }



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






        private string m_LoadedSaveFilename = string.Empty;

        private CGame? m_LoadedGame;
        private CGame? m_ModdedGame;
    }

}
