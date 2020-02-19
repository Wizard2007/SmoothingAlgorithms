using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnroledUnsafeIntrinsicsUnrolled : CommonSmoothingAlgorithm
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

                var aCurrent = aStart + 1;
                var aEnd = aStart + resultSize;

                var resultSizeUnroled = ((resultSize - 1) >> 3) << 3;
                var aUnrolledEnd = aStart + resultSizeUnroled;

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;
                var vWindowSize = Vector128.Create((double)windowSize, (double)windowSize);

                while (aCurrent < aUnrolledEnd)
                {
                    // 1
                    Sse2.Store(
                        aCurrent,
                        Sse2.Divide(
                            Sse2.Subtract(
                                Sse2.LoadVector128(valueWindowSize),
                                Sse2.LoadVector128(valueCurrent)),
                                vWindowSize
                        )
                    );

                    // 2
                    Sse2.Store(
                        aCurrent + 2,
                        Sse2.Divide(
                            Sse2.Subtract(
                                Sse2.LoadVector128(valueWindowSize + 2),
                                Sse2.LoadVector128(valueCurrent + 2)),
                                vWindowSize
                        )
                    );

                    // 3
                    Sse2.Store(
                        aCurrent + 4,
                        Sse2.Divide(
                            Sse2.Subtract(
                                Sse2.LoadVector128(valueWindowSize + 4),
                                Sse2.LoadVector128(valueCurrent + 4)),
                                vWindowSize
                        )
                    );

                    // 4
                    Sse2.Store(
                        aCurrent + 6,
                        Sse2.Divide(
                            Sse2.Subtract(
                                Sse2.LoadVector128(valueWindowSize + 6),
                                Sse2.LoadVector128(valueCurrent + 6)),
                                vWindowSize
                        )
                    );

                    valueWindowSize += 8;
                    valueCurrent += 8;
                    aCurrent += 8;
                }

                while (aCurrent < aEnd)
                {
                    *aCurrent = (*valueWindowSize - *valueCurrent) / windowSize;
                    aCurrent++;
                    valueCurrent++;
                    valueWindowSize++;
                }

                var aPrev = aStart;
                aCurrent = aStart + 1;
                aEnd = aStart + resultSize;

                *aPrev = sum / windowSize;

                resultSizeUnroled = ((resultSize - 1) >> 2) << 2;
                aUnrolledEnd = aStart + resultSizeUnroled;

                while (aCurrent < aUnrolledEnd)
                {
                    // 1
                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    // 2
                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    // 3
                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    // 4
                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;
                }

                while (aCurrent < aEnd)
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
