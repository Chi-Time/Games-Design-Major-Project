using UnityEngine;

namespace Utilities
{
	//https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
	public static class LerpCurves
	{
		public enum LerpType
		{
			Linear,
			EaseIn,
			EaseOut,
			Exponential,
			Logarithmic,
			SmoothStep,
			SmootherStep
		}

		public static float Curve (float t, LerpType lerpType)
		{
			switch (lerpType)
			{
				// Ugly hack but makes usability easier.
				case LerpType.Linear:
					return t;
				case LerpType.EaseOut:
					return EaseOut (t);
				case LerpType.EaseIn:
					return EaseIn (t);
				case LerpType.Exponential:
					return Exponential (t);
				case LerpType.Logarithmic:
					return Logarithmic (t);
				case LerpType.SmoothStep:
					return SmoothStep (t);
				case LerpType.SmootherStep:
					return SmootherStep (t);
			}

			return EaseOut (t);
		}

		public static float EaseOut (float t)
		{
			return t = Mathf.Sin (t * Mathf.PI * 0.5f);
		}

		public static float EaseIn (float t)
		{
			return t = 1f - Mathf.Cos (t * Mathf.PI * 0.5f);
		}

		public static float Exponential (float t)
		{
			return t = t * t;
		}

		public static float Logarithmic (float t)
		{
			return t = t * 0.2f;
		}

		public static float SmoothStep (float t)
		{
			return t = t * t * ( 3f - 2f * t );
		}

		public static float SmootherStep (float t)
		{
			return t = t * t * t * ( t * ( 6f * t - 15f ) + 10f );
		}
	}
}
