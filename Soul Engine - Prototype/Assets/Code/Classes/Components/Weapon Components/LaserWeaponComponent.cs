using UnityEngine;

namespace SoulEngine
{
	//TODO: Find a better way to handle the laser weapon as having the weapon turn it on and off is not very good.
	//TODO: Maybe have the laser logic abstracted to the enemy where it decides how delayed to fire the next shot etc.
	public class LaserWeaponComponent : WeaponComponent
	{
		[Header ("Laser Weapon Settings")]
		[Space (2)]
		[Tooltip ("How long does the laser fire for?"), SerializeField]
		private float _Length = 0.0f;
		[Tooltip ("How often does the weapon fire?"), SerializeField]
		private float _ShotDelay = 0.0f;

		private bool _IsFiring = false;
		private float _LengthCounter = 0.0f;
		private float _ShotDelayCounter = 0.0f;
		private Transform _Laser = null;

		private void Start ()
		{
			_Laser = _BulletPools[0].Get ()?.transform;
			
			_Laser.gameObject.SetActive (false);
			_Laser.position = _Transform.localPosition;
			_Laser.localRotation = _Transform.localRotation;
		}

		public override void Fire ()
		{
			if (CanFire ())
			{
				Shoot ();
			}
		}

		protected override void Shoot ()
		{
			_Laser.gameObject.SetActive (true);

			gameObject.SetActive (true);
		}

		protected override bool CanFire ()
		{
			if (_ShotDelayCounter < _ShotDelay)
			{
				return false;
			}

			_IsFiring = true;
			_ShotDelayCounter = 0.0f;
			return true;
		}

		private bool HasFinished ()
		{
			if (_LengthCounter < _Length)
			{
				return false;
			}

			_IsFiring = false;
			_LengthCounter = 0.0f;
			return true;
		}

		protected override void Update ()
		{
			base.Update ();
			
			if (HasFinished ())
			{
				_Laser.gameObject.SetActive (false);
			}
		}

		protected override void CalculateTimers ()
		{
			if (_IsFiring)
				_LengthCounter += Time.deltaTime;
			else 
				_ShotDelayCounter += Time.deltaTime;
		}
	}
}
