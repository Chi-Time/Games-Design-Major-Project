using UnityEngine;

namespace SoulEngine
{
	public class BombAbilityComponent : AbilityComponent
	{
		protected override void Activate ()
		{
			LevelSignals.OnBombExploded?.Invoke ();
			Deactivate ();
		}

		protected override void Deactivate ()
		{
			_IsInUse = false;
		}
	}
}
