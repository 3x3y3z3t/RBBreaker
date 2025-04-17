/*  Form1.UIHelper.cs
 *  Version 1.1 (2025.04.17)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelReality
{
    public partial class Form1
    {
        /** Always call this method on UI thread! */
        private void RevertControlsToDefaultState()
        {
            dgv_FoundMods.Rows.Clear();
            dgv_FoundMods.ClearSelection();

            dgv_SelectedMods.Rows.Clear();
            dgv_SelectedMods.ClearSelection();

            btn_RefreshModList.Enabled = false;
            btn_OpenReadme.Enabled = false;
            btn_RestoreBaseGame.Enabled = false;
            btn_ApplyMod.Enabled = false;

            btn_MoveUp.Enabled = false;
            btn_MoveDown.Enabled = false;
            btn_Select.Enabled = false;
            btn_Unselect.Enabled = false;

            lbl_Status.Text = "_";
            lbl_Status.Visible = false;

            lbl_ModName.Text = "_";
            lbl_Author.Text = "_";
            lbl_ModVer.Text = "_";
            lbl_GameVer.Text = "_";

            tb_ModFiles.Text = "";

            ActiveControl = null;
        }

        /// <summary>Always call this method in UI thread!</summary>
        private void PopulateControlsRightAfterModManagerInitialized()
        {
            RefreshFoundModsList();
            RefreshAppliedModsList();

            btn_RefreshModList.Enabled = true;

            if (m_Controller.ModManager != null && m_Controller.ModManager.IsGameModded)
            {
                btn_RestoreBaseGame.Enabled = true;
                btn_ApplyMod.Enabled = true;
            }

            ActiveControl = null;
        }

        /// <summary>Always call this method in UI thread!</summary>
        private void RefreshFoundModsList()
        {
            if (m_Controller.ModManager == null)
            {
                throw new NullReferenceException("ModManager is null (this should not happen).");
            }

            var list = m_Controller.ModManager.FoundMods;

            lbl_FoundModsCount.Text = list.Count.ToString();

            dgv_FoundMods.Rows.Clear();

            for (int i = 0; i < list.Count; ++i)
            {
                ModInfo mod = list[i];
                if (mod.LoadedOrder != -1)
                    continue;

                dynamic d = new dynamic[] {
                    mod.ModId,
                    mod.Name,
                    mod.Author,
                    mod.ModVersion,
                    mod.GameVersion,
                };
                dgv_FoundMods.Rows.Add(d);
            }

            dgv_FoundMods.ClearSelection();
        }

        /// <summary>Always call this method in UI thread!</summary>
        private void RefreshAppliedModsList()
        {
            if (m_Controller.ModManager == null)
            {
                throw new NullReferenceException("ModManager is null (this should not happen).");
            }

            var list = m_Controller.ModManager.GetLoadedMods();

            //lbl_LoadedModsCount.Text = list.Count.ToString();

            dgv_SelectedMods.Rows.Clear();
            m_SelectedModsListPauseRowChangesEvent = true;

            for (int i = 0; i < list.Count; ++i)
            {
                ModInfo mod = list[i];

                dynamic d = new dynamic[] {
                    mod.ModId,
                    mod.Name,
                    mod.Author,
                    mod.ModVersion,
                    mod.GameVersion,
                };
                dgv_SelectedMods.Rows.Add(d);
            }

            dgv_SelectedMods.ClearSelection();
            m_SelectedModsListPauseRowChangesEvent = false;

            NotifyModsOverrideEachOther();
        }

        private void CacheSelectedModsList()
        {
            for (int i = 0; i < dgv_SelectedMods.Rows.Count; ++i)
            {
                var cells = dgv_SelectedMods.Rows[i].Cells;
                if (cells.Count == 0)
                    continue;

                int id = (int)cells[0].Value;

                ModInfo? mod = m_Controller.ModManager?.GetModWithId(id);
                if (mod == null)
                    continue;

                int order = mod.LoadedOrder;
                if (order == -1)
                    order = i;

                m_Cache_SelectedMods.Add(new()
                {
                    Hash = mod.GetShortHash(),
                    Name = mod.Name,
                    LoadedOrder = order,
                });
            }
        }

        private void RestoreCachedSelectedModsList()
        {
            if (m_Cache_SelectedMods.Count == 0)
                return;

            // clear Selected list;
            dgv_SelectedMods.Rows.Clear();

            // add cached items to Selected list;
            foreach (var item in m_Cache_SelectedMods)
            {
                ModInfo? mod = m_Controller.ModManager?.GetModWithShortHash(item.Hash);
                if (mod == null)
                    continue;

                dynamic d = new dynamic[] {
                    mod.ModId,
                    mod.Name,
                    mod.Author,
                    mod.ModVersion,
                    mod.GameVersion,
                };
                dgv_SelectedMods.Rows.Add(d);
            }

            // remove cached items on Found list;
            for (int i = 0; i < dgv_FoundMods.Rows.Count; ++i)
            {
                var cells = dgv_FoundMods.Rows[i].Cells;
                if (cells.Count == 0)
                    continue;

                int id = (int)cells[0].Value;

                ModInfo? mod = m_Controller.ModManager?.GetModWithId(id);
                if (mod == null)
                    continue;

                string hash = mod.GetShortHash();
                foreach (var item in m_Cache_SelectedMods)
                {
                    if (item.Hash == hash)
                    {
                        dgv_FoundMods.Rows.RemoveAt(i);
                        --i;
                        m_Cache_SelectedMods.Remove(item);
                        break;
                    }
                }
            }

            m_Cache_SelectedMods.Clear();

            dgv_SelectedMods.ClearSelection();
        }

        /// <summary>Always call this method in UI thread!<br />This method return true if the click success.</summary>
        private bool HandlerWhenClickOnList(DataGridView _dgv)
        {
            if (_dgv.SelectedRows.Count == 0)
                return false;

            var cells = _dgv.SelectedRows[0].Cells;
            if (cells.Count == 0)
                return false;

            if (m_Controller.ModManager == null)
                return true;

            int index = (int)cells[0].Value;
            ModInfo mod = m_Controller.ModManager.FoundMods[index];
            RefreshModInfoDisplay(mod);

            return true;
        }

        /// <summary>Always call this method in UI thread!</summary>
        private void RefreshModInfoDisplay(ModInfo _mod)
        {
            btn_OpenReadme.Enabled = _mod.ReadmeFileFullname != string.Empty;

            lbl_ModName.Text = _mod.Name;
            lbl_Author.Text = _mod.Author;
            lbl_ModVer.Text = _mod.ModVersion;
            lbl_GameVer.Text = _mod.GameVersion;

            string text = "";
            foreach (string file in _mod.ModFiles)
            {
                text += "    " + file + "\r\n";
            }

            tb_ModFiles.Text = text;
        }

        /// <summary>Always call this method in UI thread!</summary>
        private void UpdateArrowButtons(DataGridView _dgv)
        {
            if (_dgv.SelectedRows.Count == 0)
                return;

            int index = _dgv.SelectedRows[0].Index;
            if (index < 0)
                return;

            btn_MoveUp.Enabled = index != 0;
            btn_MoveDown.Enabled = index != _dgv.Rows.Count - 1;
        }

        /// <summary>Always call this method in UI thread!</summary>
        private void UpdateMoveUpDownButtons()
        {
            bool enableUp = false;
            bool enableDown = false;

            bool correctList = dgv_SelectedMods.SelectedRows.Count != 0;
            if (correctList)
            {
                int index = dgv_SelectedMods.SelectedRows[0].Index;

                enableUp = correctList && index > 0;
                enableDown = correctList && index < dgv_SelectedMods.Rows.Count - 1;
            }

            btn_MoveUp.Enabled = enableUp;
            btn_MoveDown.Enabled = enableDown;
        }

        private void UpdateApplyModsButton()
        {
            if (m_Controller.IsModsApplyingInProgress)
                return;

            btn_ApplyMod.Enabled = dgv_SelectedMods.Rows.Count != 0;












        }



        private void CallbackUpdateStatusLabel(string _text)
        {
            if (lbl_Status.InvokeRequired)
            {
                lbl_Status.Invoke(() => lbl_Status.Text = _text);
            }
            else
            {
                lbl_Status.Text = _text;
            }
        }


        private static bool MoveItemBetweenDGV(DataGridView _src, DataGridView _dst)
        {
            if (_src.SelectedRows.Count == 0)
                return false;

            int selected = _src.SelectedRows[0].Index;
            if (selected < 0 || selected > _src.Rows.Count)
                return false;

            var cells = _src.SelectedRows[0].Cells;
            if (cells == null || cells.Count == 0)
                return false;

            dynamic d = new dynamic[]
            {
                cells[0].Value,
                cells[1].Value,
                cells[2].Value,
                cells[3].Value,
                cells[4].Value,
            };
            _dst.Rows.Add(d);
            _dst.Rows[^1].Cells[0].Selected = true;

            _src.Rows.RemoveAt(selected);
            _src.ClearSelection();

            return true;
        }















        //private ModManager? m_ModManager = null;

        //private CancellationTokenSource? m_ApplyModsCancellationTokenSource;

        private int m_FoundModsListSelectingRow = -1;
        private int m_SelectedModsListSelectingRow = -1;

        private bool m_SelectedModsListPauseRowChangesEvent = false;

        //private bool m_IsModsApplyingInProgress = false;
    }

}
