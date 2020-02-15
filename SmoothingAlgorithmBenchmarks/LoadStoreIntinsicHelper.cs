using System.Runtime.Intrinsics.X86;

namespace Intrinsics
{
    public static unsafe class LoadStoreIntinsicHelper
	{
		private const ulong AlignmentMask = 31UL;
		private const int VectorSizeInInts = 8;
		private const int BlockSizeInInts = 32;

		public static void LoadStore(int[] source)
		{
			fixed (int* ptr = &source[0])
			{
				var pos = 0;

				for (; pos <= source.Length - VectorSizeInInts; pos += VectorSizeInInts)
				{
					var current = Avx.LoadVector256(ptr + pos);
					Avx.Store(ptr + pos, current);
				}

			}
		}

		public static void LoadStoreArrayAligned(int[] source)
		{
			fixed (int* ptr = &source[0])
			{
				var aligned = (int*)(((ulong)ptr + AlignmentMask) & ~AlignmentMask);
				var pos = (int)(aligned - ptr);

				for (; pos <= source.Length - VectorSizeInInts; pos += VectorSizeInInts)
				{
					var current = Avx.LoadVector256(ptr + pos);
					Avx.Store(ptr + pos, current);
				}

			}
		}

		public static void LoadStoreAligned(int[] source)
		{
			fixed (int* ptr = &source[0])
			{
				var aligned = (int*)(((ulong)ptr + AlignmentMask) & ~AlignmentMask);
				var pos = (int)(aligned - ptr);

				for (; pos <= source.Length - VectorSizeInInts; pos += VectorSizeInInts)
				{
					var current = Avx.LoadVector256(ptr + pos);
					Avx.StoreAligned(ptr + pos, current);
				}

			}
		}

		public static void LoadStoreAlignedNonTemporal(int[] source)
		{
			fixed (int* ptr = &source[0])
			{
				var aligned = (int*)(((ulong)ptr + AlignmentMask) & ~AlignmentMask);
				var pos = (int)(aligned - ptr);

				for (; pos <= source.Length - VectorSizeInInts; pos += VectorSizeInInts)
				{
					var current = Avx.LoadVector256(ptr + pos);
					Avx.StoreAlignedNonTemporal(ptr + pos, current);
				}

			}
		}

		public static void Load(int[] source)
		{
			fixed (int* ptr = &source[0])
			{
				var pos = 0;

				for (; pos <= source.Length - VectorSizeInInts; pos += VectorSizeInInts)
				{
					var current = Avx.LoadVector256(ptr + pos);
				}

			}
		}

		public static void LoadArrayAligned(int[] source)
		{
			fixed (int* ptr = &source[0])
			{
				var aligned = (int*)(((ulong)ptr + AlignmentMask) & ~AlignmentMask);
				var pos = (int)(aligned - ptr);

				for (; pos <= source.Length - VectorSizeInInts; pos += VectorSizeInInts)
				{
					var current = Avx.LoadVector256(ptr + pos);
				}

			}
		}

		public static void LoadAligned(int[] source)
		{
			fixed (int* ptr = &source[0])
			{
				var aligned = (int*)(((ulong)ptr + AlignmentMask) & ~AlignmentMask);
				var pos = (int)(aligned - ptr);

				for (; pos <= source.Length - VectorSizeInInts; pos += VectorSizeInInts)
				{
					var current = Avx.LoadVector256(ptr + pos);
				}

			}
		}
	}
}
