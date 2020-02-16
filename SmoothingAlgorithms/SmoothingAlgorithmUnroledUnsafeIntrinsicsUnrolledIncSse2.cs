using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnroledUnsafeIntrinsicsUnrolledIncSse2 : CommonSmoothingAlgorithm
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
                var aUnrolledEnd = aStart + (((resultSize - 1) >> 2) << 2);

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;
                var vWindowSize = Vector128.Create((double)windowSize);
                var shift2D = Vector128.Create(0, 2*sizeof(double));

                var vCurrent = Sse2.Add(Vector128.Create((Int64)aCurrent), shift2D);
                var vValueCurrent = Sse2.Add(Vector128.Create((Int64)valueCurrent), shift2D);
                var vValueWindowSize = Sse2.Add(Vector128.Create((Int64)valueWindowSize), shift2D);
                
                var vShiftIndex = Vector128.Create((Int64)4*sizeof(double));

                while(aCurrent < aUnrolledEnd)
                {
                    #region  1

                    Sse2.Store(
                        aCurrent, 
                        Sse2.Divide(                           
                            Sse2.Subtract( 
                                Sse2.LoadVector128((double*)vValueWindowSize.GetElement(0)) , 
                                Sse2.LoadVector128((double*)vValueCurrent.GetElement(0))),
                                vWindowSize
                        )
                    );

                    #endregion

                    #region  2

                    Sse2.Store(
                        (double*)vCurrent.GetElement(1), 
                        Sse2.Divide(                           
                            Sse2.Subtract( 
                                Sse2.LoadVector128((double*)vValueWindowSize.GetElement(1)) , 
                                Sse2.LoadVector128((double*)vValueCurrent.GetElement(1))),
                                vWindowSize
                        )
                    );    

                    #endregion

                    vCurrent = Sse2.Add(vCurrent,vShiftIndex);
                    vValueCurrent = Sse2.Add(vValueCurrent,vShiftIndex);
                    vValueWindowSize = Sse2.Add(vValueWindowSize,vShiftIndex);

                    aCurrent = (double*)vCurrent.GetElement(0);
                }

                valueWindowSize = (double*)vValueWindowSize.GetElement(0);
                valueCurrent = (double*)vValueCurrent.GetElement(0);

                while(aCurrent < aEnd)
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

                var shift1D = Vector128.Create(0, sizeof(double));

                vCurrent = Sse2.Add(Vector128.Create((Int64)aCurrent), shift1D);

                var vPrev = Sse2.Add(Vector128.Create((Int64)aPrev), shift1D);

                vShiftIndex = Vector128.Create((Int64)2*sizeof(double));

                while(aCurrent < aUnrolledEnd)
                {
                    #region  1

                    *aCurrent += *(double*)vPrev.GetElement(0);

                    #endregion

                    #region  2

                    *(double*)vCurrent.GetElement(1) += *(double*)vPrev.GetElement(1);

                    #endregion

                    vCurrent = Avx.Add(vCurrent, vShiftIndex);
                    vPrev = Avx.Add(vPrev, vShiftIndex);

                    aCurrent = (double*)vCurrent.GetElement(0);
                }

                aPrev = (double*)vPrev.GetElement(0);

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
