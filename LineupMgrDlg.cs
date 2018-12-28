using System;
using System.Collections.Generic;
using System.Windows.Forms;
using somReporter;

namespace LIneupUsageEstimator
{
    public partial class LineupMgrDlg : Form
    {
        List<LineupBalanceItem> balanceItems;

        public LineupDataObj WorkingLineup { get; set; }

        public LineupMgrDlg() : this(false) 
        {
        }

        public LineupMgrDlg(LineupDataObj originalData) : this(false)
        {
            WorkingLineup = originalData;

            String s= WorkingLineup.BalanceItemFrom.ToString();
            int i = CB_FROM.FindString(s);
            CB_FROM.SelectedIndex = i;
            CB_TO.SelectedItem = WorkingLineup.BalanceItemTo;
            radioButtonLH.Checked = WorkingLineup.PitcherArm.Equals("L");
            radioButtonRH.Checked = !WorkingLineup.PitcherArm.Equals("L");
            updateLabel();
        }

        public LineupMgrDlg(Boolean testMode)
        {
            if (!testMode)
            {
                InitializeComponent();
            }

            WorkingLineup = new LineupDataObj(RecordIndex.getNextId(RecordIndex.INDEX.LineupDataId));
            balanceItems = LineupTools.buildDefaultLineupTypes();

            if (!testMode)
                populateComboBoxes(CB_FROM, CB_TO, null);
        }


        private void LineupMgrDlg_Load(object sender, EventArgs e)
        {
        }

        private void updateLabel()
        {
            String handed = radioButtonLH.Checked ? "L" : radioButtonRH.Checked ? "R" : "";

            if (CB_FROM.SelectedItem != null && CB_TO.SelectedItem != null)
            {
                this.LABEL_LINEUP_NAME.Text = handed + " " + ((LineupBalanceItem)CB_FROM.SelectedItem).ToString() + "-" + ((LineupBalanceItem)CB_TO.SelectedItem).ToString();
            }
        }

        
        public void populateComboBoxes( ComboBox cbFrom, ComboBox cbTo, ComboBox current)
        {
            int valueFrom = -999;
            int valueTo = 999;

            if (cbFrom.SelectedItem != null)
            {
                valueFrom = ((LineupBalanceItem)cbFrom.SelectedItem).Value;
            }
            if (cbTo.SelectedItem != null)
            {
                valueTo = ((LineupBalanceItem)cbTo.SelectedItem).Value;
            }

            if(current == null )
            {
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
            if(CB_FROM.SelectedItem == null || CB_TO.SelectedItem == null ||
                ((LineupBalanceItem)CB_FROM.SelectedItem).Value > ((LineupBalanceItem)CB_TO.SelectedItem).Value)
            {
                MessageBox.Show("FROM Selection must be less than or equal to the TO Selection");
                return;
            }
            WorkingLineup = new LineupDataObj(RecordIndex.getNextId(RecordIndex.INDEX.LineupDataId));
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
