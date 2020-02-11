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
            yield return new object[] 
            {
                new double[] {1, 2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9},
                2,
                new double[] {3,4,5,6,7,6.2,5.4,4.6,3.8,3,4,5,6,7}
            };
        }
    }
}
