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
using somReporter.team;
using LineupEngine;
using static LineupEngine.LineupEngine;

namespace LIneupUsageEstimator.ui
{
    public partial class PositionDepthDlg : Form
    {
        private List<Player> players = null;

        public PositionDepthDlg(List<Player> players)
        {
            InitializeComponent();
            this.players = players;
        }

        private void PositionDepthDlg_Load(object sender, EventArgs e)
        {
            addItemsToList(LIST_1B, getSortedListOfPlayers(POSITIONS.FIRSTBASE), POSITIONS.FIRSTBASE);
            addItemsToList(LIST_2B, getSortedListOfPlayers(POSITIONS.SECONDBASE), POSITIONS.SECONDBASE);
            addItemsToList(LIST_3B, getSortedListOfPlayers(POSITIONS.THIRDBASE), POSITIONS.THIRDBASE);
            addItemsToList(LIST_SS, getSortedListOfPlayers(POSITIONS.SHORTSTOP), POSITIONS.SHORTSTOP);
            addItemsToList(LIST_C, getSortedListOfPlayers(POSITIONS.CATCHER), POSITIONS.CATCHER);
            addItemsToList(LIST_LF, getSortedListOfPlayers(POSITIONS.LEFTFIELD), POSITIONS.LEFTFIELD);
            addItemsToList(LIST_CF, getSortedListOfPlayers(POSITIONS.CENTERFIELD), POSITIONS.CENTERFIELD);
            addItemsToList(LIST_RF, getSortedListOfPlayers(POSITIONS.RIGHTFIELD), POSITIONS.RIGHTFIELD);
        }



        private List<Player> getSortedListOfPlayers(POSITIONS pos)
        {
            List<Player> posPlayers = new List<Player>();

            foreach (Player player in players)
            {
                Defense def = player.Def;
                if(playsThisPosition(pos, def))
                {
                    posPlayers.Add(player);
                }
            }

            sortPlayers(pos, posPlayers);

            return posPlayers;
        }

        private Boolean playsThisPosition(POSITIONS expected, Defense def)
        {
           switch(expected)
            {
                case POSITIONS.CATCHER:
                    return def.CatcherRating.Length > 0;
                case POSITIONS.FIRSTBASE:
                    return def.FirstBaseRating.Length > 0;
                case POSITIONS.SECONDBASE:
                    return def.SecondBaseRating.Length > 0;
                case POSITIONS.THIRDBASE:
                    return def.ThirdBaseRating.Length > 0;
                case POSITIONS.SHORTSTOP:
                    return def.ShortstopRating.Length > 0;
                case POSITIONS.LEFTFIELD:
                    return def.LeftFieldRating.Length > 0;
                case POSITIONS.CENTERFIELD:
                    return def.CenterFieldRating.Length > 0;
                case POSITIONS.RIGHTFIELD:
                    return def.RightFieldRating.Length > 0;
            }

            return false;
        }


        private void sortPlayers( POSITIONS pos, List<Player> players)
        {
            IRankDefScorer scorer = RankDepthFactory.createDepthFactory(RankDepthFactory.DEPTH_ALGO.READ);
//            IRankDefScorer scorer = RankDepthFactory.createDepthFactory(RankDepthFactory.DEPTH_ALGO.SOMWORD);

            players.Sort(delegate (Player p1, Player p2)
            {
                double p1Score = 0;
                double p2Score = 0;

                switch (pos)
                {
                    case POSITIONS.CATCHER:
                   //     p1Score = scorer.calculateFirstBaseDefScore(p1.Def.getDefRating(p1.Def.CatcherRating), p1.Def.getERating(p1.Def.CatcherRating));
                  //      p2Score = scorer.calculateFirstBaseDefScore(p2.Def.getDefRating(p2.Def.CatcherRating), p2.Def.getERating(p2.Def.CatcherRating));
                        break;
                    case POSITIONS.FIRSTBASE:
                        p1Score = scorer.calculateFirstBaseDefScore(p1.Def.getDefRating(p1.Def.FirstBaseRating), p1.Def.getERating(p1.Def.FirstBaseRating));
                        p2Score = scorer.calculateFirstBaseDefScore(p2.Def.getDefRating(p2.Def.FirstBaseRating), p2.Def.getERating(p2.Def.FirstBaseRating));
                        break;
                    case POSITIONS.SECONDBASE:
                        p1Score = scorer.calculateSecondBaseDefScore(p1.Def.getDefRating(p1.Def.SecondBaseRating), p1.Def.getERating(p1.Def.SecondBaseRating));
                        p2Score = scorer.calculateSecondBaseDefScore(p2.Def.getDefRating(p2.Def.SecondBaseRating), p2.Def.getERating(p2.Def.SecondBaseRating));
                        break;
                    case POSITIONS.THIRDBASE:
                        p1Score = scorer.calculateThirdBaseDefScore(p1.Def.getDefRating(p1.Def.ThirdBaseRating), p1.Def.getERating(p1.Def.ThirdBaseRating));
                        p2Score = scorer.calculateThirdBaseDefScore(p2.Def.getDefRating(p2.Def.ThirdBaseRating), p2.Def.getERating(p2.Def.ThirdBaseRating));
                        break;
                    case POSITIONS.SHORTSTOP:
                        p1Score = scorer.calculateShortStopDefScore(p1.Def.getDefRating(p1.Def.ShortstopRating), p1.Def.getERating(p1.Def.ShortstopRating));
                        p2Score = scorer.calculateShortStopDefScore(p2.Def.getDefRating(p2.Def.ShortstopRating), p2.Def.getERating(p2.Def.ShortstopRating));
                        break;
                    case POSITIONS.LEFTFIELD:
                        p1Score = scorer.calculateLeftFieldDefScore(p1.Def.getDefRating(p1.Def.LeftFieldRating), p1.Def.getERating(p1.Def.LeftFieldRating));
                        p2Score = scorer.calculateLeftFieldDefScore(p2.Def.getDefRating(p2.Def.LeftFieldRating), p2.Def.getERating(p2.Def.LeftFieldRating));
                        break;
                    case POSITIONS.CENTERFIELD:
                        p1Score = scorer.calculateCenterFieldDefScore(p1.Def.getDefRating(p1.Def.CenterFieldRating), p1.Def.getERating(p1.Def.CenterFieldRating));
                        p2Score = scorer.calculateCenterFieldDefScore(p2.Def.getDefRating(p2.Def.CenterFieldRating), p2.Def.getERating(p2.Def.CenterFieldRating));
                        break;
                    case POSITIONS.RIGHTFIELD:
                        p1Score = scorer.calculateRightFieldDefScore(p1.Def.getDefRating(p1.Def.RightFieldRating), p1.Def.getERating(p1.Def.RightFieldRating));
                        p2Score = scorer.calculateRightFieldDefScore(p2.Def.getDefRating(p2.Def.RightFieldRating), p2.Def.getERating(p2.Def.RightFieldRating));
                        break;
                }
                return p1Score.CompareTo(p2Score);
            });
        }

        private void addItemsToList(ListView list, List<Player> sortedPlayers, POSITIONS pos)
        {
            int rank = 1;
            foreach (Player player in sortedPlayers)
            {
                String defRating = "N/A";
                switch (pos)
                {
                    case POSITIONS.CATCHER:
                        defRating = player.Def.CatcherRating;
                        break;
                    case POSITIONS.FIRSTBASE:
                        defRating = player.Def.FirstBaseRating;
                        break;
                    case POSITIONS.SECONDBASE:
                        defRating = player.Def.SecondBaseRating;
                        break;
                    case POSITIONS.THIRDBASE:
                        defRating = player.Def.ThirdBaseRating;
                        break;
                    case POSITIONS.SHORTSTOP:
                        defRating = player.Def.ShortstopRating;
                        break;
                    case POSITIONS.LEFTFIELD:
                        defRating = player.Def.LeftFieldRating;
                        break;
                    case POSITIONS.CENTERFIELD:
                        defRating = player.Def.CenterFieldRating;
                        break;
                    case POSITIONS.RIGHTFIELD:
                        defRating = player.Def.RightFieldRating;
                        break;
                }


                list.Items.Add(new ListViewItem(rank++.ToString() + ") " + player.Name + " " + defRating + " " + player.Actual + "ab"));
            }
        }
    }
}
