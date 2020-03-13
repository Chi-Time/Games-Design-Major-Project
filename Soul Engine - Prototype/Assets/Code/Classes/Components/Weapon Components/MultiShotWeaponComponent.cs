using UnityEngine;

namespace SoulEngine
{
	public class MultiShotWeaponComponent : ProjectileWeaponComponent
	{
		//TODO: Add spawn offset so that bullets aren't starting inside of the object.
		//TODO: Add use case for firing even number of projectiles.
		[Tooltip("The angle the bullet fires from the weapon."), SerializeField]
		private float _Angle = 180;
		[Tooltip ("The number of bullets to fire. (Use whole numbers only.)"), SerializeField]
		private float _ShotCount = 3f;
		[Tooltip ("The offset from the center of the weapon."), SerializeField]
		private float _SpawnOffset = 0.0f;

		private float _AngleStep = 0.0f;
		private float _StartAngle = 0.0f;

		protected void Start ()
		{
			_AngleStep = _Angle / _ShotCount;
			_StartAngle = -Mathf.Floor (_ShotCount / 2) * Mathf.Abs (_AngleStep);
		}

		protected override void Shoot ()
		{
			if (_BulletPools.Length <= 0)
				return;

			var currentAngle = _StartAngle;

			for (int i = 0; i < _ShotCount; i++)
			{
				var bullet = _BulletPools[0].Get ()?.transform;

				if (bullet != null)
				{
					float projectileDirXPosition =
						_Transform.position.x - Mathf.Sin (( currentAngle * Mathf.PI ) / 180) * _SpawnOffset;
					float projectileDirYPosition =
						_Transform.position.y + Mathf.Cos (( currentAngle * Mathf.PI ) / 180) * _SpawnOffset;
					bullet.position = new Vector3 (projectileDirXPosition, projectileDirYPosition, 0);
					bullet.rotation = Quaternion.Euler (0, 0, currentAngle + _Transform.rotation.eulerAngles.z);
					bullet.gameObject.SetActive (true);
				}
				
				currentAngle += _AngleStep;
			}
		}
	}
}
