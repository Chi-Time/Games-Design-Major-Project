using TMPro;
using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (LaserWeaponComponent))]
	public class LaserAbilityComponent : AbilityComponent
	{
		[Tooltip ("How long should the laser fire for?"), SerializeField]
		private float _Length = 0.0f;
		
		private LaserWeaponComponent _LaserWeaponComponent = null;

		private void Awake ()
		{
			_LaserWeaponComponent = GetComponent<LaserWeaponComponent> ();
			_LaserWeaponComponent.IsAlwaysOn = true;
			_LaserWeaponComponent.enabled = false;
		}

		protected override void Activate ()
		{
			_LaserWeaponComponent.enabled = true;

			Invoke (nameof(Deactivate), _Length);
		}

		protected override void Deactivate ()
		{
			_LaserWeaponComponent.enabled = false;
			_IsInUse = false;
		}
	}
}
