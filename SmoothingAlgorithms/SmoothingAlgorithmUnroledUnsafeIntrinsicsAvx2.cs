using System.Runtime.Intrinsics.X86;

namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnroledUnsafeIntrinsicsAvx2 : CommonSmoothingAlgorithm
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

                var aCurrent = aStart + 1;
                var aEnd = aStart + resultSize;

                var resultSizeUnroled = ((resultSize - 1) >> 2) << 2;
                var aUnrolledEnd = aStart + resultSizeUnroled;

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;

                var pWindowSize = stackalloc double[4] {windowSize, windowSize, windowSize, windowSize};
                var vWindowSize = Avx2.LoadVector256(pWindowSize);

                while(aCurrent < aUnrolledEnd)
                {

                    Avx2.Store(
                        aCurrent, 
                        Avx2.Divide(                           
                            Avx2.Subtract( 
                                Avx2.LoadVector256(valueWindowSize) , 
                                Avx2.LoadVector256(valueCurrent)),
                                vWindowSize
                        )
                    );

                    valueWindowSize += 4;
                    valueCurrent += 4;
                    aCurrent += 4;
                }

                while(aCurrent < aEnd)
                {
                    *aCurrent = (*valueWindowSize - *valueCurrent) /windowSize;
                    aCurrent++;
                    valueCurrent++;
                    valueWindowSize++;
                }

                var aPrev = aStart;
                aCurrent = aStart + 1;
                aEnd = aStart + resultSize;

                *aPrev = sum / windowSize;

                resultSizeUnroled = ((resultSize - 1) >> 1) << 1;
                aUnrolledEnd = aStart + resultSizeUnroled;

                while(aCurrent < aUnrolledEnd)
                {
                    // 1
                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    // 2
                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;
                }

                while(aCurrent < aEnd)
                {
                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;
                }
            }

            return a;
        }
    }

}
