using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using SmoothingAlgorithms;

namespace SmoothingAlgorithmBenchmarks
{
    public abstract class CommonSmoothingAlgorithmBenchmark<T> where T : CommonSmoothingAlgorithm, new()
    {
        #region Private fields

        private T _smoothingAlgorithm;
        private double[] _buffer;

        #endregion

        #region Public properties

        [ParamsSource(nameof(Buffers))]
        public int N { get; set; }

        public IEnumerable<int> Buffers => new[] { /*2800, 3500, 28000, 35000,*/ 280000, 350000 };


        [ParamsSource(nameof(HalfWindows))]
        public int HalfWindow { get; set; }

        public IEnumerable<int> HalfWindows => new[] { 7, 15, 20 };

        #endregion

        #region Constructor

        public CommonSmoothingAlgorithmBenchmark()
            => _smoothingAlgorithm = new T();

        #endregion

        #region Benchmarks

        [Benchmark]
        public virtual void RunApplay()
            => _smoothingAlgorithm.Applay(_buffer, HalfWindow);

        #endregion

        #region Iteration Setup

        [IterationSetup]
        public void IterationSetup()
        {
            _buffer = new double[N];

            for (var i = 0; i < _buffer.Length; i++)
            {
                _buffer[i] = 1;
            }
        }

        #endregion
    }
}