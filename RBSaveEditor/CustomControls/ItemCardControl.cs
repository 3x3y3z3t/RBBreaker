/*  CustomControls/ItemCardControl.cs
 *  Version 1 (2025.06.05)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RBSaveEditor.GameClone;
using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.CustomControls
{
    public partial class ItemCardControl : UserControl
    {
        private class ListBoxItem
        {
            public string DisplayString = string.Empty;

            public override string ToString() => DisplayString;
        }


        public CItem? SelectedItem => m_Item;


        public string Item_Title { get => lbl_Title.Text; set => lbl_Title.Text = value; }


        public ItemCardControl()
        {
            InitializeComponent();

            SuspendLayout();

            eRarity[] rarities = Enum.GetValues<eRarity>();
            foreach (eRarity rarity in rarities)
            {
                combo_Rarity.Items.Add(rarity.ToString());
            }

            eItemAffixType[] types = Enum.GetValues<eItemAffixType>();
            foreach (eItemAffixType type in types)
            {
                combo_AffixType.Items.Add(type.ToString());
            }

            m_AffixListTextDrawBrush = new SolidBrush(list_Affixes.ForeColor);





            ResumeLayout(false);
            PerformLayout();

        }


        protected override void OnPaint(PaintEventArgs _e)
        {
            base.OnPaint(_e);

            Graphics graphics = _e.Graphics;



            int width = 3;
            Rectangle rect = ClientRectangle;
            //rect.Inflate(3, 3);

            Color borderColor;
            if (m_Item == null)
                borderColor = s_RarityColors[0];
            else
                borderColor = s_RarityColors[(int)m_Item.Rarity];

            ControlPaint.DrawBorder(graphics, rect,
                borderColor, width, ButtonBorderStyle.Solid,
                borderColor, width, ButtonBorderStyle.Solid,
                borderColor, width, ButtonBorderStyle.Solid,
                borderColor, width, ButtonBorderStyle.Solid
            );

        }



        public void ClearItemCard()
        {
            m_Item = null;

            combo_Rarity.Enabled = false;
            combo_Rarity.SelectedIndex = 0;

            num_Level.Enabled = false;
            num_Level.Value = 0;

            num_StackCount.Enabled = false;
            num_StackCount.Value = 0;

            lbl_Title.Text = string.Empty;
            lbl_MainStat.Text = string.Empty;

            btn_AddAffix.Enabled = false;
            btn_RemoveAffix.Enabled = false;

            list_Affixes.Enabled = false;
            list_Affixes.Items.Clear();




            ClearAffixEditor();

            UpdateItemCardBorderColor();
            Invalidate(false);
        }

        private void ClearAffixEditor()
        {
            combo_AffixOperator.Enabled = false;
            combo_AffixOperator.SelectedIndex = 0;

            tb_AffixValue.Enabled = false;
            tb_AffixValue.Text = string.Empty;

            combo_AffixType.Enabled = false;
            combo_AffixType.SelectedIndex = 0;

            btn_RemoveAffix.Enabled = false;
        }




        public void PopulateWithItem(CItem _item)
        {
            m_Item = _item;
            // TODO: Clone UIItemTooltip.SetupItem();

            if (_item.ItemType == eItemType.Equipment)
            {
                combo_Rarity.Enabled = true;

                num_Level.Enabled = true;
                num_Level.Value = _item.ItemLevel;
            }
            else
            {
                combo_Rarity.Enabled = false;

                num_Level.Enabled = false;
                num_Level.Value = 0;
            }
            combo_Rarity.SelectedIndex = (int)_item.Rarity;

            if (_item.ItemType == eItemType.Resource || _item.ItemType == eItemType.Fragment)
            {
                num_StackCount.Enabled = true;
            }
            else
            {
                num_StackCount.Enabled = false;
            }
            num_StackCount.Value = _item.StackCount;

            lbl_Title.Text = _item.GetFullDisplayName();

            list_Affixes.Enabled = true;
            list_Affixes.Items.Clear();
            foreach (var affix in _item.Affixes)
            {
                list_Affixes.Items.Add(affix);
            }

            btn_AddAffix.Enabled = true;

            UpdateItemCardBorderColor();
            Invalidate(false);









        }

        private void PopulateWithItem_Resource(CItem _item)
        {
            lbl_Title.Text = _item.SpecializationName;



        }

        private void PopulateWithItem_Equipment(CItemEquipment _item)
        {




        }







        private void PopulateLabelItemName(CItem _item)
        {

        }





        private void UpdateItemCardBorderColor()
        {
            if (m_Item == null)
            {
                m_BorderColor = DefaultBackColor;
                return;
            }

            m_BorderColor = s_RarityColors[(int)m_Item.Rarity];
        }










        private void combo_Rarity_SelectedIndexChanged(object _sender, EventArgs e)
        {
            if (m_Item == null || m_Item.ItemType != eItemType.Equipment)
                return;

            m_Item.Rarity = (eRarity)((ComboBox)_sender).SelectedIndex;

            Invalidate(false);
        }

        private void num_Level_ValueChanged(object sender, EventArgs e)
        {
            if (m_Item == null || m_Item.ItemType != eItemType.Equipment)
                return;

            m_Item.ItemLevel = (int)num_Level.Value;
        }

        private void num_StackCount_ValueChanged(object sender, EventArgs e)
        {
            if (m_Item == null || m_Item.ItemType == eItemType.Equipment || m_Item.ItemType == eItemType.Chassis)
                return;

            m_Item.StackCount = (int)num_StackCount.Value;
        }

        private void combo_AffixOperator_SelectedIndexChanged(object _sender, EventArgs e)
        {
            if (m_Item == null || m_SelectedAffixIndex < 0 || m_SelectedAffixIndex >= m_Item.Affixes.Count)
                return;

            CItemAffix affix = m_Item.Affixes[m_SelectedAffixIndex];
            affix.AffixOperator = (eAffixOperator)((ComboBox)_sender).SelectedIndex;

            list_Affixes.Refresh();
        }

        private void tb_AffixValue_TextChanged(object sender, EventArgs e)
        {
            if (m_Item == null || m_SelectedAffixIndex < 0 || m_SelectedAffixIndex >= m_Item.Affixes.Count)
                return;

            if (!float.TryParse(tb_AffixValue.Text, out float value))
            {
                tb_AffixValue.ForeColor = Color.Red;
            }
            else
            {
                tb_AffixValue.ForeColor = DefaultForeColor;
            }

            CItemAffix affix = m_Item.Affixes[m_SelectedAffixIndex];
            affix.AffixValue = value;

            list_Affixes.Refresh();
        }

        private void combo_AffixType_SelectedIndexChanged(object _sender, EventArgs e)
        {
            if (m_Item == null || m_SelectedAffixIndex < 0 || m_SelectedAffixIndex >= m_Item.Affixes.Count)
                return;

            CItemAffix affix = m_Item.Affixes[m_SelectedAffixIndex];
            affix.AffixType = (eItemAffixType)((ComboBox)_sender).SelectedIndex;

            list_Affixes.Refresh();
        }

        private void list_Affixes_SelectedIndexChanged(object _sender, EventArgs _e)
        {
            m_SelectedAffixIndex = ((ListBox)_sender).SelectedIndex;
            if (m_Item == null || m_SelectedAffixIndex < 0 || m_SelectedAffixIndex > m_Item.Affixes.Count)
            {
                ClearAffixEditor();
                return;
            }

            var affix = m_Item.Affixes[m_SelectedAffixIndex];

            combo_AffixOperator.Enabled = true;
            combo_AffixOperator.SelectedIndex = (int)affix.AffixOperator;

            tb_AffixValue.Enabled = true;
            tb_AffixValue.Text = affix.AffixValue.ToString();

            combo_AffixType.Enabled = true;
            combo_AffixType.SelectedIndex = (int)affix.AffixType;

            btn_RemoveAffix.Enabled = true;
        }

        private void list_Affixes_DrawItem(object _sender, DrawItemEventArgs _e)
        {
            if (_e.Index < 0 || _e.Index > list_Affixes.Items.Count)
                return;

            string? str = list_Affixes.Items[_e.Index].ToString();
            if (str == null)
                str = Constants.NullStr;

            Font? font = _e.Font;
            if (font == null)
                font = ((ListBox)_sender).Font;

            Brush brush = new SolidBrush(_e.ForeColor);

            _e.DrawBackground();
            _e.Graphics.DrawString(str, font, brush, _e.Bounds);
            _e.DrawFocusRectangle();
        }

        private void btn_AddAffix_Click(object sender, EventArgs e)
        {
            if (m_Item == null)
                return;

            CItemAffix affix = new()
            {
                AffixOperator = eAffixOperator.Add,
                AffixValue = 0.0f,
                AffixType = eItemAffixType.Power,
            };
            m_Item.Affixes.Add(affix);

            m_SelectedAffixIndex = m_Item.Affixes.Count - 1;

            list_Affixes.Items.Add(affix);
            list_Affixes.SelectedIndex = m_SelectedAffixIndex;
        }

        private void btn_RemoveAffix_Click(object sender, EventArgs e)
        {
            if (m_Item == null || m_SelectedAffixIndex < 0 || m_SelectedAffixIndex >= m_Item.Affixes.Count)
                return;

            m_Item.Affixes.RemoveAt(m_SelectedAffixIndex);
            list_Affixes.Items.RemoveAt(m_SelectedAffixIndex);

            m_SelectedAffixIndex = -1;
            list_Affixes.SelectedIndex = -1;
        }



        private CItem? m_Item = null;

        private int m_SelectedAffixIndex = -1;
        private bool m_SuspendAffixListEvent = false; // hack;

        private Color m_BorderColor = DefaultBackColor;

        private Brush m_AffixListTextDrawBrush;



        private static Color[] s_RarityColors =
        {
            Color.YellowGreen,
            Color.Silver,
            SystemColors.HotTrack,
            Color.Gold,
            Color.Magenta,
            Color.Red,
            Color.Red,
        };






    }

}
