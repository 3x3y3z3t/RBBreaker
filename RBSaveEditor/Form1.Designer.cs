namespace RBSaveEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            ListViewItem listViewItem1 = new ListViewItem("ádsadsad");
            ListViewItem listViewItem2 = new ListViewItem("123213123");
            ListViewItem listViewItem3 = new ListViewItem("Item3");
            ListViewItem listViewItem4 = new ListViewItem("item4");
            ListViewItem listViewItem5 = new ListViewItem("item5");
            ListViewItem listViewItem6 = new ListViewItem("6");
            ListViewItem listViewItem7 = new ListViewItem("7");
            ListViewItem listViewItem8 = new ListViewItem("8");
            ListViewItem listViewItem9 = new ListViewItem("9");
            ListViewItem listViewItem10 = new ListViewItem("10asdsadsadsa");
            ListViewItem listViewItem11 = new ListViewItem("11");
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            btn_LoadPilot = new Button();
            lbl_Meta_SaveFile = new Label();
            lbl_Pilot_SaveFile = new Label();
            tabControl_Main = new TabControl();
            tabPage1 = new TabPage();
            dgv_Meta_TalentLevels = new DataGridView();
            Column4 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            combo_Meta_Tier = new ComboBox();
            num_Meta_TalentPoints = new NumericUpDown();
            cb_Meta_CallAttentionToDialogueRewriteTalent = new CheckBox();
            cb_Meta_CallAttentionToEntityRewriteTalent = new CheckBox();
            cb_Meta_Unlocked = new CheckBox();
            label6 = new Label();
            label4 = new Label();
            lbl_Meta_DialogueSeenInfosMap = new Label();
            lbl_Meta_CallAttentionToNewAptitudes = new Label();
            lbl_Meta_AdditionalSetFlags = new Label();
            lbl_Meta_UnlockedAptitude = new Label();
            lbl_Meta_CriteriaEverFulfilled = new Label();
            lbl_Meta_EverEncounteredEnemies = new Label();
            lbl_Meta_NumTimesRealityBroken = new Label();
            lbl_Meta_CompletedTimelines = new Label();
            lbl_Meta_CompletedMetagameNPESteps = new Label();
            label10 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label3 = new Label();
            tab_Pilot = new TabPage();
            list_StorageInventory = new ListView();
            num_Fate = new NumericUpDown();
            num_Credits = new NumericUpDown();
            num_Xp = new NumericUpDown();
            lbl_NextLvlXp = new Label();
            label32 = new Label();
            label23 = new Label();
            label31 = new Label();
            label28 = new Label();
            lbl_SelectedItem_Dbg = new Label();
            lbl_SelectedItem_Affixes = new Label();
            label27 = new Label();
            label26 = new Label();
            lbl_SelectedItem_SpecializationName = new Label();
            lbl_SelectedItem_Type = new Label();
            label25 = new Label();
            label24 = new Label();
            label22 = new Label();
            label20 = new Label();
            num_Lvl = new NumericUpDown();
            label21 = new Label();
            label19 = new Label();
            label5 = new Label();
            lbl_SaveVersionMismatch = new Label();
            tb_Meta_FunnyChar = new TextBox();
            label16 = new Label();
            tb_Meta_FunnyCharCode = new TextBox();
            label17 = new Label();
            btn_SelectDir = new Button();
            m_TbBaseGameDir = new TextBox();
            label18 = new Label();
            btn_SaveMetagame = new Button();
            btn_SavePilot = new Button();
            tabControl_Main.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Meta_TalentLevels).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_Meta_TalentPoints).BeginInit();
            tab_Pilot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_Fate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_Credits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_Xp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_Lvl).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 60);
            label1.Name = "label1";
            label1.Size = new Size(112, 15);
            label1.TabIndex = 0;
            label1.Text = "Metagame Save File";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 89);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 0;
            label2.Text = "Pilot Save File";
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(514, 56);
            button1.Name = "button1";
            button1.Size = new Size(125, 23);
            button1.TabIndex = 1;
            button1.Text = "Load Metagame";
            button1.UseVisualStyleBackColor = true;
            // 
            // btn_LoadPilot
            // 
            btn_LoadPilot.Location = new Point(514, 85);
            btn_LoadPilot.Name = "btn_LoadPilot";
            btn_LoadPilot.Size = new Size(125, 23);
            btn_LoadPilot.TabIndex = 1;
            btn_LoadPilot.Text = "Select Pilot File";
            btn_LoadPilot.UseVisualStyleBackColor = true;
            btn_LoadPilot.Click += btn_LoadPilot_Click;
            // 
            // lbl_Meta_SaveFile
            // 
            lbl_Meta_SaveFile.AutoSize = true;
            lbl_Meta_SaveFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Meta_SaveFile.Location = new Point(130, 60);
            lbl_Meta_SaveFile.Name = "lbl_Meta_SaveFile";
            lbl_Meta_SaveFile.Size = new Size(12, 15);
            lbl_Meta_SaveFile.TabIndex = 0;
            lbl_Meta_SaveFile.Text = "_";
            // 
            // lbl_Pilot_SaveFile
            // 
            lbl_Pilot_SaveFile.AutoSize = true;
            lbl_Pilot_SaveFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Pilot_SaveFile.Location = new Point(130, 89);
            lbl_Pilot_SaveFile.Name = "lbl_Pilot_SaveFile";
            lbl_Pilot_SaveFile.Size = new Size(12, 15);
            lbl_Pilot_SaveFile.TabIndex = 0;
            lbl_Pilot_SaveFile.Text = "_";
            // 
            // tabControl_Main
            // 
            tabControl_Main.Controls.Add(tabPage1);
            tabControl_Main.Controls.Add(tab_Pilot);
            tabControl_Main.Location = new Point(12, 156);
            tabControl_Main.Name = "tabControl_Main";
            tabControl_Main.SelectedIndex = 0;
            tabControl_Main.Size = new Size(1240, 498);
            tabControl_Main.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dgv_Meta_TalentLevels);
            tabPage1.Controls.Add(combo_Meta_Tier);
            tabPage1.Controls.Add(num_Meta_TalentPoints);
            tabPage1.Controls.Add(cb_Meta_CallAttentionToDialogueRewriteTalent);
            tabPage1.Controls.Add(cb_Meta_CallAttentionToEntityRewriteTalent);
            tabPage1.Controls.Add(cb_Meta_Unlocked);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(lbl_Meta_DialogueSeenInfosMap);
            tabPage1.Controls.Add(lbl_Meta_CallAttentionToNewAptitudes);
            tabPage1.Controls.Add(lbl_Meta_AdditionalSetFlags);
            tabPage1.Controls.Add(lbl_Meta_UnlockedAptitude);
            tabPage1.Controls.Add(lbl_Meta_CriteriaEverFulfilled);
            tabPage1.Controls.Add(lbl_Meta_EverEncounteredEnemies);
            tabPage1.Controls.Add(lbl_Meta_NumTimesRealityBroken);
            tabPage1.Controls.Add(lbl_Meta_CompletedTimelines);
            tabPage1.Controls.Add(lbl_Meta_CompletedMetagameNPESteps);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label15);
            tabPage1.Controls.Add(label14);
            tabPage1.Controls.Add(label13);
            tabPage1.Controls.Add(label12);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(label3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1232, 470);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Metagame";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_Meta_TalentLevels
            // 
            dgv_Meta_TalentLevels.AllowUserToAddRows = false;
            dgv_Meta_TalentLevels.AllowUserToDeleteRows = false;
            dgv_Meta_TalentLevels.AllowUserToResizeRows = false;
            dgv_Meta_TalentLevels.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dgv_Meta_TalentLevels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_Meta_TalentLevels.Columns.AddRange(new DataGridViewColumn[] { Column4, Column1, Column3, Column2, Column5 });
            dgv_Meta_TalentLevels.Location = new Point(517, 53);
            dgv_Meta_TalentLevels.MultiSelect = false;
            dgv_Meta_TalentLevels.Name = "dgv_Meta_TalentLevels";
            dgv_Meta_TalentLevels.ReadOnly = true;
            dgv_Meta_TalentLevels.RowHeadersVisible = false;
            dgv_Meta_TalentLevels.RowTemplate.Height = 25;
            dgv_Meta_TalentLevels.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv_Meta_TalentLevels.Size = new Size(709, 390);
            dgv_Meta_TalentLevels.TabIndex = 4;
            // 
            // Column4
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopRight;
            Column4.DefaultCellStyle = dataGridViewCellStyle1;
            Column4.HeaderText = "Tier";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 35;
            // 
            // Column1
            // 
            Column1.HeaderText = "Internal Name";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 200;
            // 
            // Column3
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.TopRight;
            Column3.DefaultCellStyle = dataGridViewCellStyle2;
            Column3.HeaderText = "Level";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 40;
            // 
            // Column2
            // 
            Column2.HeaderText = "Friendly Name";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 150;
            // 
            // Column5
            // 
            Column5.HeaderText = "Description";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 250;
            // 
            // combo_Meta_Tier
            // 
            combo_Meta_Tier.FormattingEnabled = true;
            combo_Meta_Tier.Location = new Point(1097, 31);
            combo_Meta_Tier.Name = "combo_Meta_Tier";
            combo_Meta_Tier.Size = new Size(129, 23);
            combo_Meta_Tier.TabIndex = 5;
            // 
            // num_Meta_TalentPoints
            // 
            num_Meta_TalentPoints.Location = new Point(86, 31);
            num_Meta_TalentPoints.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            num_Meta_TalentPoints.Name = "num_Meta_TalentPoints";
            num_Meta_TalentPoints.Size = new Size(120, 23);
            num_Meta_TalentPoints.TabIndex = 1;
            num_Meta_TalentPoints.TextAlign = HorizontalAlignment.Right;
            num_Meta_TalentPoints.ThousandsSeparator = true;
            num_Meta_TalentPoints.ValueChanged += num_Meta_TalentPoints_ValueChanged;
            // 
            // cb_Meta_CallAttentionToDialogueRewriteTalent
            // 
            cb_Meta_CallAttentionToDialogueRewriteTalent.AutoSize = true;
            cb_Meta_CallAttentionToDialogueRewriteTalent.Enabled = false;
            cb_Meta_CallAttentionToDialogueRewriteTalent.Location = new Point(6, 279);
            cb_Meta_CallAttentionToDialogueRewriteTalent.Name = "cb_Meta_CallAttentionToDialogueRewriteTalent";
            cb_Meta_CallAttentionToDialogueRewriteTalent.Size = new Size(239, 19);
            cb_Meta_CallAttentionToDialogueRewriteTalent.TabIndex = 0;
            cb_Meta_CallAttentionToDialogueRewriteTalent.Text = "Call Attention to Dialogue Rewrite Talent";
            cb_Meta_CallAttentionToDialogueRewriteTalent.UseVisualStyleBackColor = true;
            // 
            // cb_Meta_CallAttentionToEntityRewriteTalent
            // 
            cb_Meta_CallAttentionToEntityRewriteTalent.AutoSize = true;
            cb_Meta_CallAttentionToEntityRewriteTalent.Enabled = false;
            cb_Meta_CallAttentionToEntityRewriteTalent.Location = new Point(6, 254);
            cb_Meta_CallAttentionToEntityRewriteTalent.Name = "cb_Meta_CallAttentionToEntityRewriteTalent";
            cb_Meta_CallAttentionToEntityRewriteTalent.Size = new Size(222, 19);
            cb_Meta_CallAttentionToEntityRewriteTalent.TabIndex = 0;
            cb_Meta_CallAttentionToEntityRewriteTalent.Text = "Call Attention to Entity Rewrite Talent";
            cb_Meta_CallAttentionToEntityRewriteTalent.UseVisualStyleBackColor = true;
            // 
            // cb_Meta_Unlocked
            // 
            cb_Meta_Unlocked.AutoSize = true;
            cb_Meta_Unlocked.Location = new Point(6, 6);
            cb_Meta_Unlocked.Name = "cb_Meta_Unlocked";
            cb_Meta_Unlocked.Size = new Size(136, 19);
            cb_Meta_Unlocked.TabIndex = 0;
            cb_Meta_Unlocked.Text = "Metagame Unlocked";
            cb_Meta_Unlocked.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1065, 35);
            label6.Name = "label6";
            label6.Size = new Size(26, 15);
            label6.TabIndex = 0;
            label6.Text = "Tier";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(731, 34);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 0;
            label4.Text = "Talents";
            // 
            // lbl_Meta_DialogueSeenInfosMap
            // 
            lbl_Meta_DialogueSeenInfosMap.AutoSize = true;
            lbl_Meta_DialogueSeenInfosMap.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Meta_DialogueSeenInfosMap.ForeColor = Color.Red;
            lbl_Meta_DialogueSeenInfosMap.Location = new Point(215, 135);
            lbl_Meta_DialogueSeenInfosMap.Name = "lbl_Meta_DialogueSeenInfosMap";
            lbl_Meta_DialogueSeenInfosMap.Size = new Size(12, 15);
            lbl_Meta_DialogueSeenInfosMap.TabIndex = 0;
            lbl_Meta_DialogueSeenInfosMap.Text = "_";
            // 
            // lbl_Meta_CallAttentionToNewAptitudes
            // 
            lbl_Meta_CallAttentionToNewAptitudes.AutoSize = true;
            lbl_Meta_CallAttentionToNewAptitudes.Location = new Point(215, 301);
            lbl_Meta_CallAttentionToNewAptitudes.Name = "lbl_Meta_CallAttentionToNewAptitudes";
            lbl_Meta_CallAttentionToNewAptitudes.Size = new Size(13, 15);
            lbl_Meta_CallAttentionToNewAptitudes.TabIndex = 0;
            lbl_Meta_CallAttentionToNewAptitudes.Text = "0";
            // 
            // lbl_Meta_AdditionalSetFlags
            // 
            lbl_Meta_AdditionalSetFlags.AutoSize = true;
            lbl_Meta_AdditionalSetFlags.Location = new Point(215, 220);
            lbl_Meta_AdditionalSetFlags.Name = "lbl_Meta_AdditionalSetFlags";
            lbl_Meta_AdditionalSetFlags.Size = new Size(13, 15);
            lbl_Meta_AdditionalSetFlags.TabIndex = 0;
            lbl_Meta_AdditionalSetFlags.Text = "0";
            // 
            // lbl_Meta_UnlockedAptitude
            // 
            lbl_Meta_UnlockedAptitude.AutoSize = true;
            lbl_Meta_UnlockedAptitude.Location = new Point(215, 205);
            lbl_Meta_UnlockedAptitude.Name = "lbl_Meta_UnlockedAptitude";
            lbl_Meta_UnlockedAptitude.Size = new Size(13, 15);
            lbl_Meta_UnlockedAptitude.TabIndex = 0;
            lbl_Meta_UnlockedAptitude.Text = "0";
            // 
            // lbl_Meta_CriteriaEverFulfilled
            // 
            lbl_Meta_CriteriaEverFulfilled.AutoSize = true;
            lbl_Meta_CriteriaEverFulfilled.Location = new Point(215, 178);
            lbl_Meta_CriteriaEverFulfilled.Name = "lbl_Meta_CriteriaEverFulfilled";
            lbl_Meta_CriteriaEverFulfilled.Size = new Size(13, 15);
            lbl_Meta_CriteriaEverFulfilled.TabIndex = 0;
            lbl_Meta_CriteriaEverFulfilled.Text = "0";
            // 
            // lbl_Meta_EverEncounteredEnemies
            // 
            lbl_Meta_EverEncounteredEnemies.AutoSize = true;
            lbl_Meta_EverEncounteredEnemies.Location = new Point(215, 163);
            lbl_Meta_EverEncounteredEnemies.Name = "lbl_Meta_EverEncounteredEnemies";
            lbl_Meta_EverEncounteredEnemies.Size = new Size(13, 15);
            lbl_Meta_EverEncounteredEnemies.TabIndex = 0;
            lbl_Meta_EverEncounteredEnemies.Text = "0";
            // 
            // lbl_Meta_NumTimesRealityBroken
            // 
            lbl_Meta_NumTimesRealityBroken.AutoSize = true;
            lbl_Meta_NumTimesRealityBroken.Location = new Point(215, 110);
            lbl_Meta_NumTimesRealityBroken.Name = "lbl_Meta_NumTimesRealityBroken";
            lbl_Meta_NumTimesRealityBroken.Size = new Size(13, 15);
            lbl_Meta_NumTimesRealityBroken.TabIndex = 0;
            lbl_Meta_NumTimesRealityBroken.Text = "0";
            // 
            // lbl_Meta_CompletedTimelines
            // 
            lbl_Meta_CompletedTimelines.AutoSize = true;
            lbl_Meta_CompletedTimelines.Location = new Point(215, 95);
            lbl_Meta_CompletedTimelines.Name = "lbl_Meta_CompletedTimelines";
            lbl_Meta_CompletedTimelines.Size = new Size(13, 15);
            lbl_Meta_CompletedTimelines.TabIndex = 0;
            lbl_Meta_CompletedTimelines.Text = "0";
            // 
            // lbl_Meta_CompletedMetagameNPESteps
            // 
            lbl_Meta_CompletedMetagameNPESteps.AutoSize = true;
            lbl_Meta_CompletedMetagameNPESteps.Location = new Point(215, 80);
            lbl_Meta_CompletedMetagameNPESteps.Name = "lbl_Meta_CompletedMetagameNPESteps";
            lbl_Meta_CompletedMetagameNPESteps.Size = new Size(24, 15);
            lbl_Meta_CompletedMetagameNPESteps.TabIndex = 0;
            lbl_Meta_CompletedMetagameNPESteps.Text = "0/0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 135);
            label10.Name = "label10";
            label10.Size = new Size(138, 15);
            label10.TabIndex = 0;
            label10.Text = "Dialogue Seen Infos Map";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 301);
            label15.Name = "label15";
            label15.Size = new Size(175, 15);
            label15.TabIndex = 0;
            label15.Text = "Call Attention to New Aptitudes";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 220);
            label14.Name = "label14";
            label14.Size = new Size(111, 15);
            label14.TabIndex = 0;
            label14.Text = "Additional Set Flags";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 205);
            label13.Name = "label13";
            label13.Size = new Size(106, 15);
            label13.TabIndex = 0;
            label13.Text = "Unlocked Aptitude";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 178);
            label12.Name = "label12";
            label12.Size = new Size(115, 15);
            label12.TabIndex = 0;
            label12.Text = "Criteria Ever Fulfilled";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 163);
            label11.Name = "label11";
            label11.Size = new Size(146, 15);
            label11.TabIndex = 0;
            label11.Text = "Ever Encountered Enemies";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 110);
            label9.Name = "label9";
            label9.Size = new Size(116, 15);
            label9.TabIndex = 0;
            label9.Text = "Times Reality Broken";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 95);
            label8.Name = "label8";
            label8.Size = new Size(119, 15);
            label8.TabIndex = 0;
            label8.Text = "Completed Timelines";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 80);
            label7.Name = "label7";
            label7.Size = new Size(182, 15);
            label7.TabIndex = 0;
            label7.Text = "Completed Metagame NPE Steps";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.HotTrack;
            label3.Location = new Point(6, 33);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 0;
            label3.Text = "Talent Points";
            // 
            // tab_Pilot
            // 
            tab_Pilot.Controls.Add(list_StorageInventory);
            tab_Pilot.Controls.Add(num_Fate);
            tab_Pilot.Controls.Add(num_Credits);
            tab_Pilot.Controls.Add(num_Xp);
            tab_Pilot.Controls.Add(lbl_NextLvlXp);
            tab_Pilot.Controls.Add(label32);
            tab_Pilot.Controls.Add(label23);
            tab_Pilot.Controls.Add(label31);
            tab_Pilot.Controls.Add(label28);
            tab_Pilot.Controls.Add(lbl_SelectedItem_Dbg);
            tab_Pilot.Controls.Add(lbl_SelectedItem_Affixes);
            tab_Pilot.Controls.Add(label27);
            tab_Pilot.Controls.Add(label26);
            tab_Pilot.Controls.Add(lbl_SelectedItem_SpecializationName);
            tab_Pilot.Controls.Add(lbl_SelectedItem_Type);
            tab_Pilot.Controls.Add(label25);
            tab_Pilot.Controls.Add(label24);
            tab_Pilot.Controls.Add(label22);
            tab_Pilot.Controls.Add(label20);
            tab_Pilot.Controls.Add(num_Lvl);
            tab_Pilot.Controls.Add(label21);
            tab_Pilot.Controls.Add(label19);
            tab_Pilot.Location = new Point(4, 24);
            tab_Pilot.Name = "tab_Pilot";
            tab_Pilot.Padding = new Padding(3);
            tab_Pilot.Size = new Size(1232, 470);
            tab_Pilot.TabIndex = 1;
            tab_Pilot.Text = "Pilot";
            tab_Pilot.UseVisualStyleBackColor = true;
            // 
            // list_StorageInventory
            // 
            list_StorageInventory.GridLines = true;
            list_StorageInventory.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4, listViewItem5, listViewItem6, listViewItem7, listViewItem8, listViewItem9, listViewItem10, listViewItem11 });
            list_StorageInventory.Location = new Point(23, 256);
            list_StorageInventory.MultiSelect = false;
            list_StorageInventory.Name = "list_StorageInventory";
            list_StorageInventory.ShowItemToolTips = true;
            list_StorageInventory.Size = new Size(450, 139);
            list_StorageInventory.TabIndex = 5;
            list_StorageInventory.UseCompatibleStateImageBehavior = false;
            list_StorageInventory.ItemSelectionChanged += list_StorageInventory_ItemSelectionChanged;
            // 
            // num_Fate
            // 
            num_Fate.Location = new Point(73, 162);
            num_Fate.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            num_Fate.Name = "num_Fate";
            num_Fate.Size = new Size(137, 23);
            num_Fate.TabIndex = 3;
            num_Fate.TextAlign = HorizontalAlignment.Right;
            num_Fate.ThousandsSeparator = true;
            num_Fate.ValueChanged += num_Fate_ValueChanged;
            // 
            // num_Credits
            // 
            num_Credits.Location = new Point(73, 133);
            num_Credits.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            num_Credits.Name = "num_Credits";
            num_Credits.Size = new Size(137, 23);
            num_Credits.TabIndex = 3;
            num_Credits.TextAlign = HorizontalAlignment.Right;
            num_Credits.ThousandsSeparator = true;
            num_Credits.ValueChanged += num_Credits_ValueChanged;
            // 
            // num_Xp
            // 
            num_Xp.Location = new Point(63, 50);
            num_Xp.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            num_Xp.Name = "num_Xp";
            num_Xp.Size = new Size(137, 23);
            num_Xp.TabIndex = 3;
            num_Xp.TextAlign = HorizontalAlignment.Right;
            num_Xp.ThousandsSeparator = true;
            num_Xp.ValueChanged += num_Xp_ValueChanged;
            // 
            // lbl_NextLvlXp
            // 
            lbl_NextLvlXp.AutoSize = true;
            lbl_NextLvlXp.Location = new Point(206, 52);
            lbl_NextLvlXp.Name = "lbl_NextLvlXp";
            lbl_NextLvlXp.Size = new Size(21, 15);
            lbl_NextLvlXp.TabIndex = 2;
            lbl_NextLvlXp.Text = "/ 0";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.ForeColor = SystemColors.HotTrack;
            label32.Location = new Point(23, 164);
            label32.Name = "label32";
            label32.Size = new Size(29, 15);
            label32.TabIndex = 2;
            label32.Text = "Fate";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(125, 228);
            label23.Name = "label23";
            label23.Size = new Size(12, 15);
            label23.TabIndex = 2;
            label23.Text = "_";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.ForeColor = SystemColors.HotTrack;
            label31.Location = new Point(23, 135);
            label31.Name = "label31";
            label31.Size = new Size(44, 15);
            label31.TabIndex = 2;
            label31.Text = "Credits";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.ForeColor = Color.Red;
            label28.Location = new Point(32, 398);
            label28.Name = "label28";
            label28.Size = new Size(195, 30);
            label28.TabIndex = 3;
            label28.Text = "↑ This is Station 6 storage inventory.\r\n   Select an item to view details.";
            // 
            // lbl_SelectedItem_Dbg
            // 
            lbl_SelectedItem_Dbg.AutoSize = true;
            lbl_SelectedItem_Dbg.Location = new Point(663, 273);
            lbl_SelectedItem_Dbg.Name = "lbl_SelectedItem_Dbg";
            lbl_SelectedItem_Dbg.Size = new Size(12, 15);
            lbl_SelectedItem_Dbg.TabIndex = 2;
            lbl_SelectedItem_Dbg.Text = "_";
            // 
            // lbl_SelectedItem_Affixes
            // 
            lbl_SelectedItem_Affixes.AutoSize = true;
            lbl_SelectedItem_Affixes.Location = new Point(663, 313);
            lbl_SelectedItem_Affixes.Name = "lbl_SelectedItem_Affixes";
            lbl_SelectedItem_Affixes.Size = new Size(12, 15);
            lbl_SelectedItem_Affixes.TabIndex = 2;
            lbl_SelectedItem_Affixes.Text = "_";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(527, 313);
            label27.Name = "label27";
            label27.Size = new Size(43, 15);
            label27.TabIndex = 2;
            label27.Text = "Affixes";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(527, 273);
            label26.Name = "label26";
            label26.Size = new Size(130, 15);
            label26.TabIndex = 2;
            label26.Text = "item.ToString() (debug)";
            // 
            // lbl_SelectedItem_SpecializationName
            // 
            lbl_SelectedItem_SpecializationName.AutoSize = true;
            lbl_SelectedItem_SpecializationName.Location = new Point(663, 243);
            lbl_SelectedItem_SpecializationName.Name = "lbl_SelectedItem_SpecializationName";
            lbl_SelectedItem_SpecializationName.Size = new Size(12, 15);
            lbl_SelectedItem_SpecializationName.TabIndex = 2;
            lbl_SelectedItem_SpecializationName.Text = "_";
            // 
            // lbl_SelectedItem_Type
            // 
            lbl_SelectedItem_Type.AutoSize = true;
            lbl_SelectedItem_Type.Location = new Point(663, 228);
            lbl_SelectedItem_Type.Name = "lbl_SelectedItem_Type";
            lbl_SelectedItem_Type.Size = new Size(12, 15);
            lbl_SelectedItem_Type.TabIndex = 2;
            lbl_SelectedItem_Type.Text = "_";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(527, 243);
            label25.Name = "label25";
            label25.Size = new Size(114, 15);
            label25.TabIndex = 2;
            label25.Text = "Specialization Name";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(527, 228);
            label24.Name = "label24";
            label24.Size = new Size(58, 15);
            label24.TabIndex = 2;
            label24.Text = "Item Type";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(23, 228);
            label22.Name = "label22";
            label22.Size = new Size(96, 15);
            label22.TabIndex = 2;
            label22.Text = "Station 6 Storage";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.ForeColor = SystemColors.HotTrack;
            label20.Location = new Point(23, 52);
            label20.Name = "label20";
            label20.Size = new Size(27, 15);
            label20.TabIndex = 2;
            label20.Text = "EXP";
            // 
            // num_Lvl
            // 
            num_Lvl.Location = new Point(63, 21);
            num_Lvl.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            num_Lvl.Name = "num_Lvl";
            num_Lvl.Size = new Size(137, 23);
            num_Lvl.TabIndex = 3;
            num_Lvl.TextAlign = HorizontalAlignment.Right;
            num_Lvl.ThousandsSeparator = true;
            num_Lvl.ValueChanged += num_Lvl_ValueChanged;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(214, 76);
            label21.Name = "label21";
            label21.Size = new Size(94, 30);
            label21.TabIndex = 2;
            label21.Text = "↑\r\nXP To Next Level";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.ForeColor = SystemColors.HotTrack;
            label19.Location = new Point(23, 23);
            label19.Name = "label19";
            label19.Size = new Size(34, 15);
            label19.TabIndex = 2;
            label19.Text = "Level";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(489, 111);
            label5.Name = "label5";
            label5.Size = new Size(153, 15);
            label5.TabIndex = 3;
            label5.Text = "Save version mismatched!";
            label5.Visible = false;
            // 
            // lbl_SaveVersionMismatch
            // 
            lbl_SaveVersionMismatch.AutoSize = true;
            lbl_SaveVersionMismatch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_SaveVersionMismatch.ForeColor = Color.Red;
            lbl_SaveVersionMismatch.Location = new Point(130, 111);
            lbl_SaveVersionMismatch.Name = "lbl_SaveVersionMismatch";
            lbl_SaveVersionMismatch.Size = new Size(153, 15);
            lbl_SaveVersionMismatch.TabIndex = 3;
            lbl_SaveVersionMismatch.Text = "Save version mismatched!";
            lbl_SaveVersionMismatch.Visible = false;
            // 
            // tb_Meta_FunnyChar
            // 
            tb_Meta_FunnyChar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tb_Meta_FunnyChar.Location = new Point(1198, 12);
            tb_Meta_FunnyChar.MaxLength = 1;
            tb_Meta_FunnyChar.Name = "tb_Meta_FunnyChar";
            tb_Meta_FunnyChar.Size = new Size(50, 23);
            tb_Meta_FunnyChar.TabIndex = 4;
            tb_Meta_FunnyChar.TextAlign = HorizontalAlignment.Center;
            tb_Meta_FunnyChar.Visible = false;
            tb_Meta_FunnyChar.WordWrap = false;
            tb_Meta_FunnyChar.TextChanged += tb_Meta_FunnyChar_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label16.ForeColor = Color.Crimson;
            label16.Location = new Point(1102, 16);
            label16.Name = "label16";
            label16.Size = new Size(90, 15);
            label16.TabIndex = 3;
            label16.Text = "Burn your eyes";
            label16.Visible = false;
            // 
            // tb_Meta_FunnyCharCode
            // 
            tb_Meta_FunnyCharCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tb_Meta_FunnyCharCode.Location = new Point(1198, 41);
            tb_Meta_FunnyCharCode.MaxLength = 3;
            tb_Meta_FunnyCharCode.Name = "tb_Meta_FunnyCharCode";
            tb_Meta_FunnyCharCode.Size = new Size(50, 23);
            tb_Meta_FunnyCharCode.TabIndex = 4;
            tb_Meta_FunnyCharCode.TextAlign = HorizontalAlignment.Center;
            tb_Meta_FunnyCharCode.Visible = false;
            tb_Meta_FunnyCharCode.WordWrap = false;
            tb_Meta_FunnyCharCode.TextChanged += tb_Meta_FunnyCharCode_TextChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label17.ForeColor = Color.Crimson;
            label17.Location = new Point(1037, 44);
            label17.Name = "label17";
            label17.Size = new Size(155, 15);
            label17.TabIndex = 3;
            label17.Text = "Burn your eyes (char code)";
            label17.Visible = false;
            // 
            // btn_SelectDir
            // 
            btn_SelectDir.Location = new Point(514, 27);
            btn_SelectDir.Name = "btn_SelectDir";
            btn_SelectDir.Size = new Size(125, 23);
            btn_SelectDir.TabIndex = 7;
            btn_SelectDir.Text = "Select Directory";
            btn_SelectDir.UseVisualStyleBackColor = true;
            btn_SelectDir.Visible = false;
            btn_SelectDir.Click += btn_SelectDir_Click;
            // 
            // m_TbBaseGameDir
            // 
            m_TbBaseGameDir.Location = new Point(12, 27);
            m_TbBaseGameDir.Name = "m_TbBaseGameDir";
            m_TbBaseGameDir.ReadOnly = true;
            m_TbBaseGameDir.Size = new Size(496, 23);
            m_TbBaseGameDir.TabIndex = 6;
            m_TbBaseGameDir.Visible = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(12, 9);
            label18.Name = "label18";
            label18.Size = new Size(273, 15);
            label18.TabIndex = 5;
            label18.Text = "Base Game Directory (Where \"Reality Break.exe\" is)";
            label18.Visible = false;
            // 
            // btn_SaveMetagame
            // 
            btn_SaveMetagame.Location = new Point(645, 56);
            btn_SaveMetagame.Name = "btn_SaveMetagame";
            btn_SaveMetagame.Size = new Size(125, 23);
            btn_SaveMetagame.TabIndex = 1;
            btn_SaveMetagame.Text = "Save Metagame";
            btn_SaveMetagame.UseVisualStyleBackColor = true;
            btn_SaveMetagame.Click += btn_SaveMetagame_Click;
            // 
            // btn_SavePilot
            // 
            btn_SavePilot.Location = new Point(645, 85);
            btn_SavePilot.Name = "btn_SavePilot";
            btn_SavePilot.Size = new Size(125, 23);
            btn_SavePilot.TabIndex = 1;
            btn_SavePilot.Text = "Save Pilot";
            btn_SavePilot.UseVisualStyleBackColor = true;
            btn_SavePilot.Click += btn_SavePilot_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(btn_SelectDir);
            Controls.Add(m_TbBaseGameDir);
            Controls.Add(label18);
            Controls.Add(tb_Meta_FunnyCharCode);
            Controls.Add(tb_Meta_FunnyChar);
            Controls.Add(lbl_SaveVersionMismatch);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(label5);
            Controls.Add(tabControl_Main);
            Controls.Add(btn_SavePilot);
            Controls.Add(btn_LoadPilot);
            Controls.Add(btn_SaveMetagame);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(lbl_Pilot_SaveFile);
            Controls.Add(lbl_Meta_SaveFile);
            Controls.Add(label1);
            Name = "Form1";
            Load += Form1_Load;
            tabControl_Main.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Meta_TalentLevels).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_Meta_TalentPoints).EndInit();
            tab_Pilot.ResumeLayout(false);
            tab_Pilot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_Fate).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_Credits).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_Xp).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_Lvl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Button btn_LoadPilot;
        private Label lbl_Meta_SaveFile;
        private Label lbl_Pilot_SaveFile;
        private TabControl tabControl_Main;
        private TabPage tabPage1;
        private TabPage tab_Pilot;
        private CheckBox cb_Meta_Unlocked;
        private Label label5;
        private Label lbl_SaveVersionMismatch;
        private NumericUpDown num_Meta_TalentPoints;
        private Label label3;
        private DataGridView dgv_Meta_TalentLevels;
        private Label label4;
        private ComboBox combo_Meta_Tier;
        private Label label6;
        private Label label7;
        private Label lbl_Meta_CompletedMetagameNPESteps;
        private Label lbl_Meta_CompletedTimelines;
        private Label label8;
        private Label lbl_Meta_NumTimesRealityBroken;
        private Label label9;
        private Label lbl_Meta_DialogueSeenInfosMap;
        private Label label10;
        private Label lbl_Meta_EverEncounteredEnemies;
        private Label label11;
        private Label lbl_Meta_CriteriaEverFulfilled;
        private Label label12;
        private Label lbl_Meta_UnlockedAptitude;
        private Label label13;
        private Label lbl_Meta_AdditionalSetFlags;
        private Label label14;
        private CheckBox cb_Meta_CallAttentionToEntityRewriteTalent;
        private CheckBox cb_Meta_CallAttentionToDialogueRewriteTalent;
        private Label lbl_Meta_CallAttentionToNewAptitudes;
        private Label label15;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column5;
        private TextBox tb_Meta_FunnyChar;
        private Label label16;
        private TextBox tb_Meta_FunnyCharCode;
        private Label label17;
        private Button btn_SelectDir;
        private TextBox m_TbBaseGameDir;
        private Label label18;
        private NumericUpDown num_Lvl;
        private Label label19;
        private NumericUpDown num_Xp;
        private Label label20;
        private Label lbl_NextLvlXp;
        private Label label21;
        private ListView list_StorageInventory;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label lbl_SelectedItem_Dbg;
        private Label lbl_SelectedItem_SpecializationName;
        private Label lbl_SelectedItem_Type;
        private Label label31;
        private NumericUpDown num_Credits;
        private NumericUpDown num_Fate;
        private Label label32;
        private Label lbl_SelectedItem_Affixes;
        private Label label27;
        private Button btn_SaveMetagame;
        private Button btn_SavePilot;
        private Label label28;
    }
}
