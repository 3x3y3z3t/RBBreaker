using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using RBSaveEditor.GameClone;

namespace RBSaveEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Text = "RB Save Editor v0.2";

        }







        #region Event Handlers
        private void btn_SelectDir_Click(object sender, EventArgs e)
        {

        }
        #endregion







        private void Form1_Load(object sender, EventArgs e)
        {
            uc_ItemCard.ClearItemCard();



            TalentManager.Init();



            tabControl_Main.SelectedTab = tab_Pilot;
            list_StorageInventory.Items.Clear();
            combo_Meta_Tier.SelectedIndex = -1;

            //uc_ItemCard.Invalidate();





            tb_Meta_FunnyChar.Text = CMetagame.FunnyChar.ToString();




            string pilotFullname = "C:\\Users\\TOWER\\AppData\\LocalLow\\Element Games\\Reality Break\\Reality Break pilot 1.sav";





            LoadMetagame();
            PopulateMetagame();


            LoadPilot("Reality Break pilot 1.sav");
            PopulatePilot();



            return;



            string metagameFullname = "C:\\Users\\TOWER\\AppData\\LocalLow\\Element Games\\Reality Break\\Reality Break progression.sav";


            lbl_Pilot_SaveFile.ForeColor = Color.Red;
            lbl_Pilot_SaveFile.Text = "Not Loaded";


            CGame game = new();
            game.LoadMetagame(metagameFullname);

            var metagame = game.Metagame;
            //CMetagame.FunnyChar = '\0';

            //Console.WriteLine("Unused Bytes: " + CMetagame.UnusedBytes);

            //game.SaveMetagame("Reality Break progression.sav");




            Stopwatch sw = Stopwatch.StartNew();
            PopulateMetagame();
            sw.Stop();

            TimeSpan elapsed = sw.Elapsed;
            string elapsedStr = string.Format("{0:0}s {1:000}ms", (int)elapsed.TotalSeconds, elapsed.Milliseconds);
            Console.WriteLine("Populating Metagame panel took " + elapsedStr + ".");

            List<string> lst = new()
            {
                "s1", "s2", "s3", "s4"
            };

            List<string> lst2 = new(lst);

            lst2[0] = "asd";

            int z = 0;
        }



        private void ClearMetagamePanel()
        {
            lbl_Meta_SaveFile.Text = "Not Found";
            lbl_Meta_SaveFile.ForeColor = Color.Red;

            cb_Meta_Unlocked.Enabled = false;
            cb_Meta_Unlocked.Checked = false;

            num_Meta_TalentPoints.Enabled = false;
            num_Meta_TalentPoints.Value = 0;

            // ===== tier levels =====;
            combo_Meta_Tier.Enabled = false;
            combo_Meta_Tier.SelectedIndex = -1;

            dgv_Meta_TalentLevels.Rows.Clear();
            dgv_Meta_TalentLevels.Enabled = false;






            lbl_Meta_CompletedMetagameNPESteps.Text = Constants.ZeroOverZero;
            lbl_Meta_CompletedTimelines.Text = Constants.Zero;
            lbl_Meta_NumTimesRealityBroken.Text = Constants.Zero;

            lbl_Meta_DialogueSeenInfosMap.Text = Constants.EmptyLabel;

            lbl_Meta_EverEncounteredEnemies.Text = Constants.Zero;
            lbl_Meta_CriteriaEverFulfilled.Text = Constants.Zero;

            lbl_Meta_UnlockedAptitude.Text = Constants.Zero;
            lbl_Meta_AdditionalSetFlags.Text = Constants.Zero;

            cb_Meta_CallAttentionToEntityRewriteTalent.Enabled = false;
            cb_Meta_CallAttentionToEntityRewriteTalent.Checked = false;

            cb_Meta_CallAttentionToDialogueRewriteTalent.Enabled = false;
            cb_Meta_CallAttentionToDialogueRewriteTalent.Checked = false;

            lbl_Meta_CallAttentionToNewAptitudes.Text = Constants.Zero;




        }

        private void PopulateMetagame()
        {
            if (m_ModdedMetagame == null)
            {
                ClearMetagamePanel();
                return;
            }


            CMetagame mgame = m_ModdedMetagame;

            string metagameSaveFileStr = "Save Version " + mgame.VersionNumber + " (" + mgame.SaveDateTime.ToString("yyyy/MM/dd HH:mm:ss") + ")";
            lbl_Meta_SaveFile.Text = metagameSaveFileStr;
            lbl_Meta_SaveFile.ForeColor = Color.Green;

            cb_Meta_Unlocked.Enabled = true;
            cb_Meta_Unlocked.Checked = mgame.Unlocked;

            num_Meta_TalentPoints.Enabled = true;
            num_Meta_TalentPoints.Value = mgame.TalentPoints;

            // ===== talents levels =====;
            combo_Meta_Tier.Enabled = true;
            combo_Meta_Tier.SelectedIndex = 0;

            PopulateTalentsList(dgv_Meta_TalentLevels, mgame.TalentLevels, -1);





            lbl_Meta_CompletedMetagameNPESteps.Text = mgame.CompletedMetagameNPESteps.Count + "/" + Enum.GetValues<eMetagameNPEStep>().Length;
            lbl_Meta_CompletedTimelines.Text = mgame.CompletedTimelines.Count.ToString();
            lbl_Meta_NumTimesRealityBroken.Text = mgame.NumTimesRealityBroken + " times";

            int bcount = mgame.DialogueSeenInfosMap.Count;
            lbl_Meta_DialogueSeenInfosMap.Text = "A BIIIIIIG chunk of " + bcount * 20 * 5 * 100 + " bytes.";

            lbl_Meta_EverEncounteredEnemies.Text = mgame.EverEncounteredEnemies?.Count.ToString();
            lbl_Meta_CriteriaEverFulfilled.Text = mgame.CriteriaEverFulfilled.Count + "/" + Enum.GetValues<eUnlockableCriterion>().Length;

            lbl_Meta_UnlockedAptitude.Text = mgame.UnlockedAptitudeInfos.Count.ToString();
            lbl_Meta_AdditionalSetFlags.Text = mgame.AdditionalSetFlags.Count.ToString();

            cb_Meta_CallAttentionToEntityRewriteTalent.Enabled = true;
            cb_Meta_CallAttentionToEntityRewriteTalent.Checked = mgame.CallAttentionToEntityRewritingTalent;

            cb_Meta_CallAttentionToDialogueRewriteTalent.Enabled = true;
            cb_Meta_CallAttentionToDialogueRewriteTalent.Checked = mgame.CallAttentionToDialogueRewritingTalent;

            lbl_Meta_CallAttentionToNewAptitudes.Text = mgame.CallAttentionToNewAptitudes.Count.ToString();








        }

        private dynamic[] ConstructTalentRow(KeyValuePair<string, uint> _pair)
        {
            int tier = -1;
            string name = _pair.Key;
            uint level = _pair.Value;
            string friendlyName = string.Empty;
            string description = string.Empty;

            if (TalentManager.TryGetValue(_pair.Key, out TalentInfo? talentInfo))
            {

                tier = talentInfo.Tier;
                name = talentInfo.ProtoNameShort;
                friendlyName = talentInfo.FriendlyName;
                description = talentInfo.Description;
            }

            return new dynamic[]
            {
                    tier, name, level, friendlyName, description
            };
        }





        private void ShowNotificationIfSaveVersionMismatch()
        {
            if (m_LoadedMetagame == null || m_LoadedPilot == null)
            {
                lbl_SaveVersionMismatch.Visible = false;
                return;
            }

            lbl_SaveVersionMismatch.Visible = m_LoadedMetagame.VersionNumber != m_LoadedPilot.VersionNumber;
        }




        private void ClearPilot()
        {
            lbl_Pilot_SaveFile.Text = Constants.NotFound;
            lbl_Pilot_SaveFile.ForeColor = Color.Red;
        }





        private void LoadMetagame()
        {
            string fullname = s_SaveFilePath + "/Reality Break progression.sav";

            SaveFileReader reader;
            try
            {
                reader = new(fullname);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("LoadMetagame(): Could not read metagame save file: " + _ex);
                return;
            }

            m_LoadedMetagame = new();
            if (!m_LoadedMetagame.Load(reader))
            {
                m_LoadedMetagame = null;
                m_ModdedMetagame = null;

                // TODO: buttons;

                Console.WriteLine("LoadMetagame(): Could not read metagame from file.");
                return;
            }

            m_ModdedMetagame = (CMetagame?)m_LoadedMetagame.DeepClone();
        }

        private void LoadProfile(string _filename)
        {
            string fullname = s_SaveFilePath + "/" + _filename;

            SaveFileReader reader;
            try
            {
                reader = new(fullname);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("LoadProfile(): Could not read profile save file: " + _ex);
                return;
            }

            m_LoadedPilot = new();
            if (!m_LoadedPilot.LoadProfile(reader))
            {
                m_LoadedPilot = null;
                m_ModdedPilot = null;

                // TODO: buttons;

                Console.WriteLine("LoadProfile(): Could not read profile from file.");
                return;
            }

            m_ModdedPilot = new(m_LoadedPilot);
        }



        CMetagame? m_LoadedMetagame;
        CMetagame? m_ModdedMetagame;







        private static string s_SaveFilePath = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low/Element Games/Reality Break");



        private void tb_Meta_FunnyChar_TextChanged(object sender, EventArgs e)
        {
            string text = tb_Meta_FunnyChar.Text;
            if (text.Length == 0 || text == string.Empty)
                return;

            char @char = text[0];
            uint code = @char;
            if (code > byte.MaxValue)
            {
                tb_Meta_FunnyChar.ForeColor = Color.Red;
                tb_Meta_FunnyCharCode.ForeColor = Color.Red;
                return;
            }

            FunnyCharNoError();

            tb_Meta_FunnyCharCode.Text = code.ToString();
            CMetagame.FunnyChar = @char;
        }

        private void tb_Meta_FunnyCharCode_TextChanged(object sender, EventArgs e)
        {
            string text = tb_Meta_FunnyCharCode.Text;
            if (text.Length == 0 || text == string.Empty)
                return;

            if (!byte.TryParse(text, out byte code))
            {
                FunnyCharError();
                return;
            }

            FunnyCharNoError();
            char @char = (char)code;

            tb_Meta_FunnyChar.Text = @char.ToString();
            CMetagame.FunnyChar = @char;

        }

        private void FunnyCharError()
        {
            tb_Meta_FunnyChar.ForeColor = Color.Red;
            tb_Meta_FunnyCharCode.ForeColor = Color.Red;
        }

        private void FunnyCharNoError()
        {
            tb_Meta_FunnyChar.ForeColor = SystemColors.WindowText;
            tb_Meta_FunnyCharCode.ForeColor = SystemColors.WindowText;
        }
















        #region Metagame Event Handlers

        private void btn_SaveMetagame_Click(object sender, EventArgs e)
        {
            if (m_LoadedMetagame == null)
            {
                Console.WriteLine("LoadedMetagame is null (this should not happen).");
                return;
            }

            SaveMetagame();
        }





        private void num_Meta_TalentPoints_ValueChanged(object sender, EventArgs e)
        {
            if (m_LoadedMetagame == null)
                return;

            m_LoadedMetagame.TalentPoints = (uint)num_Meta_TalentPoints.Value;
        }

        private void combo_Meta_Tier_SelectedIndexChanged(object _sender, EventArgs _e)
        {
            if (combo_Meta_Tier.SelectedIndex < 0 || m_LoadedMetagame == null)
                return;

            int tier = combo_Meta_Tier.SelectedIndex - 1;

            PopulateTalentsList(dgv_Meta_TalentLevels, m_LoadedMetagame.TalentLevels, tier);
        }










        #endregion

        private static void PopulateTalentsList(DataGridView _dgv, Dictionary<string, uint> _talents, int _tier)
        {
            _dgv.SuspendLayout();
            _dgv.Rows.Clear();

            foreach (var pair in _talents)
            {
                dynamic[]? row = null;
                if (TalentManager.TryGetValue(pair.Key, out TalentInfo? talentInfo))
                {
                    if (_tier != -1 && (talentInfo.Tier == -1 || talentInfo.Tier != _tier))
                        continue;

                    row = new dynamic[]
                    {
                        talentInfo.Tier, talentInfo.ProtoNameShort, pair.Value, talentInfo.FriendlyName, talentInfo.Description
                    };
                }
                else
                {
                    if (_tier == -1)
                        row = new dynamic[] { -1, pair.Key, pair.Value, string.Empty, string.Empty };
                }

                if (row != null)
                    _dgv.Rows.Add(row);
            }

            _dgv.ClearSelection();
            _dgv.ResumeLayout();
        }










        #region Helpers



        #endregion






        private void SaveMetagame()
        {
            if (m_LoadedMetagame == null)
                return;

            string orgFileFullname = s_SaveFilePath + "/" + Constants.MetagameSaveFilename;
            string bakFileFullname = orgFileFullname + ".bak";
            File.Copy(orgFileFullname, bakFileFullname, true);
            Console.WriteLine("Metagame save file backed up as '" + Constants.MetagameSaveFilename + ".bak'.");

            SaveFileWriter writer;
            try
            {
                writer = new();
            }
            catch (Exception _ex)
            {
                Console.WriteLine("SaveMetagame(): Could not open metagame save file to write: " + _ex);
                return;
            }

            if (!m_LoadedMetagame.Save(writer))
            {
                Console.WriteLine("Error during saving Metagame file.");
                writer.Close();
            }
            writer.SaveFile(orgFileFullname);
            writer.Close();

            Console.WriteLine("Metagame file saved.");
        }


        private void dgv_Meta_TalentLevels_CellValueChanged(object _sender, DataGridViewCellEventArgs _e)
        {
            int row = _e.RowIndex;
            int col = _e.ColumnIndex;
            if (row < 0 || col != 2 || m_LoadedMetagame == null)
                return;

            var cells = ((DataGridView)_sender).Rows[row].Cells;
            if (!uint.TryParse((string)cells[col].Value, out uint level))
            {
                return;
            }

            string key = TalentInfo.GetFullProtoName((string)cells[1].Value);

            var talents = m_LoadedMetagame.TalentLevels;
            if (!talents.ContainsKey(key))
            {
                Console.WriteLine("Talent '" + key + "' not found.");
                return;
            }

            talents[key] = level;
        }

    }

}
