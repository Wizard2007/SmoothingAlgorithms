using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SmoothingAlgorithms
{
    public class SmoothingAlgorithmUnroledUnsafeIntrinsicsUnrolledAvx2 : CommonSmoothingAlgorithm
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
                var aUnrolledEnd = aStart + (((resultSize - 1) >> 4) << 4);

                valueCurrent = valueStart;

                var valueWindowSize = valueStart + windowSize;
                                var vWindowSize = Vector256.Create(
                    (double)windowSize, 
                    (double)windowSize, 
                    (double)windowSize, 
                    (double)windowSize);

                while(aCurrent < aUnrolledEnd)
                {
                    #region  1

                    Avx2.Store(
                        aCurrent, 
                        Avx2.Divide(                           
                            Avx2.Subtract( 
                                Avx2.LoadVector256(valueWindowSize) , 
                                Avx2.LoadVector256(valueCurrent)),
                                vWindowSize
                        )
                    );

                    #endregion

                    #region  2

                    Avx2.Store(
                        aCurrent + 4, 
                        Avx2.Divide(                           
                            Avx2.Subtract( 
                                Avx2.LoadVector256(valueWindowSize + 4) , 
                                Avx2.LoadVector256(valueCurrent + 4)),
                                vWindowSize
                        )
                    );    

                    #endregion

                    #region  3

                    Avx2.Store(
                        aCurrent + 8, 
                        Avx2.Divide(                           
                            Avx2.Subtract( 
                                Avx2.LoadVector256(valueWindowSize + 8) , 
                                Avx2.LoadVector256(valueCurrent + 8)),
                                vWindowSize
                        )
                    ); 

                    #endregion

                    #region  4

                    Avx2.Store(
                        aCurrent + 12, 
                        Avx2.Divide(                           
                            Avx2.Subtract( 
                                Avx2.LoadVector256(valueWindowSize + 12) , 
                                Avx2.LoadVector256(valueCurrent + 12)),
                                vWindowSize
                        )
                    ); 

                    #endregion 

                    valueWindowSize += 16;
                    valueCurrent += 16;
                    aCurrent += 16;
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

                aUnrolledEnd = aStart + (((resultSize - 1) >> 2) << 2);

                while(aCurrent < aUnrolledEnd)
                {
                    #region  1

                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    #endregion

                    #region  2

                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    #endregion

                    #region  3

                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    #endregion

                    #region  4

                    *aCurrent += *aPrev;
                    aCurrent++;
                    aPrev++;

                    #endregion                    
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
