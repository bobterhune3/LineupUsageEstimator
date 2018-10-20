using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIneupUsageEstimator
{
    public partial class LineupMgrDlg : Form
    {
        List<LineupBalanceItem> balanceItems = new List<LineupBalanceItem>();

        public Lineup WorkingLineup { get; set; }

        public LineupMgrDlg() : this(false) 
        {
        }

        public LineupMgrDlg(Boolean testMode)
        {
            if (!testMode)
            {
                InitializeComponent();
            }

            WorkingLineup = new Lineup();

            balanceItems.Add(new LineupBalanceItem(0, 9, "L"));
            balanceItems.Add(new LineupBalanceItem(1, 8, "L"));
            balanceItems.Add(new LineupBalanceItem(2, 7, "L"));
            balanceItems.Add(new LineupBalanceItem(3, 6, "L"));
            balanceItems.Add(new LineupBalanceItem(4, 5, "L"));
            balanceItems.Add(new LineupBalanceItem(5, 4, "L"));
            balanceItems.Add(new LineupBalanceItem(6, 3, "L"));
            balanceItems.Add(new LineupBalanceItem(7, 2, "L"));
            balanceItems.Add(new LineupBalanceItem(8, 1, "L"));
            balanceItems.Add(new LineupBalanceItem(9, 0, ""));
            balanceItems.Add(new LineupBalanceItem(10, 1, "R"));
            balanceItems.Add(new LineupBalanceItem(11, 2, "R"));
            balanceItems.Add(new LineupBalanceItem(12, 3, "R"));
            balanceItems.Add(new LineupBalanceItem(13, 4, "R"));
            balanceItems.Add(new LineupBalanceItem(14, 5, "R"));
            balanceItems.Add(new LineupBalanceItem(15, 6, "R"));
            balanceItems.Add(new LineupBalanceItem(16, 7, "R"));
            balanceItems.Add(new LineupBalanceItem(17, 8, "R"));
            balanceItems.Add(new LineupBalanceItem(18, 9, "R"));
        }


        private void LineupMgrDlg_Load(object sender, EventArgs e)
        {
            populateComboBoxes(CB_FROM, CB_TO, null);
        }

        private void updateLabel()
        {
            String handed = radioButtonLH.Checked ? "L" : radioButtonRH.Checked ? "R" : "";

            if (CB_FROM.SelectedItem != null && CB_TO.SelectedItem != null)
            {
                /*
                WorkingLineup.PitcherArm = handed;
                WorkingLineup.StartBalanceLevel = calculateLevelValue(CB_FROM);
                if (CB_FROM.SelectedItem.Equals("E"))
                    WorkingLineup.StartBalanceArm = "E";
                else
                    WorkingLineup.StartBalanceArm = CB_FROM.SelectedItem.ToString().Substring(1, 1);
                WorkingLineup.EndBalanceLevel = calculateLevelValue(CB_TO);
                if(CB_TO.SelectedItem.Equals("E"))
                    WorkingLineup.EndBalanceArm = "E";
                else
                    WorkingLineup.EndBalanceArm = CB_TO.SelectedItem.ToString().Substring(1, 1);
                    */
                this.LABEL_LINEUP_NAME.Text = WorkingLineup.ToString();
            }
        }

        
        public void populateComboBoxes( ComboBox cbFrom, ComboBox cbTo, ComboBox current)
        {
            int valueFrom = -999;
            int valueTo = 999;

            if ( cbFrom.SelectedItem != null )
            {
                valueFrom = ((LineupBalanceItem)cbFrom.SelectedItem).Value;
            }
            if (cbTo.SelectedItem != null)
            {
                valueTo = ((LineupBalanceItem)cbTo.SelectedItem).Value;
            }

            if(current == null )
            {
                // Initial Setup
                cbFrom.Items.Clear();
                cbTo.Items.Clear();

                foreach( LineupBalanceItem item in balanceItems)
                {
                    cbFrom.Items.Add(item);
                    cbTo.Items.Add(item);
                }
            }
            else
            {
                if( current == cbTo )
                {
                    if(cbFrom.SelectedItem == null || getComboBoxValue(cbFrom) > getComboBoxValue(cbTo))
                    {
                        cbFrom.Items.Clear();
                        for (int i = 0; i <= ((LineupBalanceItem)cbTo.SelectedItem).Value; i++)
                        {
                            cbFrom.Items.Add(balanceItems[i]);
                        }
                    }
                }
                else if (current == cbFrom )
                {
                    if (cbTo.SelectedItem == null || getComboBoxValue(cbTo) < getComboBoxValue(cbFrom))
                    {
                        cbTo.Items.Clear();
                        for (int i = ((LineupBalanceItem)cbFrom.SelectedItem).Value; i < balanceItems.Count; i++)
                        {
                            cbTo.Items.Add(balanceItems[i]);
                        }
                    }

                }
            }
        }

        private int getComboBoxValue(ComboBox cb)
        {
            return ((LineupBalanceItem)cb.SelectedItem).Value;
        }

        private int calculateLevelValue(ComboBox cb)
        {
            String toLevel = cb.SelectedItem.ToString().Substring(0, 1);
            int levelVal = 0;
            if (!toLevel.Equals("E"))
                levelVal = Convert.ToInt32(toLevel);
            return levelVal;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
        }


        private void CB_FROM_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateLabel();
            populateComboBoxes(CB_FROM, CB_TO, CB_FROM);
        }

        private void CB_TO_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateLabel();
            populateComboBoxes(CB_FROM, CB_TO, CB_TO);
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            if( ((LineupBalanceItem)CB_FROM.SelectedItem).Value > ((LineupBalanceItem)CB_TO.SelectedItem).Value)
            {
                MessageBox.Show("FROM Selection must be less than or equal to the TO Selection");
                return;
            }
            WorkingLineup = new Lineup();
            WorkingLineup.BalanceItemFrom = (LineupBalanceItem)CB_FROM.SelectedItem;
            WorkingLineup.BalanceItemTo = (LineupBalanceItem)CB_TO.SelectedItem;
            WorkingLineup.PitcherArm = radioButtonLH.Checked ? "L" : "R";

            this.Close();
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
