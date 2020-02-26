using UnityEngine;

namespace SoulEngine
{
	public abstract class ProjectileWeaponComponent : WeaponComponent
	{
		[Tooltip("The speed at which the weapon fires projectiles."), SerializeField]
		protected float _FireRate = 0.0f;

		private float _Counter = 0.0f;
		
		/// <summary>Fires a projectile from the weapon if it's cooldown is ok.</summary>
		public override void Fire ()
		{
			if (CanFire ())
				Shoot ();
		}

		protected override bool CanFire ()
		{
			if (_Counter >= _FireRate)
			{
				_Counter = 0.0f;
				return true;
			}
			
			return false;
		}

		protected override void CalculateTimers ()
		{
			_Counter += Time.deltaTime;
		}
	}
}
