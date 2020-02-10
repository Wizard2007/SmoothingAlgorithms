﻿using System;
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
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowBenchmark), configApplay),
                /*BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledUnsafeBenchmark), configApplay),
                BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnrolledUnsafeIntrinsicsBenchmark), configApplay),
                */BenchmarkConverter.TypeToBenchmarks(typeof(MovingWindowUnsafeBenchmark), configApplay),
            });
        }
    }
}
