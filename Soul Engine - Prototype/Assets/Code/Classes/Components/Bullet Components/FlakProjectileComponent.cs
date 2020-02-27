using System;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulEngine
{
	//TODO: Add pathfinding for bullet so that it rotates around and voids targets in it's way when it initially fires.
	//TODO: Find a better way to handle the logic. Currently turning off the renderer and tracking the parent is stupid and messy, RECONSIDER THIS WITH FREETIME.
	//TODO: Consider making bullet movement logic it's own component so that each bullet type simply adds the component with the relevant logic it needs.
	//TODO: Consider removing BulletComponent base class entirely and design new system with interfaces and simplified abstract class.
	public class FlakProjectileComponent : BulletComponent
	{
		[Header ("Flak Settings")]
		[Space (2)]
		[Tooltip ("Is this projectile fired by the player?"), SerializeField]
		private bool _IsPlayerProjectile = false;
		[Tooltip ("The max distance a flak explosion can occur from the point of firing."), SerializeField]
		private float _ExplosionDistance = 1.0f;
		[Tooltip ("The image to display to the player where the explosion will happen."), SerializeField]
		private Transform _Marker = null;
		[Tooltip ("The explosion to spawn upon detonation."), SerializeField]
		private ExplosionComponent _Explosion = null;

		private Transform _Target = null;
		private Renderer _Renderer = null;
		private CircleCollider2D _Collider2D = null;

		protected override void Awake ()
		{
			base.Awake ();
			_Renderer = GetComponent<Renderer> ();
			_Marker.gameObject.SetActive (false);
			_Explosion.gameObject.SetActive (false);
			_Collider2D = GetComponent<CircleCollider2D> ();
		}

		private void CacheTarget ()
		{
			if (_IsPlayerProjectile == false)
			{
				_Target = FindObjectOfType<PlayerController> ().transform;
			}
		}

		protected override void OnEnable ()
		{
			CacheTarget ();
			var position = SelectDetonationTarget ();
			SetupBullet (ref position);
			SetupMarker (ref position);
			SetupExplosion (ref position);
		}

		private void SetupBullet (ref Vector3 position)
		{
			_Renderer.enabled = true;
			_Collider2D.enabled = true;
			
			_Transform.position = _Transform.parent.position;
			_Transform.up = position - _Transform.position;
		}

		private void SetupMarker (ref Vector3 position)
		{
			_Marker.position = position;
			_Marker.gameObject.SetActive (true);
		}

		private void SetupExplosion (ref Vector3 position)
		{
			//TODO: Find a way around doing this, constructing the explosion is needless and prone to bugs.
			_Explosion.Construct (_Damage, _Tags);
			_Explosion.transform.position = position;
		}
		
		//TODO: Find a way to cache this value for enemies as we don't need to keep looking for them when we can simply store all of the spawned enemies in the level in a global object and then find out which of them are nearest us as the player.
		private Vector3 SelectDetonationTarget ()
		{
			if (_IsPlayerProjectile)
			{
				_Target = FindObjectOfType<EnemyComponent> ().transform;
			}
			
			return ( _Target.position );
		}

		protected override void OnDisable ()
		{
			_Marker.gameObject.SetActive (false);
		}
		
		//TODO: This is terrible and needs to be cleaned up as the concept of checking if the explosion has finished is stupid.
		private void Update ()
		{
			if (_Renderer.enabled)
			{
				float maxDistance = 1f;
				if (Vector3.Distance (_Transform.position, _Marker.position) < maxDistance)
				{
					Explode ();
				}
				
				_Rigidbody2D.MovePosition (_Rigidbody2D.position + (Vector2)_Transform.up * (_Speed * Time.deltaTime));
			}
			//If we're currently disabled.
			else
			{
				// And the explosion has ended.
				if (_Explosion.isActiveAndEnabled == false)
				{
					// Then deactivate ourselves for later use.
					_Transform.parent.gameObject.SetActive (false);
				}
			}
		}

		//TODO: Clean this up as it's doing a lot of different logic at once.
		private void Explode ()
		{
			//Deactivate the bullet and marker.
			_Renderer.enabled = false;
			_Collider2D.enabled = false;
			_Marker.gameObject.SetActive (false);

			_Explosion.gameObject.SetActive (true);
		}

		protected override void EnteredCollider (Collider2D other)
		{
			//If we've hit the player, then place our explosion at our current location and detonate.
			_Explosion.transform.position = _Transform.position;
			Explode ();
			LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		protected override void ExitedCollider (Collider2D other)
		{
		}
	}
}
