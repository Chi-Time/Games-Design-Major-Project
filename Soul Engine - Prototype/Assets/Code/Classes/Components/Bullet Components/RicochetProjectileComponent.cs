using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulEngine
{
	public class RicochetProjectileComponent : BulletComponent
	{
		[Header ("Ricochet Settings")]
		[Space (2)]
		[Tooltip ("The angle of the projectile. The higher the value the tighter the angle."), Range (1, 5), SerializeField]
		private float _Angle = 2.0f;
		//TODO: Find a way to change this so it uses screen boundaries instead.
		[Tooltip ("The boundaries that the bullet can reach before bouncing back"), SerializeField]
		private Boundary _XBounds = Boundary.Zero;

		protected override void OnEnable ()
		{
			base.OnEnable ();
			SetStartAngle ();
		}

		private void SetStartAngle ()
		{
			_Angle = Random.Range (0, 2) == 1 ? _Angle : -_Angle;
		}

		private void FixedUpdate ()
		{
			CheckPosition ();
			Move ();
		}

		private void CheckPosition ()
		{
			if (_Transform.position.x < _XBounds.Min || _Transform.position.x > _XBounds.Max)
				_Angle = -_Angle;
		}

		private void Move ()
		{
			var speed = _Speed * Time.deltaTime;
			var delta = (Vector2) _Transform.up + new Vector2 (_Angle, 0.0f);
			delta.Normalize ();
			
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + delta * speed);
		}

		protected override void EnteredCollider (Collider2D other)
		{
			LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		protected override void ExitedCollider (Collider2D other)
		{
		}
	}
}
