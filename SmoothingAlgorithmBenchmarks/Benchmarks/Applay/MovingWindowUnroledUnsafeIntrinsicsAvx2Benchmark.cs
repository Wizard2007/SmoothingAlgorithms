using BenchmarkDotNet.Attributes;
using SmoothingAlgorithmBenchmarks.Configs;
using SmoothingAlgorithms;

namespace SmoothingAlgorithmBenchmarks
{
    [Config(typeof(CommonApplayConfig))]
    public class MovingWindowUnroledUnsafeIntrinsicsAvx2Benchmark : CommonSmoothingAlgorithmBenchmark<SmoothingAlgorithmUnroledUnsafeIntrinsicsAvx2>
    {
    }
}