﻿using UnityEngine;

namespace SoulEngine
{
	public class HomingMissileComponent : BulletComponent
	{
		[Header ("Missile Specific Settings")]
		[Space (2)]
		[Tooltip ("How long should the missile stay homed to the target?"), SerializeField]
		private float _LockOnLength = 0.0f;
		[Tooltip ("How long does it take for the missile to lock onto a target."), SerializeField]
		private float _TargetingDelay = 0.0f;
		[Tooltip ("Is the projectile fired by the player?"), SerializeField]
		private bool _IsPlayerProjectile = false;
		
		private Transform _Target = null;
		private bool _TrackTarget = false;

		protected override void OnEnable ()
		{
			base.OnEnable ();
			
			AssignTarget ();
			Invoke (nameof(TrackTarget), _TargetingDelay);
		}
		
		private void AssignTarget ()
		{
			if (_IsPlayerProjectile)
			{
				_Target = FindObjectOfType<EnemyComponent> ().transform;
			}
			else
			{
				_Target = FindObjectOfType<PlayerController> ().transform;
			}
		}

		protected override void OnDisable ()
		{
			base.OnDisable ();
			_Target = null;
		}
		
		private void Update ()
		{
			if (_TrackTarget == false)
				return;

			FaceTarget ();
		}

		private void FaceTarget ()
		{
			_Transform.up = _Target.position - _Transform.position;
		}

		private void FixedUpdate ()
		{
			Move ();
		}

		private void Move ()
		{
			var velocity = CalculateVelocity ();
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + velocity);
		}

		private Vector2 CalculateVelocity ()
		{
			Vector2 velocity = _Transform.up * ( _Speed * Time.deltaTime );

			// Clamp max magnitude so that we don't have diagonal speedups.
			if (velocity.magnitude > 1.0f)
				velocity.Normalize ();

			return velocity;
		}

		private void TrackTarget ()
		{
			_TrackTarget = true;
			Invoke (nameof(LoseTarget), _LockOnLength);
		}

		private void LoseTarget ()
		{
			_TrackTarget = false;
		}

		protected override void EnteredCollider (Collider2D other)
		{
			Cull ();
			LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		protected override void ExitedCollider (Collider2D other)
		{ }
	}
}
