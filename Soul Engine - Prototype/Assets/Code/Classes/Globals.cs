using Code.Enums;

namespace Code.Classes
{
	public static class Globals
	{
		/// <summary>The standard time scale that the game operates at.</summary>
		public static readonly float StandardTimeScale = 1.0f;
		/// <summary>The timescale the game runs at when slowed.</summary>
		public static readonly float SlowedTimeScale = 0.75f;
		/// <summary>The timescale the game runs at when boosted.</summary>
		public static readonly float BoostTimeScale = 1.35f;
		/// <summary>The current difficulty mode of the game.</summary>
		public static Diificulty CurrentDifficulty = Diificulty.Normal;
	}
}
