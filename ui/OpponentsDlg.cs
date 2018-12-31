using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using somReporter;
using LineupEngine;


namespace LIneupUsageEstimator
{
    public partial class OpponentsDlg : Form
    {
        private List<Team> allTeams;
        private TeamInfo storedTeamInfo;
        private Dictionary<Team, ComboBox> teamData = new Dictionary<Team, ComboBox>();
        private static String[] divisions = { "", "A", "B", "C", "D", "E", "F" };

        public OpponentsDlg(TeamInfo info, List<Team> allTeams)
        {
            this.allTeams = allTeams;
            this.storedTeamInfo = info;
            InitializeComponent();

            for (int i = 1; i < allTeams.Count; i++)
                this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20));

        }

        private void Opponents_Load(object sender, EventArgs e)
        {

            tableLayoutPanel.Controls.Add(buildHeaderLabel("Opponent"), 0, 0);
            tableLayoutPanel.Controls.Add(buildHeaderLabel("Division"), 1, 0);

            this.tableLayoutPanel.RowCount = allTeams.Count-1;
            Boolean initDatabase = false;
            if (storedTeamInfo.Team.Count == 0)
                initDatabase = true;

            if (initDatabase)
            {
                foreach (Team team in allTeams) {
                    storedTeamInfo.Team.Add(team);
                }
            }

            int row = 1;
            foreach (Team team in allTeams)
            {
                tableLayoutPanel.Controls.Add(buildTeamLabel(team.Name), 0, row);
                ComboBox cbbox = buildTeamComboBox();
                String savedSelection = lookupTeamsSavedSelection(team);
                if (savedSelection.Length > 0)
                    cbbox.SelectedText = savedSelection;
                teamData.Add(team, cbbox);
                tableLayoutPanel.Controls.Add(cbbox, 1, row++);
            }

            TXT_IN_DIV_COUNT.Text = storedTeamInfo.InDivisionGameCount.ToString();
            TXT_OUT_DIV_COUNT.Text = storedTeamInfo.OutofDivisionGameCount.ToString();

            BTN_SAVE.Enabled = !storedTeamInfo.hasEmptyData();

            setMemoText();
        }

        private String lookupTeamsSavedSelection(Team team)
        {
            foreach(Team t in storedTeamInfo.Team)
            {
                if( t.Abrv.Equals(team.Abrv ))
                {
                    return t.Division;
                }
            }
            return "";
        }

        private Label buildHeaderLabel(String text )
        {
            Label label = new Label();
            label.Text = text;
            label.Font = new Font(label.Font, FontStyle.Bold);
            return label;
        }

        private Label buildTeamLabel(String teamName)
        {
            Label label = new Label();
            label.Text = teamName;
            label.TextAlign = ContentAlignment.TopRight;
            label.AutoSize = true;
            return label;
        }

        private ComboBox buildTeamComboBox()
        {
            ComboBox cb = new ComboBox();
            foreach (string div in divisions)
            {
                cb.Items.Add(div);
            }
            cb.SelectedIndex = 0;
            cb.TextChanged += teamComboBoxValue_Changed;
            cb.TextUpdate += teamComboBoxValue_Changed;
            return cb;
        }

        private void setMemoText()
        {
            int gameCount = 0;
            if(this.TXT_IN_DIV_COUNT.Text.Length == 0 ||
               this.TXT_OUT_DIV_COUNT.Text.Length == 0 )
            {
                return;
            }
            try
            {
                foreach (Team team in storedTeamInfo.Team)
                {
                    ComboBox box = teamData[team];
                    String s = box.Text;
                    team.Division = s;
                }

                int inDivCount = Int32.Parse(this.TXT_IN_DIV_COUNT.Text);
                int outDivCount = Int32.Parse(this.TXT_OUT_DIV_COUNT.Text);

                Team firstTeam = null;
                foreach (Team team in teamData.Keys)
                {
                    if (firstTeam == null)
                    {
                        firstTeam = team;
                        team.Division = teamData[team].Text;
                    }
                    else
                    {
                        String division = teamData[team].Text;
                        if (firstTeam.Division.Equals(division))
                            gameCount += inDivCount;
                        else if (division.Length > 0)
                            gameCount += outDivCount;
                    }

                }
                if (firstTeam == null)
                    this.LABEL_IN_TOTAL.Text = "Loading...";
                else
                    this.LABEL_IN_TOTAL.Text = firstTeam.Division + " division total=" + gameCount;
            }
            catch (Exception) {
                this.LABEL_IN_TOTAL.Text = "Error";
            }
        }

        private void teamComboBoxValue_Changed(object sender, EventArgs e)
        {
            setMemoText();
            BTN_SAVE.Enabled = !storedTeamInfo.hasEmptyData();
        }

        private void TXT_DIV_COUNT_TextChanged(object sender, EventArgs e)
        {
            setMemoText();
            BTN_SAVE.Enabled = !storedTeamInfo.hasEmptyData();
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            foreach (Team team in storedTeamInfo.Team)
            {
                ComboBox box= teamData[team];
                String s = box.Text;
                team.Division = s;
            }

            storedTeamInfo.InDivisionGameCount = Convert.ToInt32(TXT_IN_DIV_COUNT.Text);
            storedTeamInfo.OutofDivisionGameCount = Convert.ToInt32(TXT_OUT_DIV_COUNT.Text);

            this.Close();
        }
    }
}
