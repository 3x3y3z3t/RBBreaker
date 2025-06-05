namespace RBSaveEditor.CustomControls
{
    partial class ItemCardControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl_Title = new Label();
            lbl_MainStat = new Label();
            combo_AffixOperator = new ComboBox();
            combo_AffixType = new ComboBox();
            tb_AffixValue = new TextBox();
            list_Affixes = new ListBox();
            combo_Rarity = new ComboBox();
            num_StackCount = new NumericUpDown();
            btn_AddAffix = new Button();
            btn_RemoveAffix = new Button();
            label1 = new Label();
            num_Level = new NumericUpDown();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)num_StackCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_Level).BeginInit();
            SuspendLayout();
            // 
            // lbl_Title
            // 
            lbl_Title.AutoSize = true;
            lbl_Title.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Title.Location = new Point(8, 50);
            lbl_Title.Name = "lbl_Title";
            lbl_Title.Size = new Size(44, 21);
            lbl_Title.TabIndex = 0;
            lbl_Title.Text = "Title";
            // 
            // lbl_MainStat
            // 
            lbl_MainStat.AutoSize = true;
            lbl_MainStat.Location = new Point(41, 85);
            lbl_MainStat.Name = "lbl_MainStat";
            lbl_MainStat.Size = new Size(12, 15);
            lbl_MainStat.TabIndex = 1;
            lbl_MainStat.Text = "_";
            // 
            // combo_AffixOperator
            // 
            combo_AffixOperator.FormattingEnabled = true;
            combo_AffixOperator.Items.AddRange(new object[] { "+", "x" });
            combo_AffixOperator.Location = new Point(8, 138);
            combo_AffixOperator.Name = "combo_AffixOperator";
            combo_AffixOperator.Size = new Size(50, 23);
            combo_AffixOperator.TabIndex = 4;
            combo_AffixOperator.SelectedIndexChanged += combo_AffixOperator_SelectedIndexChanged;
            // 
            // combo_AffixType
            // 
            combo_AffixType.FormattingEnabled = true;
            combo_AffixType.Location = new Point(145, 138);
            combo_AffixType.Name = "combo_AffixType";
            combo_AffixType.Size = new Size(147, 23);
            combo_AffixType.TabIndex = 4;
            combo_AffixType.SelectedIndexChanged += combo_AffixType_SelectedIndexChanged;
            // 
            // tb_AffixValue
            // 
            tb_AffixValue.Location = new Point(64, 138);
            tb_AffixValue.Name = "tb_AffixValue";
            tb_AffixValue.Size = new Size(75, 23);
            tb_AffixValue.TabIndex = 5;
            tb_AffixValue.TextChanged += tb_AffixValue_TextChanged;
            // 
            // list_Affixes
            // 
            list_Affixes.DrawMode = DrawMode.OwnerDrawFixed;
            list_Affixes.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            list_Affixes.FormattingEnabled = true;
            list_Affixes.HorizontalScrollbar = true;
            list_Affixes.ItemHeight = 20;
            list_Affixes.Location = new Point(8, 167);
            list_Affixes.Name = "list_Affixes";
            list_Affixes.Size = new Size(284, 144);
            list_Affixes.TabIndex = 6;
            list_Affixes.DrawItem += list_Affixes_DrawItem;
            list_Affixes.SelectedIndexChanged += list_Affixes_SelectedIndexChanged;
            // 
            // combo_Rarity
            // 
            combo_Rarity.FormattingEnabled = true;
            combo_Rarity.Location = new Point(8, 8);
            combo_Rarity.Name = "combo_Rarity";
            combo_Rarity.Size = new Size(110, 23);
            combo_Rarity.TabIndex = 7;
            combo_Rarity.SelectedIndexChanged += combo_Rarity_SelectedIndexChanged;
            // 
            // num_StackCount
            // 
            num_StackCount.Location = new Point(211, 357);
            num_StackCount.Name = "num_StackCount";
            num_StackCount.Size = new Size(81, 23);
            num_StackCount.TabIndex = 8;
            num_StackCount.TextAlign = HorizontalAlignment.Right;
            num_StackCount.ThousandsSeparator = true;
            num_StackCount.ValueChanged += num_StackCount_ValueChanged;
            // 
            // btn_AddAffix
            // 
            btn_AddAffix.Location = new Point(8, 317);
            btn_AddAffix.Name = "btn_AddAffix";
            btn_AddAffix.Size = new Size(100, 23);
            btn_AddAffix.TabIndex = 9;
            btn_AddAffix.Text = "Add Affix";
            btn_AddAffix.UseVisualStyleBackColor = true;
            btn_AddAffix.Click += btn_AddAffix_Click;
            // 
            // btn_RemoveAffix
            // 
            btn_RemoveAffix.Location = new Point(192, 317);
            btn_RemoveAffix.Name = "btn_RemoveAffix";
            btn_RemoveAffix.Size = new Size(100, 23);
            btn_RemoveAffix.TabIndex = 9;
            btn_RemoveAffix.Text = "Remove Affix";
            btn_RemoveAffix.UseVisualStyleBackColor = true;
            btn_RemoveAffix.Click += btn_RemoveAffix_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(134, 359);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 10;
            label1.Text = "Stack Count";
            // 
            // num_Level
            // 
            num_Level.Location = new Point(211, 9);
            num_Level.Name = "num_Level";
            num_Level.Size = new Size(81, 23);
            num_Level.TabIndex = 11;
            num_Level.TextAlign = HorizontalAlignment.Right;
            num_Level.ValueChanged += num_Level_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(171, 11);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 10;
            label2.Text = "Level";
            // 
            // ItemCardControl
            // 
            Controls.Add(num_Level);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btn_RemoveAffix);
            Controls.Add(btn_AddAffix);
            Controls.Add(num_StackCount);
            Controls.Add(combo_Rarity);
            Controls.Add(list_Affixes);
            Controls.Add(tb_AffixValue);
            Controls.Add(combo_AffixType);
            Controls.Add(combo_AffixOperator);
            Controls.Add(lbl_MainStat);
            Controls.Add(lbl_Title);
            DoubleBuffered = true;
            Name = "ItemCardControl";
            Size = new Size(300, 500);
            ((System.ComponentModel.ISupportInitialize)num_StackCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_Level).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_Title;
        private Label lbl_MainStat;
        private ComboBox combo_AffixOperator;
        private ComboBox combo_AffixType;
        private TextBox tb_AffixValue;
        private ListBox list_Affixes;
        private ComboBox combo_Rarity;
        private NumericUpDown num_StackCount;
        private Button btn_AddAffix;
        private Button btn_RemoveAffix;
        private Label label1;
        private NumericUpDown num_Level;
        private Label label2;
    }
}
