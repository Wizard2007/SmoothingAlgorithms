namespace SmoothingAlgorithms
{

    public class SmoothingAlgorithmUnroled : CommonSmoothingAlgorithm
    {
        public override double[] Applay(double[] values, int halfWindow)
        {
            var windowSize = 2 * halfWindow + 1;
            var resultSize = values.Length - windowSize + 1;

            if (resultSize == 0) return null;

            var a = new double[resultSize];
            
            var sum = 0d;

            for (var i = 0; i < windowSize; i++)
            {
                sum += values[i];
            }

            a[0] = sum;

            var resultSizeUnroled = ((resultSize - 1) >> 2) << 2;
            for (var i = 1; i < resultSizeUnroled; )
            {
                var index = i - 1;
                a[i] = a[index] - values[index] + values[index + windowSize];
                a[index] /= windowSize;
                i++;

                index = i - 1;
                a[i] = a[index] - values[index] + values[index + windowSize];
                a[index] /= windowSize;
                i++;

                index = i - 1;
                a[i] = a[index] - values[index] + values[index + windowSize];
                a[index] /= windowSize;
                i++;

                index = i - 1;
                a[i] = a[index] - values[index] + values[index + windowSize];
                a[index] /= windowSize;
                i++;
            }

            for (var i = resultSizeUnroled + 1; i < resultSize; )
            {
                var index = i - 1;
                a[i] = a[index] - values[index] + values[index + windowSize];
                a[index] /= windowSize;
                i++;
            }

            a[resultSize - 1] /= (windowSize);
            

            return a;
        }
    }
}
