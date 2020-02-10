using BenchmarkDotNet.Attributes;
using SmoothingAlgorithmBenchmarks.Configs;
using SmoothingAlgorithms;

namespace SmoothingAlgorithmBenchmarks
{

    [Config(typeof(CommonApplayConfig))]
    public class MovingWindowUnrolledBenchmark : CommonSmoothingAlgorithmBenchmark<SmoothingAlgorithmUnroled>
    {
    }
}