using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIneupUsageEstimator
{
    public interface IUsageCalculator
    {
        List<Dictionary<int, int>> calculate();
        List<Dictionary<int, int>> calculate(Func<int, String, int, int, int, int, int> createRowFunc);
    }
}
