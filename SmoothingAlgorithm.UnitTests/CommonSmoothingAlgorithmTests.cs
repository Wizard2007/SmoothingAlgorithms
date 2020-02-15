using SmoothingAlgorithms;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SmoothingAlgorithm.UnitTests
{
    [TestClass]
    public class CommonSmoothingAlgorithmTests<T> where T : CommonSmoothingAlgorithm, new()
    {
        private T _smoothingAlgorithm;

        public CommonSmoothingAlgorithmTests()
            => _smoothingAlgorithm = new T();

        [DataTestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        public void ApplayTest(double[] signal, int halfWindow, double[] expectedResult)
        {
            //Arrange

            //Act
            var result =_smoothingAlgorithm.Applay(signal, halfWindow);

            //Assert
            for(var i = 0; i < result.Length; i++)
            {
                result[i] = Math.Round(result[i], 2);
            }

            CollectionAssert.AreEqual(expectedResult, result);
        }

        public static IEnumerable<object[]> GetData()
        {
// first test
            yield return new object[] 
            {
                new double[] {1, 2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7}
            };
#region 6 - 10

//6
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1},
                2,
                new double[] {3,4,5,6,7,6.2}
            };
// 7
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2},
                2,
                new double[] {3,4,5,6,7,6.2,5.4}
            };
//8
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6}
            };
// 9
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8}
            };
// 10
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3}
            };

#endregion
        
#region 14 - 18

// 14
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7}
            };
// 15
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6}
            };
// 16
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4}
            };
// 17
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4}
            };
// 18
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6}
            };
#endregion

#region 22 - 26
// 22
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13}
            };
// 23
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15}
            };
// 24
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17}
            };
// 25
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21,23},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17,19}
            };
// 26
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21,23,3},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17,19,16.6}
            };
#endregion

#region 30 - 34
// 30
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21,23,3,4,5,6,6},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17,19,16.6,14,11.2,8.2,4.8}
            };
// 31
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21,23,3,4,5,6,6,8},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17,19,16.6,14,11.2,8.2,4.8,5.8}
            };
// 32
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21,23,3,4,5,6,6,8,10},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17,19,16.6,14,11.2,8.2,4.8,5.8,7}
            };
// 33
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21,23,3,4,5,6,6,8,10,12},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17,19,16.6,14,11.2,8.2,4.8,5.8,7,8.4}
            };
// 34
            yield return new object[] 
            {
                new double[] {1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9,3,5,7,9,11,13,15,17,19,21,23,3,4,5,6,6,8,10,12,14},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7,6.6,6.4,6.4,6.6,7,9,11,13,15,17,19,16.6,14,11.2,8.2,4.8,5.8,7,8.4,10}
            };
#endregion
        }
    }
}