using UnityEngine;

namespace SoulEngine
{
	public class PositionedWeapon : ProjectileWeaponComponent
	{
		[Tooltip ("The various anchors which will fire projectiles. (Must be in same order as projectiles array to ensure correct firing.)"), SerializeField]
		private Transform[] _Anchors = null;
		
		protected override void Shoot ()
		{
			//Consider removing the if statement to prevent branch mis-predictions and perform check elsewhere.
			if (_Anchors.Length == _BulletPrefabs.Length || _Anchors.Length == _BulletPools.Length)
			{
				int i;
				for (i = 0; i < _BulletPrefabs.Length; i++)
				{
					var bullet = _BulletPools[i].Get ()?.transform;
					var anchor = _Anchors[i];

					if (anchor != null && bullet != null)
					{
						bullet.gameObject.SetActive (true);
						bullet.position = anchor.position;
						bullet.rotation = anchor.rotation;
					}
				}
			}
		}
	}
}
