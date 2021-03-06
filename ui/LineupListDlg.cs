﻿using System;
using System.Windows.Forms;
using somReporter;
using LineupEngine;

namespace LIneupUsageEstimator
{
    public partial class LineupListDlg: Form
    {
        private Team team;
        private TeamLineup storedLineups;
        
        public Boolean ApplyAsTemplate { get; set; }

        public LineupListDlg(Team team, TeamLineup lineups)
        {
            this.team = team;
            this.storedLineups = lineups;
            InitializeComponent();
            ApplyAsTemplate = false;
        }


        private void LineupListDlg_Load(object sender, EventArgs e)
        {
            BTN_DELETE.Enabled = false;
            BTN_SAVE.Enabled = false;
            BTN_EDIT.Enabled = false;
            int index = 0;
            foreach(LineupData data in storedLineups.Lineups)
            {
                LineupDataObj obj = new LineupDataObj(data);
                listBox1.Items.Add(obj);
                BTN_DELETE.Enabled = true;
                index++;
            }
        }

        private void BTN_ADD_Click(object sender, EventArgs e)
        {
            LineupMgrDlg dlg = new LineupMgrDlg();
            if( dlg.ShowDialog(this) == DialogResult.OK )
            {
                LineupDataObj data = dlg.WorkingLineup;
                storedLineups.Lineups.Add(data.getLineupData());
                listBox1.Items.Add(data);

            }
            if(listBox1.Items.Count > 0 )
            {
                BTN_SAVE.Enabled = true;
                BTN_DELETE.Enabled = true;
            }
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            storedLineups.Lineups.Clear();

            foreach ( LineupDataObj item in listBox1.Items)
            {
                storedLineups.Lineups.Add(item.getLineupData());
            }
            this.Close();
        }

        public void applyConfigurationToAnotherTeam(TeamLineup team)
        {
            foreach (LineupDataObj item in listBox1.Items)
            {
                team.Lineups.Add(item.getLineupData());
            }
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_DELETE_Click(object sender, EventArgs e)
        {
            LineupDataObj selected = (LineupDataObj)listBox1.SelectedItem;
            if (selected != null)
            {
                listBox1.Items.Remove(selected);
                storedLineups.Lineups.Remove(selected.getLineupData());
                BTN_DELETE.Enabled = listBox1.Items.Count > 0;
                BTN_EDIT.Enabled = listBox1.Items.Count > 0;
                BTN_SAVE.Enabled = true;
            }
        }

        private void BTN_EDIT_Click(object sender, EventArgs e)
        {
            LineupDataObj selected = (LineupDataObj)listBox1.SelectedItem;
            LineupMgrDlg dlg = new LineupMgrDlg(selected);
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                int index = listBox1.Items.IndexOf(selected);
                listBox1.Items.Remove(selected);
                selected = dlg.WorkingLineup;
                listBox1.Items.Insert(index, selected);
                listBox1.Refresh();
                BTN_SAVE.Enabled = true;
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            BTN_EDIT.Enabled = true;
        }

        private void useAsTemplate_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show("This will overwrite any previously setup lineups after you hit save.  Do you want to continue?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes )
            {
                ApplyAsTemplate = true;
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            LineupDataObj selected = (LineupDataObj)listBox1.SelectedItem;
            int index = listBox1.Items.IndexOf(selected);
            listBox1.Items.Remove(selected);
            index = index - 1;
            listBox1.Items.Insert(index, selected);
            listBox1.Refresh();
            listBox1.SelectedItem = selected;

            btnMoveUp.Enabled = index > 0;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            LineupDataObj selected = (LineupDataObj)listBox1.SelectedItem;
            int index = listBox1.Items.IndexOf(selected);
            listBox1.Items.Remove(selected);
            index = index + 1;
            listBox1.Items.Insert(index, selected);
            listBox1.Refresh();
            listBox1.SelectedItem = selected;

            btnMoveUp.Enabled = index > listBox1.Items.Count - 1;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnMoveUp.Enabled = listBox1.SelectedIndex > 1;
            btnMoveDown.Enabled = listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < listBox1.Items.Count - 1;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            BTN_EDIT_Click(sender, e);
            btnMoveUp.Enabled = listBox1.SelectedIndex > 1;
            btnMoveDown.Enabled = listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < listBox1.Items.Count - 1;
        }
    }
}
