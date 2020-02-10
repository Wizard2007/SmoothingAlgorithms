using System.Runtime.Intrinsics.X86;

namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnroledUnsafeIntrinsics : CommonSmoothingAlgorithm
    {
        public unsafe override double[] Applay(double[] values, int halfWindow)
        {
            var windowSize = 2 * halfWindow + 1;
            var resultSize = values.Length - windowSize + 1;

            if (resultSize == 0) return null;

            var a = new double[resultSize];
            
            var sum = 0d;
            fixed(double* valueStart = values, aStart = a)
            {

                var valueCurrent = valueStart;
                var valueEndwindowSize = valueCurrent + windowSize;
                while(valueCurrent < valueEndwindowSize)
                {
                    sum += *valueCurrent;
                    valueCurrent++;
                }

                var aPrev = aStart;
                var aCurrent = aStart + 1;
                var aEnd = aStart + resultSize;
                var aIntrinsics = aPrev;

                *aPrev = sum;

                var resultSizeUnroled = ((resultSize - 1) >> 1) << 1;
                var aUnrolledEnd = aStart + resultSizeUnroled;

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;

                var pWindowsSize = stackalloc double[2];

                pWindowsSize[0] = windowSize;
                pWindowsSize[1] = windowSize;

                var vWindowsSize = Sse2.LoadVector128(pWindowsSize);

                while(aCurrent < aUnrolledEnd)
                {
                    // 1
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    //*aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;

                    // 2
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    //*aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;

                    Sse2.Store(
                        aIntrinsics, 
                        Sse2.Divide( 
                            Sse2.LoadVector128(aIntrinsics) , 
                            vWindowsSize)
                    );

                    aIntrinsics = aPrev;
                    // 3
                    /*
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    *aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;
                    */
                    // 4
                    /*
                    *aCurrent = *aPrev - *valueCurrent + *valueWindowSize;
                    *aPrev /= windowSize;
                    aCurrent++;
                    aPrev++;
                    valueCurrent++;
                    valueWindowSize++;*/
                }

                while(aCurrent < aEnd)
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
