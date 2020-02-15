using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Intrinsics
{
    [MarkdownExporter]
	public class LoadStoreIntinsicBenchmark
	{
		private int[] data;

		[Params(32*1000, 32*1024, 51*1000, 50*1024, 358000, 350*1024)]
		public int Length { get; set; }

		[Params(8, 32)]
		public int Alignment { get; set; }

		[GlobalSetup]
		public unsafe void GlobalSetup()
		{
			for (; ; )
			{
				data = Enumerable.Range(0, Length).ToArray();

				fixed (int* ptr = data)
				{
					if ((Alignment == 32 && (uint)ptr % 32 == 0) || (Alignment == 8 && (uint)ptr % 16 != 0))
					{
						break;
					}
				}
			}
		}

		[Benchmark(Baseline=true)]
		public void LoadStore() => LoadStoreIntinsicHelper.LoadStore(data);

		[Benchmark]
		public void LoadStoreArrayAligned() => LoadStoreIntinsicHelper.LoadStoreArrayAligned(data);

		[Benchmark]
		public void LoadStoreAligned() => LoadStoreIntinsicHelper.LoadStoreAligned(data);

		[Benchmark]
		public void LoadStoreAlignedNonTemporal() => LoadStoreIntinsicHelper.LoadStoreAlignedNonTemporal(data);

	}
}
