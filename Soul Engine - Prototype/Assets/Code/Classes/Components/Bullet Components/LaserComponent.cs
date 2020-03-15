using UnityEngine;
using Utilities;

namespace SoulEngine
{
	public class LaserComponent : BulletComponent
	{
		[Tooltip ("How far is the reach of the laser weapon in the game?"), SerializeField]
		private float _Range = 0.0f;
		[Tooltip ("How often can the weapon hurt the player whilst they're inside of it?"), SerializeField]
		private Regulator _DamageRegulator = null;

		//TODO: Replace this debug with an actual graphic.
		private void OnDrawGizmos ()
		{
			if (_Transform != null)
				Debug.DrawRay (_Transform.position, Vector2.up * _Range, Color.green);
		}

		protected override void OnEnable ()
		{
		}

		protected override void OnDisable ()
		{
		}

		private void Update ()
		{
			CastRay ();
			_DamageRegulator.Tick ();
		}

		private void CastRay ()
		{
			var hit = Physics2D.Raycast (_Transform.position, Vector2.up, _Range);
			
			if (hit.collider.HasTags (_TagController.Tags))
			{
				EnteredCollider (hit.collider);
			}
		}

		protected override void EnteredCollider (Collider2D other)
		{
			if (_DamageRegulator.HasElapsed (false))
				LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		//TODO: Consider removing mandatory check from every bullet and only perform it in Bubble where it's needed.
		protected override void ExitedCollider (Collider2D other)
		{
		}
	}
}
