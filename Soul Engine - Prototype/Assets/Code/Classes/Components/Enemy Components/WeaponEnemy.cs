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

		protected override void OnTriggerEnter2D (Collider2D other)
		{
		}

		protected void Update ()
		{
			_WeaponSystem.Fire ();
		}
	}
}
