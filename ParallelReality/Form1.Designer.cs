namespace ParallelReality
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
            label1 = new Label();
            m_TbBaseGameDir = new TextBox();
            lbl_FoundModsCount = new Label();
            label3 = new Label();
            dgv_ModsList = new DataGridView();
            Column2 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            btn_SelectDir = new Button();
            tb_ModFiles = new TextBox();
            label2 = new Label();
            btn_ApplyMod = new Button();
            btn_ReturnBaseGame = new Button();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lbl_ModName = new Label();
            lbl_Author = new Label();
            lbl_ModVer = new Label();
            lbl_GameVer = new Label();
            dgv_SelectedMod = new DataGridView();
            Column6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            btn_Select = new Button();
            btn_Unselect = new Button();
            btn_MoveUp = new Button();
            btn_MoveDown = new Button();
            label8 = new Label();
            btn_OpenReadme = new Button();
            btn_RefreshModList = new Button();
            lbl_Status = new Label();
            ((System.ComponentModel.ISupportInitialize)dgv_ModsList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgv_SelectedMod).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(263, 15);
            label1.TabIndex = 0;
            label1.Text = "Base Game Directory (Where Reality Break.exe is)";
            // 
            // m_TbBaseGameDir
            // 
            m_TbBaseGameDir.Location = new Point(12, 27);
            m_TbBaseGameDir.Name = "m_TbBaseGameDir";
            m_TbBaseGameDir.Size = new Size(555, 23);
            m_TbBaseGameDir.TabIndex = 1;
            // 
            // lbl_FoundModsCount
            // 
            lbl_FoundModsCount.AutoSize = true;
            lbl_FoundModsCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_FoundModsCount.Location = new Point(105, 76);
            lbl_FoundModsCount.Name = "lbl_FoundModsCount";
            lbl_FoundModsCount.Size = new Size(14, 15);
            lbl_FoundModsCount.TabIndex = 0;
            lbl_FoundModsCount.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 76);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 0;
            label3.Text = "Found Mods:";
            // 
            // dgv_ModsList
            // 
            dgv_ModsList.AllowUserToAddRows = false;
            dgv_ModsList.AllowUserToDeleteRows = false;
            dgv_ModsList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgv_ModsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_ModsList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_ModsList.Columns.AddRange(new DataGridViewColumn[] { Column2, Column1, Column3, Column4, Column5 });
            dgv_ModsList.Location = new Point(12, 94);
            dgv_ModsList.MultiSelect = false;
            dgv_ModsList.Name = "dgv_ModsList";
            dgv_ModsList.RowHeadersVisible = false;
            dgv_ModsList.RowTemplate.Height = 25;
            dgv_ModsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_ModsList.Size = new Size(555, 312);
            dgv_ModsList.TabIndex = 2;
            dgv_ModsList.CellClick += dgv_ModsList_CellClick;
            dgv_ModsList.CellDoubleClick += dgv_ModsList_CellDoubleClick;
            // 
            // Column2
            // 
            Column2.HeaderText = "Id";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 30;
            // 
            // Column1
            // 
            Column1.HeaderText = "Mod Name";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 200;
            // 
            // Column3
            // 
            Column3.HeaderText = "Author";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Mod Version";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "Game Version";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // btn_SelectDir
            // 
            btn_SelectDir.Location = new Point(573, 27);
            btn_SelectDir.Name = "btn_SelectDir";
            btn_SelectDir.Size = new Size(125, 23);
            btn_SelectDir.TabIndex = 3;
            btn_SelectDir.Text = "Select Directory";
            btn_SelectDir.UseVisualStyleBackColor = true;
            btn_SelectDir.Click += btn_SelectDir_Click;
            // 
            // tb_ModFiles
            // 
            tb_ModFiles.Location = new Point(12, 507);
            tb_ModFiles.Multiline = true;
            tb_ModFiles.Name = "tb_ModFiles";
            tb_ModFiles.ReadOnly = true;
            tb_ModFiles.ScrollBars = ScrollBars.Vertical;
            tb_ModFiles.Size = new Size(552, 162);
            tb_ModFiles.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 489);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 0;
            label2.Text = "Modified Files:";
            // 
            // btn_ApplyMod
            // 
            btn_ApplyMod.Location = new Point(1102, 413);
            btn_ApplyMod.Name = "btn_ApplyMod";
            btn_ApplyMod.Size = new Size(150, 23);
            btn_ApplyMod.TabIndex = 3;
            btn_ApplyMod.Text = "Apply Selected Mod(s)";
            btn_ApplyMod.UseVisualStyleBackColor = true;
            btn_ApplyMod.Click += btn_ApplyMod_Click;
            // 
            // btn_ReturnBaseGame
            // 
            btn_ReturnBaseGame.Location = new Point(697, 413);
            btn_ReturnBaseGame.Name = "btn_ReturnBaseGame";
            btn_ReturnBaseGame.Size = new Size(150, 23);
            btn_ReturnBaseGame.TabIndex = 3;
            btn_ReturnBaseGame.Text = "Restore Base Game";
            btn_ReturnBaseGame.UseVisualStyleBackColor = true;
            btn_ReturnBaseGame.Click += btn_RestoreBaseGame_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 417);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 0;
            label4.Text = "Mod Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 432);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 0;
            label5.Text = "Author:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 447);
            label6.Name = "label6";
            label6.Size = new Size(76, 15);
            label6.TabIndex = 0;
            label6.Text = "Mod Version:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 462);
            label7.Name = "label7";
            label7.Size = new Size(82, 15);
            label7.TabIndex = 0;
            label7.Text = "Game Version:";
            // 
            // lbl_ModName
            // 
            lbl_ModName.AutoSize = true;
            lbl_ModName.Location = new Point(105, 417);
            lbl_ModName.Name = "lbl_ModName";
            lbl_ModName.Size = new Size(12, 15);
            lbl_ModName.TabIndex = 0;
            lbl_ModName.Text = "_";
            // 
            // lbl_Author
            // 
            lbl_Author.AutoSize = true;
            lbl_Author.Location = new Point(105, 432);
            lbl_Author.Name = "lbl_Author";
            lbl_Author.Size = new Size(12, 15);
            lbl_Author.TabIndex = 0;
            lbl_Author.Text = "_";
            // 
            // lbl_ModVer
            // 
            lbl_ModVer.AutoSize = true;
            lbl_ModVer.Location = new Point(105, 447);
            lbl_ModVer.Name = "lbl_ModVer";
            lbl_ModVer.Size = new Size(12, 15);
            lbl_ModVer.TabIndex = 0;
            lbl_ModVer.Text = "_";
            // 
            // lbl_GameVer
            // 
            lbl_GameVer.AutoSize = true;
            lbl_GameVer.Location = new Point(105, 462);
            lbl_GameVer.Name = "lbl_GameVer";
            lbl_GameVer.Size = new Size(12, 15);
            lbl_GameVer.TabIndex = 0;
            lbl_GameVer.Text = "_";
            // 
            // dgv_SelectedMod
            // 
            dgv_SelectedMod.AllowUserToAddRows = false;
            dgv_SelectedMod.AllowUserToDeleteRows = false;
            dgv_SelectedMod.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_SelectedMod.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgv_SelectedMod.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_SelectedMod.Columns.AddRange(new DataGridViewColumn[] { Column6, dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dgv_SelectedMod.Location = new Point(697, 94);
            dgv_SelectedMod.MultiSelect = false;
            dgv_SelectedMod.Name = "dgv_SelectedMod";
            dgv_SelectedMod.RowHeadersVisible = false;
            dgv_SelectedMod.RowTemplate.Height = 25;
            dgv_SelectedMod.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_SelectedMod.Size = new Size(555, 312);
            dgv_SelectedMod.TabIndex = 5;
            dgv_SelectedMod.CellClick += dgv_SelectedMod_CellClick;
            dgv_SelectedMod.CellDoubleClick += dgv_SelectedMod_CellDoubleClick;
            // 
            // Column6
            // 
            Column6.HeaderText = "Id";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Mod Name";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Author";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Mod Version";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Game Version";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // btn_Select
            // 
            btn_Select.Location = new Point(606, 139);
            btn_Select.Name = "btn_Select";
            btn_Select.Size = new Size(50, 23);
            btn_Select.TabIndex = 3;
            btn_Select.Text = "->";
            btn_Select.UseVisualStyleBackColor = true;
            btn_Select.Click += btn_Select_Click;
            // 
            // btn_Unselect
            // 
            btn_Unselect.Location = new Point(606, 168);
            btn_Unselect.Name = "btn_Unselect";
            btn_Unselect.Size = new Size(50, 23);
            btn_Unselect.TabIndex = 3;
            btn_Unselect.Text = "<-";
            btn_Unselect.UseVisualStyleBackColor = true;
            btn_Unselect.Click += btn_Unselect_Click;
            // 
            // btn_MoveUp
            // 
            btn_MoveUp.Location = new Point(606, 215);
            btn_MoveUp.Name = "btn_MoveUp";
            btn_MoveUp.Size = new Size(50, 23);
            btn_MoveUp.TabIndex = 3;
            btn_MoveUp.Text = "^";
            btn_MoveUp.UseVisualStyleBackColor = true;
            btn_MoveUp.Click += btn_MoveUp_Click;
            // 
            // btn_MoveDown
            // 
            btn_MoveDown.Location = new Point(606, 244);
            btn_MoveDown.Name = "btn_MoveDown";
            btn_MoveDown.Size = new Size(50, 23);
            btn_MoveDown.TabIndex = 3;
            btn_MoveDown.Text = "v";
            btn_MoveDown.UseVisualStyleBackColor = true;
            btn_MoveDown.Click += btn_MoveDown_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(697, 76);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 0;
            label8.Text = "Selected Mods:";
            // 
            // btn_OpenReadme
            // 
            btn_OpenReadme.Location = new Point(417, 413);
            btn_OpenReadme.Name = "btn_OpenReadme";
            btn_OpenReadme.Size = new Size(150, 23);
            btn_OpenReadme.TabIndex = 6;
            btn_OpenReadme.Text = "Open README";
            btn_OpenReadme.UseVisualStyleBackColor = true;
            btn_OpenReadme.Click += btn_OpenReadme_Click;
            // 
            // btn_RefreshModList
            // 
            btn_RefreshModList.Location = new Point(442, 68);
            btn_RefreshModList.Name = "btn_RefreshModList";
            btn_RefreshModList.Size = new Size(125, 23);
            btn_RefreshModList.TabIndex = 3;
            btn_RefreshModList.Text = "Refresh";
            btn_RefreshModList.UseVisualStyleBackColor = true;
            btn_RefreshModList.Click += btn_RefreshModList_Click;
            // 
            // lbl_Status
            // 
            lbl_Status.AutoSize = true;
            lbl_Status.Location = new Point(697, 447);
            lbl_Status.Name = "lbl_Status";
            lbl_Status.Size = new Size(39, 15);
            lbl_Status.TabIndex = 0;
            lbl_Status.Text = "Status";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(btn_OpenReadme);
            Controls.Add(dgv_SelectedMod);
            Controls.Add(tb_ModFiles);
            Controls.Add(btn_ReturnBaseGame);
            Controls.Add(btn_Unselect);
            Controls.Add(btn_MoveDown);
            Controls.Add(btn_MoveUp);
            Controls.Add(btn_Select);
            Controls.Add(btn_ApplyMod);
            Controls.Add(btn_RefreshModList);
            Controls.Add(btn_SelectDir);
            Controls.Add(dgv_ModsList);
            Controls.Add(m_TbBaseGameDir);
            Controls.Add(lbl_GameVer);
            Controls.Add(label7);
            Controls.Add(lbl_ModVer);
            Controls.Add(label6);
            Controls.Add(lbl_Author);
            Controls.Add(label5);
            Controls.Add(lbl_ModName);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(lbl_Status);
            Controls.Add(label8);
            Controls.Add(label3);
            Controls.Add(lbl_FoundModsCount);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Parallel Reality (RB Mod Loader)";
            Shown += Form1_Shown;
            ((System.ComponentModel.ISupportInitialize)dgv_ModsList).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgv_SelectedMod).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox m_TbBaseGameDir;
        private Label lbl_FoundModsCount;
        private Label label3;
        private DataGridView dgv_ModsList;
        private Button btn_SelectDir;
        private TextBox tb_ModFiles;
        private Label label2;
        private Button btn_ApplyMod;
        private Button btn_ReturnBaseGame;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lbl_ModName;
        private Label lbl_Author;
        private Label lbl_ModVer;
        private Label lbl_GameVer;
        private DataGridView dgv_SelectedMod;
        private Button btn_Select;
        private Button btn_Unselect;
        private Button btn_MoveUp;
        private Button btn_MoveDown;
        private Label label8;
        private Button btn_OpenReadme;
        private Button btn_RefreshModList;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Label lbl_Status;
    }
}
