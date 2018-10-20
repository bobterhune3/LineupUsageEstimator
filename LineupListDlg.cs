using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using somReporter;

namespace LIneupUsageEstimator
{
    public partial class LineupListDlg: Form
    {
        private Team team;

        public LineupListDlg(Team team)
        {
            this.team = team;
            InitializeComponent();
        }


        private void LineupListDlg_Load(object sender, EventArgs e)
        {
            BTN_DELETE.Enabled = false;
            BTN_SAVE.Enabled = false;
            BTN_EDIT.Enabled = false;
        }

        private void BTN_ADD_Click(object sender, EventArgs e)
        {
            LineupMgrDlg dlg = new LineupMgrDlg();
            if( dlg.ShowDialog(this) == DialogResult.OK )
            {
                listBox1.Items.Add(dlg.WorkingLineup);
            }
            if(listBox1.Items.Count > 0 )
            {
                BTN_SAVE.Enabled = true;
                BTN_DELETE.Enabled = true;
            }
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_DELETE_Click(object sender, EventArgs e)
        {

        }

        private void BTN_EDIT_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            BTN_EDIT.Enabled = true;
        }
    }
}
