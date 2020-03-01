using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (WeaponSystemComponent))]
	public class WeaponEnemy : EnemyComponent
	{
		private WeaponSystemComponent _WeaponSystem = null;

		protected override void Awake ()
		{
			base.Awake ();
			_WeaponSystem = GetComponent<WeaponSystemComponent> ();
		}

		protected void Update ()
		{
			_WeaponSystem.Fire ();
		}
	}
}
