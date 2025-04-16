/*  Form1.cs
 *  Version 1.6 (2025.04.16)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ParallelReality
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            m_Cache_SelectedMods = new();
            m_Controller = new(this);



        }


        public void NotifyModsFolderChanged()
        {
            if (m_ModsFolderChangesNotified)
                return;

            if (InvokeRequired)
            {
                Invoke(NotifyModsFolderChanged);
                return;
            }

            m_ModsFolderChangesNotified = true;

            lbl_ModsFolderChanged.Visible = true;

            btn_RefreshModList.ForeColor = Color.Red;
            btn_RefreshModList.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        }

        public void ClearModsFolderChangesNotification()
        {
            if (!m_ModsFolderChangesNotified)
                return;

            m_ModsFolderChangesNotified = false;

            lbl_ModsFolderChanged.Visible = false;
            btn_RefreshModList.ForeColor = Control.DefaultForeColor;
            btn_RefreshModList.Font = Control.DefaultFont;
        }


        #region Helpers
        public enum PromptResult_
        {
            OK,
            OK_DirChanged,
            OK_NoDirChanges,
            UnknownError,
            UserCancelled,
            InvalidExecutableLocation
        }

        public PromptResult_ PromptSelectGameExecutable()
        {
            FileDialog dialog = new OpenFileDialog()
            {
                AddExtension = true,
                Filter = "RB Executable|Reality Break.exe",
            };

            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return PromptResult_.UserCancelled;

            string fullname = dialog.FileName;
            if (fullname == string.Empty)
                return PromptResult_.UnknownError;

            string? path = Path.GetDirectoryName(fullname);
            if (path == null)
                return PromptResult_.InvalidExecutableLocation;

            if (!File.Exists(path + "/GameAssembly.dll"))
                return PromptResult_.InvalidExecutableLocation;

            if (Config.BaseGameDir == string.Empty)
            {
                Config.BaseGameDir = path;
                return PromptResult_.OK;
            }

            if (Config.BaseGameDir != path)
            {
                Config.BaseGameDir = path;
                return PromptResult_.OK_DirChanged;
            }

            return PromptResult_.OK_NoDirChanges;
        }

        private void DoRefresh()
        {
            Console.WriteLine("\r\n===== Refresh =====");

            CacheSelectedModsList();

            RevertControlsToDefaultState();
            m_Controller.CancelOperationInProgress(true);

            m_Controller.InitializeModManager();
            PopulateControlsRightAfterModManagerInitialized();

            ClearModsFolderChangesNotification();

            RestoreCachedSelectedModsList();

            ActiveControl = null;
        }

        private async void DoRestoreBaseGame()
        {
            if (m_Controller.ModManager == null || m_Controller.IsBaseGameRestoringInProgress)
                return;

            if (m_Controller.ModManager.GetLoadedMods().Count == 0)
                return;

            lbl_Status.Text = "Restoring Base Game..";
            lbl_Status.Visible = true;

            await Task.Run(() => m_Controller.DoRestoreBaseGame(CallbackUpdateStatusLabel));

            RefreshFoundModsList();
            RefreshAppliedModsList();

            dgv_FoundMods.Enabled = true;
            dgv_SelectedMods.Enabled = true;

            btn_RestoreBaseGame.Enabled = false;

            lbl_Status.Text = "Base Game restored.";

            ActiveControl = null;

            await Task.Delay(2000);
            lbl_Status.Visible = false;
        }

        private async void DoApplyMods(List<int> _selectedMods)
        {
            if (m_Controller.ModManager == null)
                return;

            btn_ApplyMod.Text = "Cancel (NO Rollback!)";

            lbl_Status.Text = "";
            lbl_Status.Visible = true;

            try
            {
                await Task.Run(() => m_Controller.DoApplyMods(_selectedMods, CallbackUpdateStatusLabel));
                //await Task.Run(() => m_Coordinator.ModManager.ApplyMods(_selectedMods, CallbackUpdateStatusLabel, cancellationToken), cancellationToken);

                string text = lbl_Status.Text[0..(lbl_Status.Text.Length - 2)];
                lbl_Status.Text += " (Done).";
            }
            catch (OperationCanceledException)
            {
                lbl_Status.Text = "Cancelled.";
            }

            btn_RestoreBaseGame.Enabled = true;
            btn_ApplyMod.Text = "Apply Selected Mod(s)";

            await Task.Delay(2000, CancellationToken.None);
            lbl_Status.Visible = false;
        }

        private async void DoCancelApplyMods()
        {
            m_Controller.CancelOperationInProgress();

            btn_ApplyMod.Text = "Apply Selected Mod(s)";

            await Task.Delay(2000);
            lbl_Status.Visible = false;
        }

        private void MoveModUp(DataGridView _dgv)
        {
            if (_dgv.SelectedRows.Count == 0)
                return;

            int selectedRow = _dgv.SelectedRows[0].Index;
            if (selectedRow < 1 || selectedRow >= _dgv.Rows.Count)
                return;

            var row = _dgv.Rows[selectedRow];
            _dgv.Rows.RemoveAt(selectedRow);
            _dgv.Rows.Insert(selectedRow - 1, row);

            _dgv.ClearSelection();
            _dgv.Rows[selectedRow - 1].Selected = true;
        }

        private void MoveModDown(DataGridView _dgv)
        {
            if (_dgv.SelectedRows.Count == 0)
                return;

            int selectedRow = _dgv.SelectedRows[0].Index;
            if (selectedRow < 0 || selectedRow >= _dgv.Rows.Count - 1)
                return;

            var row = _dgv.Rows[selectedRow];
            _dgv.Rows.RemoveAt(selectedRow);
            _dgv.Rows.Insert(selectedRow + 1, row);

            _dgv.ClearSelection();
            _dgv.Rows[selectedRow + 1].Selected = true;
        }
        #endregion





























        //private async Task DoApplyMods(List<int> _selectedMods, CancellationToken _cancellationToken)
        //{
        //    lbl_Status.Text = "";
        //    lbl_Status.Visible = true;

        //    try
        //    {
        //        await Task.Run(() => m_ModManager?.ApplyMods(_selectedMods, UpdateProgressLabel, _cancellationToken), _cancellationToken);

        //        string text = lbl_Status.Text[0..(lbl_Status.Text.Length - 2)];
        //        lbl_Status.Text += " (Done).";
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        Console.WriteLine("    Cancelled.");
        //        lbl_Status.Text = "Cancelled.";
        //    }

        //    btn_ApplyMod.Text = "Apply Selected Mod(s)";

        //    await Task.Delay(2000, CancellationToken.None);
        //    lbl_Status.Visible = false;
        //}



        private void AddBaseGameEntryToList()
        {

        }

        private void ClearSelectionOnList(DataGridView _dgv)
        {
            _dgv.ClearSelection();






        }


        private void UpdateProgressLabel(int _copiedFiles, int _totalFiles)
        {
            string text = "Status: " + _copiedFiles + "/" + _totalFiles + " files copied.";
            if (lbl_Status.InvokeRequired)
            {
                lbl_Status.Invoke(() => lbl_Status.Text = text);
            }
            else
            {
                lbl_Status.Text = text;
            }
        }




        private const string c_ConfigFilename = "config.ini";


























        // ==================================================
        // EVENT HANDLER
        // ==================================================
        #region Event Handler

        private void Form1_Shown(object sender, EventArgs e)
        {
            RevertControlsToDefaultState();

            if (!m_Controller.InitialCheckBaseGameDir())
                return;

            // =====
            m_Controller.InitializeModManager();

            m_TbBaseGameDir.Text = Config.BaseGameDir;
            PopulateControlsRightAfterModManagerInitialized();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Controller.Shutdown();
        }

        #region Button
        private void btn_SelectDir_Click(object sender, EventArgs e)
        {
            PromptResult_ result = PromptSelectGameExecutable();
            if (Config.BaseGameDir == string.Empty)
            {
                m_Controller.CancelOperationInProgress(true);
                RevertControlsToDefaultState();
                return;
            }

            if (result != PromptResult_.OK && result != PromptResult_.OK_DirChanged)
            {
                return;
            }

            m_Controller.CancelOperationInProgress(true);

            Config.SaveConfigFile();

            m_Controller.InitializeModManager();

            m_TbBaseGameDir.Text = Config.BaseGameDir;
            PopulateControlsRightAfterModManagerInitialized();
        }

        private void btn_RefreshModList_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void btn_OpenReadme_Click(object sender, EventArgs e)
        {
            if (m_Controller.ModManager == null)
                return;

            int index = -1;
            if (dgv_FoundMods.SelectedRows.Count != 0)
            {
                int selectedRow = dgv_FoundMods.SelectedRows[0].Index;
                index = (int)dgv_FoundMods.Rows[selectedRow].Cells[0].Value;
            }
            if (dgv_SelectedMods.SelectedRows.Count != 0)
            {
                int selectedRow = dgv_SelectedMods.SelectedRows[0].Index;
                index = (int)dgv_SelectedMods.Rows[selectedRow].Cells[0].Value;
            }

            if (index == -1)
                return;

            ModInfo mod = m_Controller.ModManager.FoundMods[index];
            if (mod.ReadmeFileFullname == string.Empty)
                return;

            Process? proc = Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = mod.ReadmeFileFullname,
            });
        }
        private void btn_RestoreBaseGame_Click(object sender, EventArgs e)
        {
            DoRestoreBaseGame();
        }

        private void btn_ApplyMods_Click(object sender, EventArgs e)
        {
            if (m_Controller.ModManager == null || m_Controller.IsBaseGameRestoringInProgress)
                return;

            if (m_Controller.IsModsApplyingInProgress)
            {
                DoCancelApplyMods();
                return;
            }

            if (m_ModsFolderChangesNotified)
            {
                string caption = "Mods Folder Changed";
                string message = "Mods folder has changed since the last refresh.\n\n" +
                    "Click Abort to abort.\n" +
                    "Click Retry to refresh mods list now.\n" +
                    "Click Ignore to ignore and apply mods anyway (and potentially break things).\n\n" +
                    "If I have time I will redesign this message box.";
                DialogResult result = MessageBox.Show(this, message, caption, MessageBoxButtons.AbortRetryIgnore);

                if (result == DialogResult.Abort)
                    return;

                if (result == DialogResult.Retry)
                {
                    DoRefresh();
                    return;
                }

                if (result == DialogResult.Ignore)
                {
                    // do nothing;
                }
            }

            // =====
            List<int> selectedMods = new();
            foreach (DataGridViewRow row in dgv_SelectedMods.Rows)
            {
                if (row.Cells.Count == 0)
                    continue;

                int index = (int)row.Cells[0].Value;
                selectedMods.Add(index);
            }

            if (selectedMods.Count == 0)
            {
                Console.WriteLine("No mod selected. Restore to Base Game instead.");
                DoRestoreBaseGame();
                return;
            }

            DoApplyMods(selectedMods);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            _ = MoveItemBetweenDGV(dgv_FoundMods, dgv_SelectedMods);
            m_FoundModsListSelectingRow = -1;

            UpdateApplyModsButton();

            UpdateMoveUpDownButtons();

            btn_Select.Enabled = false;
            btn_Unselect.Enabled = true;
        }

        private void btn_Unselect_Click(object sender, EventArgs e)
        {
            _ = MoveItemBetweenDGV(dgv_SelectedMods, dgv_FoundMods);
            m_SelectedModsListSelectingRow = -1;

            UpdateApplyModsButton();

            UpdateMoveUpDownButtons();

            btn_Select.Enabled = true;
            btn_Unselect.Enabled = false;
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            MoveModUp(dgv_SelectedMods);
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            MoveModDown(dgv_SelectedMods);
        }
        #endregion // Button

        #region DataGridView
        private void dgv_FoundMods_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (m_FoundModsListSelectingRow == -1)
            {
                dgv_FoundMods.ClearSelection();
            }

            UpdateMoveUpDownButtons();
        }

        private void dgv_FoundMods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_FoundMods.Rows.Count)
                return;

            m_FoundModsListSelectingRow = e.RowIndex;
            if (HandlerWhenClickOnList(dgv_FoundMods))
            {
                dgv_SelectedMods.ClearSelection();
                m_SelectedModsListSelectingRow = -1;

                UpdateMoveUpDownButtons();

                btn_Select.Enabled = true;
                btn_Unselect.Enabled = false;
            }
        }

        private void dgv_FoundMods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_FoundMods.Rows.Count)
                return;

            _ = MoveItemBetweenDGV(dgv_FoundMods, dgv_SelectedMods);
            m_FoundModsListSelectingRow = -1;

            UpdateApplyModsButton();

            UpdateMoveUpDownButtons();

            btn_Select.Enabled = false;
            btn_Unselect.Enabled = true;
        }

        private void dgv_SelectedMods_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (m_SelectedModsListSelectingRow == -1)
            {
                dgv_SelectedMods.ClearSelection();
            }

            UpdateMoveUpDownButtons();
        }

        private void dgv_SelectedMods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_SelectedMods.Rows.Count)
                return;

            m_SelectedModsListSelectingRow = e.RowIndex;
            if (HandlerWhenClickOnList(dgv_SelectedMods))
            {
                dgv_FoundMods.ClearSelection();
                m_FoundModsListSelectingRow = -1;

                UpdateMoveUpDownButtons();

                btn_Select.Enabled = false;
                btn_Unselect.Enabled = true;
            }
        }

        private void dgv_SelectedMods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_SelectedMods.Rows.Count)
                return;

            _ = MoveItemBetweenDGV(dgv_SelectedMods, dgv_FoundMods);
            m_SelectedModsListSelectingRow = -1;

            UpdateApplyModsButton();

            UpdateMoveUpDownButtons();

            btn_Select.Enabled = true;
            btn_Unselect.Enabled = false;
        }
        #endregion // DataGridView






        #endregion
























        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "https://discord.com/channels/639225872305487907/1361835527262699580/1361835527262699580",
                UseShellExecute = true,
            });

        }


        private readonly List<ModInfoSimple> m_Cache_SelectedMods;

        private bool m_ModsFolderChangesNotified = false;
    }

}
