using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using SmoothingAlgorithmBenchmarks.Configs;

namespace SmoothingAlgorithmBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var configApplay =  CommonApplayConfig.Create(DefaultConfig.Instance);

            BenchmarkRunner.Run(new[]{
/*                 BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnsafeBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledUnsafeBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledUnsafeIntrinsicsBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledPPBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsAvx2Benchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledAvx2Benchmark), configApplay), */
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledIncAvx2Benchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledIncAvxBenchmark), configApplay),
                //BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledAvxBenchmark), configApplay),               
                //BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnroledUnsafeIntrinsicsUnrolledPPAvx2Benchmark), configApplay),
            });
        }
    }
}
