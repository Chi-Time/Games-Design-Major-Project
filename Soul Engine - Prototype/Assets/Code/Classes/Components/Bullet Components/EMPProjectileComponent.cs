using UnityEngine;

namespace SoulEngine
{
	public class EMPProjectileComponent : BulletComponent
	{
		[Header ("Missile Specific Settings")]
		[Space (2)]
		[Tooltip ("Is the projectile fired by the player?"), SerializeField]
		private bool _IsPlayerProjectile = false;

		private Transform _Target = null;

		protected override void OnEnable ()
		{
			base.OnEnable ();

			AssignTarget ();
		}
		
		//TODO: Find a way to cache this value as we don't need to keep looking for the player when we can cache him at game start.</summary>
		//TODO: Find a way to cache this value for enemies as we don't need to keep looking for them when we can simply store all of the spawned enemies in the level in a global object and then find out which of them are nearest us as the player.
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
		
		private void Update ()
		{
			_Transform.up = _Target.position - _Transform.position;
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + (Vector2)_Transform.up * (_Speed * Time.deltaTime));
		}
		
		protected override void EnteredCollider (Collider2D other)
		{
			Cull ();
			LevelSignals.OnEntityEMP?.Invoke (other.gameObject);
			LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		protected override void ExitedCollider (Collider2D other)
		{
		}
	}
}
