using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SmoothingAlgorithms
{

    public class SmoothingAlgorithmUnroledUnsafeIntrinsicsUnrolledIncAvx2 : CommonSmoothingAlgorithm
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
                var aUnrolledEnd = aStart + (((resultSize - 1) >> 4) << 4);

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;
                var vWindowSize = Vector256.Create((double)windowSize);

                var vCurrent = Vector256.Create(
                    (ulong)aCurrent,
                    (ulong)aCurrent + 4 * sizeof(double),
                    (ulong)aCurrent + 8 * sizeof(double),
                    (ulong)aCurrent + 12 * sizeof(double));

                var vValueCurrent = Vector256.Create(
                    (ulong)valueCurrent,
                    (ulong)valueCurrent + 4 * sizeof(double),
                    (ulong)valueCurrent + 8 * sizeof(double),
                    (ulong)valueCurrent + 12 * sizeof(double));


                var vValueWindowSize = Vector256.Create(
                    (ulong)valueWindowSize,
                    (ulong)valueWindowSize + 4 * sizeof(double),
                    (ulong)valueWindowSize + 8 * sizeof(double),
                    (ulong)valueWindowSize + 12 * sizeof(double));

                var vShiftIndex1 = Vector256.Create(16ul * sizeof(double));

                while (aCurrent < aUnrolledEnd)
                {
                    #region  1

                    Avx2.Store(
                        aCurrent,
                        Avx2.Divide(
                            Avx2.Subtract(
                                Avx2.LoadVector256((double*)vValueWindowSize.GetElement(0)),
                                Avx2.LoadVector256((double*)vValueCurrent.GetElement(0))),
                                vWindowSize
                        )
                    );

                    #endregion

                    #region  2

                    Avx2.Store(
                        (double*)vCurrent.GetElement(1),
                        Avx2.Divide(
                            Avx2.Subtract(
                                Avx2.LoadVector256((double*)vValueWindowSize.GetElement(1)),
                                Avx2.LoadVector256((double*)vValueCurrent.GetElement(1))),
                                vWindowSize
                        )
                    );

                    #endregion

                    #region  3

                    Avx2.Store(
                        (double*)vCurrent.GetElement(2),
                        Avx2.Divide(
                            Avx2.Subtract(
                                Avx2.LoadVector256((double*)vValueWindowSize.GetElement(2)),
                                Avx2.LoadVector256((double*)vValueCurrent.GetElement(2))),
                                vWindowSize
                        )
                    );

                    #endregion

                    #region  4

                    Avx2.Store(
                        (double*)vCurrent.GetElement(3),
                        Avx2.Divide(
                            Avx2.Subtract(
                                Avx2.LoadVector256((double*)vValueWindowSize.GetElement(3)),
                                Avx2.LoadVector256((double*)vValueCurrent.GetElement(3))),
                                vWindowSize
                        )
                    );

                    #endregion 

                    vCurrent = Avx2.Add(vCurrent.AsDouble(), vShiftIndex1.AsDouble()).AsUInt64();
                    vValueCurrent = Avx2.Add(vValueCurrent.AsDouble(), vShiftIndex1.AsDouble()).AsUInt64();
                    vValueWindowSize = Avx2.Add(vValueWindowSize.AsDouble(), vShiftIndex1.AsDouble()).AsUInt64();

                    aCurrent = (double*)vCurrent.GetElement(0);
                }

                valueWindowSize = (double*)vValueWindowSize.GetElement(0);
                valueCurrent = (double*)vValueCurrent.GetElement(0);

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

                aUnrolledEnd = aStart + (((resultSize - 1) >> 2) << 2);

                vCurrent = Vector256.Create(
                    (ulong)aCurrent,
                    (ulong)aCurrent + sizeof(double),
                    (ulong)aCurrent + 2 * sizeof(double),
                    (ulong)aCurrent + 3 * sizeof(double));

                var vPrev = Vector256.Create(
                    (ulong)aPrev,
                    (ulong)aPrev + sizeof(double),
                    (ulong)aPrev + 2 * sizeof(double),
                    (ulong)aPrev + 3 * sizeof(double));

                var vShiftIndex = Vector256.Create(4ul * sizeof(double));

                while (aCurrent < aUnrolledEnd)
                {
                    #region  1

                    *aCurrent += *(double*)vPrev.GetElement(0);

                    #endregion

                    #region  2

                    *(double*)vCurrent.GetElement(1) += *(double*)vPrev.GetElement(1);

                    #endregion

                    #region  3

                    *(double*)vCurrent.GetElement(2) += *(double*)vPrev.GetElement(2);

                    #endregion

                    #region  4

                    *(double*)vCurrent.GetElement(3) += *(double*)vPrev.GetElement(3);

                    #endregion 

                    vCurrent = Avx2.Add(vCurrent.AsDouble(), vShiftIndex.AsDouble()).AsUInt64();
                    vPrev = Avx2.Add(vPrev.AsDouble(), vShiftIndex.AsDouble()).AsUInt64();

                    aCurrent = (double*)vCurrent.GetElement(0);
                }

                aPrev = (double*)vPrev.GetElement(0);

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
