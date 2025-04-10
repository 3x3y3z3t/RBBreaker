/*  Form1.cs
 *  Version 1.0 (2025.04.10)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.IO;

namespace ParallelReality
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            m_ModManager = new();

            if (!ReadConfigFile())
            {

            }

            if (m_ModManager.BaseGameDir == string.Empty)
            {
                SelectBaseDirectory();
            }

            AddBaseGameEntryToList();

            if (m_ModManager.BaseGameDir != string.Empty)
            {
                m_TbBaseGameDir.Text = m_ModManager.BaseGameDir;

                m_ModManager.PopulateModInfo();
                Console.WriteLine("Found " + m_ModManager.FoundMods.Count + " mod(s).");




            }

            AddModsToList();

            m_ModManager.BackupBaseGame();



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


                m_ModManager.BaseGameDir = path;
                //m_TbBaseGameDir.Invoke(() => m_TbBaseGameDir.Text = fullname);

                SaveConfigFile();
            }
        }

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

                string key = line[0..pos];
                string value = line[(pos + 1)..commentPos];

                if (key == "BaseGameDir")
                {
                    m_ModManager.BaseGameDir = value;
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


            stream.WriteLine("BaseGameDir=" + m_ModManager.BaseGameDir);

            stream.Flush();
            stream.Close();

            return true;
        }

        private void AddBaseGameEntryToList()
        {

        }

        private void AddModsToList()
        {
            lbl_FoundModsCount.Text = m_ModManager.FoundMods.Count.ToString();

            dgv_ModsList.SuspendLayout();

            dgv_ModsList.ClearSelection();
            dgv_ModsList.Rows.Clear();

            for (int i = 0; i < m_ModManager.FoundMods.Count; ++i)
            {
                ModInfo mod = m_ModManager.FoundMods[i];
                dynamic d = new dynamic[] {
                    i,
                    mod.Name,
                    mod.Author,
                    mod.ModVersion,
                    mod.GameVersion,
                };
                dgv_ModsList.Rows.Add(d);
                //dgv_ModsList.Rows.Add(d);
            }

            dgv_ModsList.ResumeLayout(true);

            dgv_ModsList.ClearSelection();


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




        private const string c_ConfigFilename = "config.ini";


        private ModManager m_ModManager;

        private void dgv_ModsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //return;


            if (e.RowIndex < 0 || e.RowIndex >= dgv_ModsList.Rows.Count)
                return;

            string? name = dgv_ModsList.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (name == null)
                return;

            //Console.WriteLine(e.RowIndex + ": " + name);

            ModInfo mod = m_ModManager.FoundMods[e.RowIndex];

            string text = "";
            foreach (string file in mod.ModifiedFiles)
            {
                text += "    " + file + "\r\n";
            }

            tb_ModFiles.Text = text;

            lbl_ModName.Text = mod.Name;
            lbl_Author.Text = mod.Author;
            lbl_ModVer.Text = mod.ModVersion;
            lbl_GameVer.Text = mod.GameVersion;





            //if (e.RowIndex == m_DgvGame_CodexList_SelectedRow)
            //    return;

            //m_DgvGame_CodexList_SelectedRow = e.RowIndex;
            //string name = m_DgvGame_CodexList.Rows[e.RowIndex].Cells[0].Value.ToString();

            //var itemSets = m_Game.Player.PlayerProgressionElement.ItemSets;
            //foreach (CItemSet set in itemSets)
            //{
            //    if (set.ProtoName == name)
            //    {
            //        PresentItemSetEntries(set);
            //        return;
            //    }
            //}
        }

        private void btn_SelectDir_Click(object sender, EventArgs e)
        {
            SelectBaseDirectory();
        }

        private void btn_RestoreBaseGame_Click(object sender, EventArgs e)
        {
            m_ModManager.RestoreBaseGame();
        }

        private void btn_ApplyMod_Click(object sender, EventArgs e)
        {
            List<int> selectedMods = new();
            for (int i = 0; i < dgv_SelectedMod.Rows.Count; ++i)
            {
                int index = (int)dgv_SelectedMod.Rows[i].Cells[0].Value;


                //bool b = (bool)dgv_SelectedMod.Rows[i].Cells[0].Value;
                //if (!b)
                //    continue;
                selectedMods.Add(index);
            }

            m_ModManager.ApplyMods(selectedMods);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            int selectedRow = dgv_ModsList.SelectedRows[0].Index;
            if (selectedRow < 0 || selectedRow > dgv_ModsList.Rows.Count)
                return;

            var cells = dgv_ModsList.SelectedRows[0].Cells;
            if (cells == null || cells.Count == 0)
                return;

            dynamic d = new dynamic[]
            {
                cells[0].Value,
                cells[1].Value,
                cells[2].Value,
                cells[3].Value,
                cells[4].Value,
            };
            dgv_SelectedMod.Rows.Add(d);

            dgv_ModsList.Rows.RemoveAt(selectedRow);
        }

        private void btn_Unselect_Click(object sender, EventArgs e)
        {
            int selectedRow = dgv_SelectedMod.SelectedRows[0].Index;
            if (selectedRow < 0 || selectedRow > dgv_SelectedMod.Rows.Count)
                return;

            var cells = dgv_SelectedMod.SelectedRows[0].Cells;
            if (cells == null || cells.Count == 0)
                return;

            dynamic d = new dynamic[]
            {
                cells[0].Value,
                cells[1].Value,
                cells[2].Value,
                cells[3].Value,
                cells[4].Value,
            };
            dgv_ModsList.Rows.Add(d);

            dgv_SelectedMod.Rows.RemoveAt(selectedRow);
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            //MoveModUp(dgv_ModsList);
            MoveModUp(dgv_SelectedMod);
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            //MoveModDown(dgv_ModsList);
            MoveModDown(dgv_SelectedMod);
        }
    }

}
