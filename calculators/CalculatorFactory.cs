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
        public enum CalculatorType { SP_BASIC, SP_SCHEDULE, ALL_PITCHERS_AND_SCHEDULE };

        public static IUsageCalculator getCalculator(CalculatorType type, SOMTeamReportFile teamReportFile, Team targetTeam)
        {
            if (type == CalculatorType.SP_BASIC)
                return new SPOnly_Straight_Calculator(teamReportFile, targetTeam);
            else if (type == CalculatorType.SP_SCHEDULE)
                return new SPOnly_ScheduleWeighted_Calculator(teamReportFile, targetTeam);
            else if (type == CalculatorType.ALL_PITCHERS_AND_SCHEDULE)
                return new AllPitchers_ScheduleWeighted_Calculator(teamReportFile, targetTeam);

            throw new Exception("Unimplemented Calculator Type Requested");
        }
    }
}
