using System.Runtime.Intrinsics.X86;

namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnroledUnsafeIntrinsicsUnrolledPP : CommonSmoothingAlgorithm
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
                    sum += *(valueCurrent++);
                }

                var aCurrent = aStart + 1;
                var aEnd = aStart + resultSize;

                var aUnrolledEnd = aStart + (((resultSize - 1) >> 3) << 3);

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;

                var pWindowSize = stackalloc double[2] {windowSize, windowSize};
                var vWindowSize = Sse2.LoadVector128(pWindowSize);

                while(aCurrent < aUnrolledEnd)
                {
                    // 1
                    Sse2.Store(
                        aCurrent, 
                        Sse2.Divide(                           
                            Sse2.Subtract( 
                                Sse2.LoadVector128(valueWindowSize) , 
                                Sse2.LoadVector128(valueCurrent)),
                                vWindowSize
                        )
                    );

                    // 2
                    Sse2.Store(
                        aCurrent + 2, 
                        Sse2.Divide(                           
                            Sse2.Subtract( 
                                Sse2.LoadVector128(valueWindowSize + 2) , 
                                Sse2.LoadVector128(valueCurrent + 2)),
                                vWindowSize
                        )
                    );    

                    // 3
                    Sse2.Store(
                        aCurrent + 4, 
                        Sse2.Divide(                           
                            Sse2.Subtract( 
                                Sse2.LoadVector128(valueWindowSize + 4) , 
                                Sse2.LoadVector128(valueCurrent + 4)),
                                vWindowSize
                        )
                    ); 

                    // 4
                    Sse2.Store(
                        aCurrent + 6, 
                        Sse2.Divide(                           
                            Sse2.Subtract( 
                                Sse2.LoadVector128(valueWindowSize + 6) , 
                                Sse2.LoadVector128(valueCurrent + 6)),
                                vWindowSize
                        )
                    ); 

                    valueWindowSize += 8;
                    valueCurrent += 8;
                    aCurrent += 8;
                }

                while(aCurrent < aEnd)
                {
                    *(aCurrent++) = (*(valueWindowSize++) - *(valueCurrent++)) /windowSize;
                }

                var aPrev = aStart;
                aCurrent = aStart + 1;
                aEnd = aStart + resultSize;

                *aPrev = sum / windowSize;

                aUnrolledEnd = aStart + (((resultSize - 1) >> 1) << 1);

                while(aCurrent < aUnrolledEnd)
                {
                    // 1
                    *(aCurrent++) += *(aPrev++);

                    // 2
                    *(aCurrent++) += *(aPrev++);
                }

                while(aCurrent < aEnd)
                {
                    *(aCurrent++) += *(aPrev++);
                }
            }

            return a;
        }
    }
}
