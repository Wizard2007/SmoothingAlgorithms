using System.Linq;
using MathNet.Numerics.Statistics;

namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmMathNet : CommonSmoothingAlgorithm
    {
        public unsafe override double[] Applay(double[] values, int halfWindow)
        {
            var result =  Statistics.MovingAverage(values, 2*halfWindow + 1);
            return result.ToArray();
        }
    }
}
