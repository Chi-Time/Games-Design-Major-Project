using System;
using UnityEngine;
using System.Collections.Generic;

namespace SoulEngine
{
	[RequireComponent (typeof (Collider2D), typeof (Rigidbody2D))]
	[RequireComponent (typeof (HealthComponent), typeof (ItemDropComponent), typeof (TagComponent))]
	public class EnemyComponent : MonoBehaviour, IRequireComponents, IDamage
	{
		/// <summary>The damage this projectile inflicts.</summary>
		public int Damage => _Damage;
		public GameObject GameObject => gameObject;

		[Tooltip ("The amount of damage the enemy does when colliding with the player."), SerializeField]
		protected int _Damage = 0;
		[SerializeField]
		protected TagController _TagController = new TagController ();
		
		protected HealthComponent _Health = null;
		protected ItemDropComponent _ItemDrop = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (HealthComponent),
			};
		}

		protected virtual void Awake ()
		{
			GetComponent<Collider2D> ().isTrigger = true;
			_Health = GetComponent<HealthComponent> ();
			_ItemDrop = GetComponent<ItemDropComponent> ();

			var rigidbody2D = GetComponent<Rigidbody2D> ();
			rigidbody2D.isKinematic = true;
			rigidbody2D.gravityScale = 0.0f;
			rigidbody2D.freezeRotation = true;
		}

		protected virtual void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				gameObject.SetActive (false);
				LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
			}
		}

		protected virtual void OnEnable ()
		{
			LevelSignals.OnEntityHit += OnEntityHit;
			LevelSignals.OnEntityKilled += OnEntityKilled;
			LevelSignals.OnBombExploded += OnBombExploded;
		}

		protected virtual void OnDestroy ()
		{
			LevelSignals.OnEntityHit -= OnEntityHit;
			LevelSignals.OnEntityKilled -= OnEntityKilled;
			LevelSignals.OnBombExploded -= OnBombExploded;
		}

		protected virtual void OnEntityHit (IDamage damage, GameObject other)
		{
			if (Equals (other, gameObject))
			{
				_Health.TakeDamage (damage.Damage);
			}
		}

		protected virtual void OnEntityKilled (GameObject other)
		{	
			if (Equals (gameObject, other))
			{
				_ItemDrop.Drop ();
			}
		}

		protected virtual void OnBombExploded ()
		{
			if (gameObject.activeInHierarchy)
				_Health.TakeDamage (int.MaxValue);
		}
	}
}
