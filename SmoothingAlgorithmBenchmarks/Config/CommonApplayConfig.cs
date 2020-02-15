using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;

namespace SmoothingAlgorithmBenchmarks.Configs
{
    public class CommonApplayConfig : CommonConfig
    {
        public CommonApplayConfig()
        {
            Add(
                Job.Default
                    .With(RunStrategy.Monitoring)
                    .With(Jit.RyuJit)
                    .With(Platform.X64)
                    .With(CsProjCoreToolchain.NetCoreApp31)
                    .WithInvocationCount(256)
            );
        }
    }
}