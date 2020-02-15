using BenchmarkDotNet.Attributes;
using SmoothingAlgorithmBenchmarks.Configs;
using SmoothingAlgorithms;

namespace SmoothingAlgorithmBenchmarks
{
    
    [Config(typeof(CommonApplayConfig))]
    public class MovingWindowUnroledUnsafeIntrinsicsUnrolledAvx2Benchmark : CommonSmoothingAlgorithmBenchmark<SmoothingAlgorithmUnroledUnsafeIntrinsicsUnrolledAvx2>
    {
    }
}