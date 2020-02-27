using UnityEngine;

namespace SoulEngine
{
	public class MultiShotWeaponComponent : ProjectileWeaponComponent
	{
		[Range (1, 64), SerializeField]
		private float _ShotCount = 0;

		protected override void Shoot ()
		{
			if (_BulletPools.Length <= 0)
				return;

			var currentAngle = 0.0f;
			var angleStep = 360f / _ShotCount;

			for (int i = 0; i < _ShotCount; i++)
			{
				var bullet = _BulletPools[0].Get ()?.transform;

				if (bullet != null)
				{
					//BUG: Set transformations BEFORE setting the object as active.
					// IF we don't do this, then we transform the object after it's OnEnable has been called and screw up any and all calculations which are being performed.
					// This is a naff bug and a custom callback will need to be added so that we "get" bulletcomponents from the pool
					// And then proceed to fire a function when we want them to setup for use such as a .Construct () method.
					// This means that they can then do any and all of the calculations that they need to without fear of worrying about things moving.
					bullet.position = _Transform.position;
					bullet.rotation = Quaternion.Euler (0, 0, currentAngle + _Transform.rotation.eulerAngles.z);
					//TODO: Store bullet anchor position.
					bullet.gameObject.SetActive (true);
				}

				currentAngle += angleStep;
			}
		}
	}
}
