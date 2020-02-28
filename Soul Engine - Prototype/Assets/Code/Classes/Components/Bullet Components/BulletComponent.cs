using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	public abstract class BulletComponent : MonoBehaviour, IRequireComponents, IDamage
	{
		/// <summary>The damage the projectile inflicts.</summary>
		public int Damage => _Damage;
		public GameObject GameObject => gameObject;

		[Header ("Bullet Settings")]
		[Tooltip ("The amount of damage the projectile does to an enemy."), SerializeField]
		protected int _Damage = 0;
		[Tooltip("The projectile's speed across the world."), SerializeField]
		protected float _Speed = 0.0f;
		[Tooltip ("How long the projectile lives for before being culled."), SerializeField]
		protected float _Lifetime = 0.0f;
		[Tooltip ("The various tags this projectile can collide with."), SerializeField]
		protected bool _ShouldDestroy = false;

		protected Transform _Transform = null;
		protected Rigidbody2D _Rigidbody2D = null;
		protected TagComponent _TagComponent = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (Rigidbody2D),
				typeof (TagComponent),
			};
		}

		protected virtual void Awake ()
		{
			SetupRigidbody ();
			_Transform = GetComponent<Transform> ();
			GetComponent<Collider2D> ().isTrigger = true;
			_TagComponent = GetComponent<TagComponent> ();
		}

		private void SetupRigidbody ()
		{
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.gravityScale = 0.0f;
			_Rigidbody2D.freezeRotation = true;
		}

		protected virtual void OnEnable ()
		{
			Invoke (nameof(Cull), _Lifetime);
		}

		protected virtual void OnDisable ()
		{
			CancelInvoke (nameof(Cull));
		}

		protected virtual void Cull ()
		{
			if (_ShouldDestroy)
				Destroy (gameObject);

			gameObject.SetActive (false);
		}

		protected void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagComponent.Tags))
			{
				EnteredCollider (other);
			}
		}

		protected void OnTriggerExit2D (Collider2D other)
		{
			if (other.HasTags (_TagComponent.Tags))
			{
				ExitedCollider (other);
			}
		}

		protected abstract void EnteredCollider (Collider2D other);
		protected abstract void ExitedCollider (Collider2D other);
	}
}
