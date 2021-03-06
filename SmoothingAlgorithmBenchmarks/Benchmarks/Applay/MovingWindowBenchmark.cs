using BenchmarkDotNet.Attributes;
using SmoothingAlgorithmBenchmarks.Configs;
using SmoothingAlgorithms;

namespace SmoothingAlgorithmBenchmarks
{

    [Config(typeof(CommonApplayConfig))]
    public class MovingWindowBenchmark : CommonSmoothingAlgorithmBenchmark<SmoothingAlgorithmW>
    {
        [Benchmark(Baseline = true)]
        public override void RunApplay() => base.RunApplay();
    }
}