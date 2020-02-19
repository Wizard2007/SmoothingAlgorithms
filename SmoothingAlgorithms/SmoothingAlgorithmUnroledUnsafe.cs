namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnroledUnsafe : CommonSmoothingAlgorithm
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

                var resultSizeUnroled = ((resultSize - 1) >> 2) << 2;
                var aUnrolledEnd = aStart + resultSizeUnroled;

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;

                while (aCurrent < aUnrolledEnd)
                {
                    // 1
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    *aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;

                    // 2
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    *aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;

                    // 3
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    *aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;

                    // 4
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    *aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;
                }

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
