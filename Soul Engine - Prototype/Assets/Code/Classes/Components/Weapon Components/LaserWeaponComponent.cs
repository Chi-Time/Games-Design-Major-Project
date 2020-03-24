using UnityEngine;

namespace SoulEngine
{
	//TODO: Use state machine to control logic flow between firing and idle states to seperate concerns.
	//TODO: Find a better way to handle the laser weapon as having the weapon turn it on and off is not very good.
	//TODO: Maybe have the laser logic abstracted to the enemy where it decides how delayed to fire the next shot etc.
	public class LaserWeaponComponent : WeaponComponent
	{
		[Header ("Laser Weapon Settings")]
		[Space (2)]
		[Tooltip ("Is the laser always firing?"), SerializeField]
		private bool _IsAlwaysOn = false;
		[Tooltip ("How long does the laser fire for?"), SerializeField]
		private float _Length = 0.0f;
		[Tooltip ("How often does the weapon fire?"), SerializeField]
		private float _ShotDelay = 0.0f;

		/// <summary>Determines if the weapon is currently a laser.</summary>
		private bool _IsFiring = false;
		/// <summary>Counter to determine how much time has elapsed for the fire length check.</summary>
		private float _LengthCounter = 0.0f;
		/// <summary>Counter to determine how much time has elapsed for the shot delay check.</summary>
		private float _ShotDelayCounter = 0.0f;
		/// <summary>Reference to the laser projectile's transform component.</summary>
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
		
		protected override bool CanFire ()
		{
			// Not enough time has elapsed to fire?
			if (_ShotDelayCounter < _ShotDelay)
			{
				return false;
			}

			// Switch our state to firing reset values.
			_IsFiring = true;
			_ShotDelayCounter = 0.0f;
			return true;
		}

		protected override void Shoot ()
		{
			_Laser.gameObject.SetActive (true);

			gameObject.SetActive (true);
		}

		protected override void Update ()
		{
			base.Update ();

			if (_IsAlwaysOn)
			{
				Shoot ();
				return;
			}

			if (_IsFiring == false)
				Fire ();
			
			if (HasFinished ())
			{
				_Laser.gameObject.SetActive (false);
			}
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

		protected override void CalculateTimers ()
		{
			if (_IsAlwaysOn)
				return;
			
			if (_IsFiring)
				_LengthCounter += Time.unscaledDeltaTime;
			else 
				_ShotDelayCounter += Time.unscaledDeltaTime;
		}
	}
}
