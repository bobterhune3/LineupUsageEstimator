using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using somReporter;

namespace LIneupUsageEstimator
{
    public class CalculatorFactory
    {
        public enum CalculatorType { BASIC };

        public static IUsageCalculator getCalculator(CalculatorType type, SOMTeamReportFile teamReportFile, Team targetTeam)
        {
            if (type == CalculatorType.BASIC)
                return new StartingPitcherOnlyCalculator(teamReportFile, targetTeam);
            else
                throw new Exception("Unimplemented Calculator Type Requested");
        }
    }
}
