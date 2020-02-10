namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmW : CommonSmoothingAlgorithm
    {
        public override double[] Applay(double[] values, int halfWindow)
        {
            var windowSize = 2 * halfWindow + 1;
            var resultSize = values.Length - windowSize + 1;

            if (resultSize == 0) return null;

            var a = new double[resultSize];
            //var a = new double[values.Length];
            var sum = 0d;

            for (var i = 0; i < windowSize; i++)
            {
                sum += values[i];
            }

            a[0] = sum;

            for (var i = 1; i < resultSize; i++)
            {
                var index = i - 1;
                a[i] = a[index] - values[index] + values[index + windowSize];
                a[index] /= windowSize;
            }

            a[resultSize - 1] /= (windowSize);

            return a;
        }
    }
}
