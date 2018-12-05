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
    public partial class SettingsDlg : Form
    {
        public SettingsDlg()
        {
            InitializeComponent();
        }

        private void SettingsDlg_Load(object sender, EventArgs e)
        {
            this.txtABAddition.Text = Properties.Settings.Default.ABAddition;
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ABAddition = this.txtABAddition.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
