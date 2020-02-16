﻿using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Intrinsics;
using SmoothingAlgorithmBenchmarks.Configs;

namespace SmoothingAlgorithmBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var configApplay =  CommonApplayConfig.Create(DefaultConfig.Instance);
            //BenchmarkRunner.Run<LoadStoreIntinsicBenchmark>();

            BenchmarkRunner.Run(new[]{
                #region Simple implementation

                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledBenchmark), configApplay),

                #endregion

                #region Unsafe code

/*                 BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnsafeBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledUnsafeBenchmark), configApplay), */

                #endregion

                #region Sse2

/*                 BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledUnsafeIntrinsicsBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledPPBenchmark), configApplay), */
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledIncSse2Benchmark), configApplay),

                #endregion

                #region Avx

                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledAvxBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledIncAvxBenchmark), configApplay), 
                
                #endregion

                #region Avx2

/*                 BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsAvx2Benchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledAvx2Benchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledIncAvx2Benchmark), configApplay),                            
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledPPAvx2Benchmark), configApplay), */

                #endregion
                
            });
        }
    }
}
