namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnsafe : CommonSmoothingAlgorithm
    {
        public unsafe override double[] Applay(double[] values, int halfWindow)
        {
            var windowSize = 2 * halfWindow + 1;
            var resultSize = values.Length - windowSize + 1;

            if (resultSize == 0) return null;

            var a = new double[resultSize];

            var sum = 0d;

            fixed (double* valueStart = values, aStart = a)
            {

                var valueCurrent = valueStart;
                var valueEndwindowSize = valueCurrent + windowSize;
                while (valueCurrent < valueEndwindowSize)
                {
                    sum += *valueCurrent;
                    valueCurrent++;
                }

                var aPrev = aStart;
                var aCurrent = aStart + 1;
                var aEnd = aStart + resultSize;

                *aPrev = sum;

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;

                while (aCurrent < aEnd)
                {
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    *aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;
                }

                *aPrev /= (windowSize);
            }

            return a;
        }
    }
}
