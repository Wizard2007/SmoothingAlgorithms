using BenchmarkDotNet.Attributes;
using SmoothingAlgorithmBenchmarks.Configs;
using SmoothingAlgorithms;

namespace SmoothingAlgorithmBenchmarks
{
    [Config(typeof(CommonApplayConfig))]
    public class MovingWindowUnroledUnsafeIntrinsicsUnrolledPPAvx2Benchmark : CommonSmoothingAlgorithmBenchmark<SmoothingAlgorithmUnroledUnsafeIntrinsicsUnrolledPPAvx2>
    {
    }
}