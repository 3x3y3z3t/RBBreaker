/*  Form1.cs
 *  Version 1.5 (2025.04.13)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.Diagnostics;
using System.IO;

namespace ParallelReality
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



            if (!ReadConfigFile())
            {
                Console.WriteLine("Config file not found. A new file will be created soon.");
            }

            if (Config.BaseGameDir == string.Empty)
            {
                SelectBaseDirectory();
            }

            if (Config.BaseGameDir != string.Empty)
            {
                m_TbBaseGameDir.Text = Config.BaseGameDir;

                m_ModManager = new();
                m_ModManager.BaseGameDir = Config.BaseGameDir;

                m_ModManager.CheckForDownloadedMods();
                m_ModManager.CheckForAppliedMods();

                RefreshFoundModsList();
                RefreshAppliedModsList();

                if (!m_ModManager.IsGameModded)
                {
                    m_ModManager.BackupBaseGame();
                }
            }

            SaveConfigFile();



        }



        private void SelectBaseDirectory()
        {
            FileDialog dialog = new OpenFileDialog()
            {
                AddExtension = true,
                Filter = "RB Executable|Reality Break.exe",
                //InitialDirectory = initialDir,
            };

            //string initialDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", "Element Games", "Reality Break");


            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.Cancel)
            {

            }

            string fullname = dialog.FileName;
            if (fullname != string.Empty)
            {
                string? path = Path.GetDirectoryName(fullname);
                if (path == null)
                {
                    throw new NotImplementedException();
                }


                Config.BaseGameDir = path;
                //m_TbBaseGameDir.Invoke(() => m_TbBaseGameDir.Text = fullname);
            }
        }

        #region Config File
        /// <summary>
        /// 
        /// </summary>
        /// <returns><c>false</c> if the config file does not exist, or config couldn't be read.</returns>
        private bool ReadConfigFile()
        {
            if (!File.Exists(c_ConfigFilename))
            {
                StreamWriter stream = File.CreateText(c_ConfigFilename);
                stream.Close();
                return false;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(c_ConfigFilename);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("Couldn't open config file: " + _ex);
                return false;
            }

            if (lines.Length == 0)
                return false;

            foreach (string line in lines)
            {
                int commentPos = line.IndexOf("#");
                if (commentPos == 0)
                    continue;
                if (commentPos == -1)
                    commentPos = line.Length;

                int pos = line.IndexOf('=');
                if (pos == -1)
                    continue;

                if (commentPos < pos)
                    continue;

                // ==========
                string key = line[0..pos];
                string value = line[(pos + 1)..commentPos];

                if (key == "BaseGameDir")
                {
                    Config.BaseGameDir = value;
                }


            }




            return true;
        }

        private bool SaveConfigFile()
        {
            StreamWriter stream;

            try
            {
                stream = new StreamWriter(File.Open(c_ConfigFilename, FileMode.Truncate, FileAccess.Write));
            }
            catch (Exception _ex)
            {
                Console.WriteLine("Couldn't open config file: " + _ex);
                return false;
            }

            stream.WriteLine("BaseGameDir=" + Config.BaseGameDir);

            stream.Flush();
            stream.Close();

            return true;
        }
        #endregion

        private async void DoRestoreBaseGame()
        {
            lbl_Status.Text = "Restoring Base Game..";
            lbl_Status.Visible = true;

            await Task.Run(() => m_ModManager?.RestoreBaseGame());

            RefreshFoundModsList();
            RefreshAppliedModsList();

            lbl_Status.Text = "Base Game restored.";
            await Task.Delay(2000);
            lbl_Status.Visible = false;

        }

        private async Task DoApplyMods(List<int> _selectedMods, CancellationToken _cancellationToken)
        {
            lbl_Status.Text = "";
            lbl_Status.Visible = true;
            try
            {
                await Task.Run(() => m_ModManager?.ApplyMods(_selectedMods, UpdateProgressLabel, _cancellationToken));

                string text = lbl_Status.Text[0..(lbl_Status.Text.Length - 2)];
                lbl_Status.Text += " (Done).";
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("    Cancelled.");
                lbl_Status.Text = "Cancelled.";
            }

            btn_ApplyMod.Text = "Apply Selected Mod(s)";

            await Task.Delay(2000, CancellationToken.None);
            lbl_Status.Visible = false;
        }

        private async void DoCancelApplyMods()
        {
            lbl_Status.Text = "Cancelled.";
            await Task.Delay(2000);
            lbl_Status.Visible = false;
        }

        private void RefreshFoundModsList()
        {
            if (m_ModManager == null || m_ModManager.BaseGameDir == string.Empty)
                return;

            lbl_FoundModsCount.Text = m_ModManager.FoundMods.Count.ToString();

            dgv_FoundMod.ClearSelection();
            dgv_FoundMod.Rows.Clear();

            for (int i = 0; i < m_ModManager.FoundMods.Count; ++i)
            {
                ModInfo mod = m_ModManager.FoundMods[i];
                if (mod.LoadedOrder != -1)
                    continue;

                dynamic d = new dynamic[] {
                    mod.Index,
                    mod.Name,
                    mod.Author,
                    mod.ModVersion,
                    mod.GameVersion,
                };
                dgv_FoundMod.Rows.Add(d);
            }

            dgv_FoundMod.ClearSelection();

        }

        private void RefreshAppliedModsList()
        {
            if (m_ModManager == null || m_ModManager.BaseGameDir == string.Empty)
                return;

            List<ModInfo> modList = m_ModManager.GetLoadedMods();
            //lbl_FoundModsCount.Text = m_ModManager.FoundMods.Count.ToString();

            dgv_SelectedMod.ClearSelection();
            dgv_SelectedMod.Rows.Clear();

            for (int i = 0; i < modList.Count; ++i)
            {
                ModInfo mod = modList[i];

                dynamic d = new dynamic[] {
                    mod.Index,
                    mod.Name,
                    mod.Author,
                    mod.ModVersion,
                    mod.GameVersion,
                };
                dgv_SelectedMod.Rows.Add(d);
            }

            dgv_SelectedMod.ClearSelection();
        }

        private void AddBaseGameEntryToList()
        {

        }

        private void RefreshModInfoDisplay(ModInfo _mod)
        {
            btn_OpenReadme.Invoke(() => btn_OpenReadme.Enabled = _mod.ReadmeFileFullname != string.Empty);

            lbl_ModName.Invoke(() => lbl_ModName.Text = _mod.Name);
            lbl_Author.Invoke(() => lbl_Author.Text = _mod.Author);
            lbl_ModVer.Invoke(() => lbl_ModVer.Text = _mod.ModVersion);
            lbl_GameVer.Invoke(() => lbl_GameVer.Text = _mod.GameVersion);

            string text = "";
            foreach (string file in _mod.ModifiedFiles)
            {
                text += "    " + file + "\r\n";
            }

            tb_ModFiles.Invoke(() => tb_ModFiles.Text = text);


        }

        // return true if the click success;
        private bool HandlerWhenClickOnList(DataGridView _dgv)
        {
            if (_dgv.SelectedRows.Count == 0)
                return false;

            var cells = _dgv.SelectedRows[0].Cells;
            if (cells.Count == 0)
                return false;

            if (m_ModManager == null)
                return true;

            int index = (int)cells[0].Value;
            ModInfo mod = m_ModManager.FoundMods[index];
            RefreshModInfoDisplay(mod);

            return true;
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


        private ModManager? m_ModManager = null;

        private CancellationTokenSource m_ApplyModsCancellationToken;
        //private readonly System.Windows.Forms.Timer m_StatusUpdateTimer = new System.Windows.Forms.Timer();







        // ==================================================
        // EVENT HANDLER
        // ==================================================
        #region Event Handler

        #region Button
        #endregion

        #region DataGridView
        private void dgv_FoundMod_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv_FoundMod.ClearSelection();
        }

        private void dgv_FoundMod_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_FoundMod.Rows.Count)
            {
                dgv_FoundMod.ClearSelection();
                return;
            }

            if (HandlerWhenClickOnList(dgv_FoundMod))
            {
                dgv_SelectedMod.ClearSelection();
            }
        }

        private void dgv_FoundMod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_FoundMod.Rows.Count)
            {
                dgv_FoundMod.ClearSelection();
                return;
            }

            _ = MoveItemBetweenDGV(dgv_FoundMod, dgv_SelectedMod);
        }

        private void dgv_SelectedMod_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv_SelectedMod.ClearSelection();
        }

        private void dgv_SelectedMod_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_SelectedMod.Rows.Count)
            {
                dgv_SelectedMod.ClearSelection();
                return;
            }

            if (HandlerWhenClickOnList(dgv_SelectedMod))
            {
                dgv_FoundMod.ClearSelection();
            }
        }

        private void dgv_SelectedMod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_SelectedMod.Rows.Count)
            {
                dgv_SelectedMod.ClearSelection();
                return;
            }

            _ = MoveItemBetweenDGV(dgv_SelectedMod, dgv_FoundMod);
        }
        #endregion


        #endregion



        private void Form1_Shown(object sender, EventArgs e)
        {
            dgv_FoundMod.ClearSelection();
            dgv_SelectedMod.ClearSelection();

            btn_OpenReadme.Enabled = false;
            lbl_Status.Visible = false;

            ActiveControl = null;
        }

        private void btn_RefreshModList_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            m_ModManager.CheckForDownloadedMods();
            m_ModManager.CheckForAppliedMods();

            RefreshFoundModsList();
            RefreshAppliedModsList();
        }

        private void btn_SelectDir_Click(object sender, EventArgs e)
        {
            SelectBaseDirectory();
            SaveConfigFile();

            if (m_ModManager == null)
                return;

            m_ModManager.BaseGameDir = Config.BaseGameDir;
        }

        private void btn_OpenReadme_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            int index = -1;
            if (dgv_FoundMod.SelectedRows.Count != 0)
            {
                int selectedRow = dgv_FoundMod.SelectedRows[0].Index;
                index = (int)dgv_FoundMod.Rows[selectedRow].Cells[0].Value;
            }
            else if (dgv_SelectedMod.SelectedRows.Count != 0)
            {
                int selectedRow = dgv_SelectedMod.SelectedRows[0].Index;
                index = (int)dgv_SelectedMod.Rows[selectedRow].Cells[0].Value;
            }

            if (index == -1)
                return;

            ModInfo mod = m_ModManager.FoundMods[index];
            if (mod.ReadmeFileFullname != string.Empty)
            {
                Process? proc = Process.Start(new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = mod.ReadmeFileFullname,
                });
            }
        }

        private void btn_RestoreBaseGame_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            DoRestoreBaseGame();
        }

        private async void btn_ApplyMod_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            if (m_ModManager.IsApplyInProgress)
            {
                m_ApplyModsCancellationToken.Cancel();

                DoCancelApplyMods();
                btn_ApplyMod.Text = "Apply Selected Mod(s)";
            }
            else
            {
                List<int> selectedMods = new();
                for (int i = 0; i < dgv_SelectedMod.Rows.Count; ++i)
                {
                    int index = (int)dgv_SelectedMod.Rows[i].Cells[0].Value;

                    selectedMods.Add(index);
                }

                if (selectedMods.Count > 0)
                {
                    m_ApplyModsCancellationToken = new CancellationTokenSource();

                    btn_ApplyMod.Text = "Cancel (NO Rollback!)";
                    await DoApplyMods(selectedMods, m_ApplyModsCancellationToken.Token);
                    m_ApplyModsCancellationToken.Dispose();
                }
                else
                {
                    Console.WriteLine("No mod selected. Restore to Base Game instead.");
                    DoRestoreBaseGame();
                }
            }
        }

        private bool MoveItemBetweenDGV(DataGridView _src, DataGridView _dst)
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

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            _ = MoveItemBetweenDGV(dgv_FoundMod, dgv_SelectedMod);

            //if (dgv_ModsList.SelectedRows.Count == 0)
            //    return;

            //int selectedRow = dgv_ModsList.SelectedRows[0].Index;
            //if (selectedRow < 0 || selectedRow > dgv_ModsList.Rows.Count)
            //    return;

            //var cells = dgv_ModsList.SelectedRows[0].Cells;
            //if (cells == null || cells.Count == 0)
            //    return;

            //dynamic d = new dynamic[]
            //{
            //    cells[0].Value,
            //    cells[1].Value,
            //    cells[2].Value,
            //    cells[3].Value,
            //    cells[4].Value,
            //};
            //dgv_SelectedMod.Rows.Add(d);
            //dgv_SelectedMod.Rows[dgv_SelectedMod.Rows.Count - 1].Cells[0].Selected = true;

            //dgv_ModsList.Rows.RemoveAt(selectedRow);
            //dgv_ModsList.ClearSelection();
        }

        private void btn_Unselect_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            _ = MoveItemBetweenDGV(dgv_SelectedMod, dgv_FoundMod);

            //if (dgv_SelectedMod.SelectedRows.Count == 0)
            //    return;

            //int selectedRow = dgv_SelectedMod.SelectedRows[0].Index;
            //if (selectedRow < 0 || selectedRow > dgv_SelectedMod.Rows.Count)
            //    return;

            //var cells = dgv_SelectedMod.SelectedRows[0].Cells;
            //if (cells == null || cells.Count == 0)
            //    return;

            //dynamic d = new dynamic[]
            //{
            //    cells[0].Value,
            //    cells[1].Value,
            //    cells[2].Value,
            //    cells[3].Value,
            //    cells[4].Value,
            //};
            //dgv_ModsList.Rows.Add(d);
            //dgv_ModsList.Rows[dgv_ModsList.Rows.Count - 1].Cells[0].Selected = true;

            //dgv_SelectedMod.Rows.RemoveAt(selectedRow);
            //dgv_SelectedMod.ClearSelection();
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            //MoveModUp(dgv_ModsList);
            MoveModUp(dgv_SelectedMod);
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            if (m_ModManager == null)
                return;

            //MoveModDown(dgv_ModsList);
            MoveModDown(dgv_SelectedMod);
        }

    }

    internal static class Config
    {
        public static string BaseGameDir = string.Empty;
    }

}
