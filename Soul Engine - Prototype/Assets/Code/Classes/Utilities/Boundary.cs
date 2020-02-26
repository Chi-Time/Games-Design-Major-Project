using System;

namespace SoulEngine
{
	[Serializable]
	public struct Boundary
	{
		public static Boundary Zero => new Boundary (0, 0);

		public float Min;
		public float Max;

		public Boundary (float min)
		{
			Min = min;
			Max = -min;
		}
		
		public Boundary (float min, float max)
		{
			Min = min;
			Max = max;
		}
	}
}
