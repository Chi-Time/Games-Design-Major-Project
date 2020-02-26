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
					//TODO: Store bullet anchor position.
					bullet.gameObject.SetActive (true);
					bullet.position = _Transform.position;
					bullet.rotation = Quaternion.Euler (0, 0, currentAngle + _Transform.rotation.eulerAngles.z);
				}

				currentAngle += angleStep;
			}
		}
	}
}
