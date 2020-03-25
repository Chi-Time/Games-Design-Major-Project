using UnityEngine;

namespace SoulEngine
{
	public class HealAbilityComponent : AbilityComponent
	{
		private HealthComponent _PlayerHealth = null;

		private void Awake ()
		{
			var playerController = FindObjectOfType<PlayerController> ();
			_PlayerHealth = playerController.GetComponent<HealthComponent> ();
		}

		protected override void Activate ()
		{
			_PlayerHealth.Heal (int.MaxValue);
			Deactivate ();
		}

		protected override void Deactivate ()
		{
			_IsInUse = false;
		}
	}
}
