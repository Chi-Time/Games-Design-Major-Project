using System;
using UnityEngine;
using System.Collections.Generic;

namespace SoulEngine
{
	[RequireComponent (typeof (HealthComponent), typeof (TagComponent), typeof (Rigidbody2D))]
	public class EnemyComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		protected HealthComponent _Health = null;
		
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
			
			var rigidbody2D = GetComponent<Rigidbody2D> ();
			rigidbody2D.isKinematic = true;
			rigidbody2D.gravityScale = 0.0f;
			rigidbody2D.freezeRotation = true;
		}

		protected virtual void OnEnable ()
		{
			LevelSignals.OnEntityHit += OnEntityHit;
		}

		protected virtual void OnDisable ()
		{
			LevelSignals.OnEntityHit -= OnEntityHit;			
		}

		protected virtual void OnEntityHit (IDamage damage, GameObject other)
		{
			print ("HIT");
			if (Equals (other, gameObject))
			{
				_Health.TakeDamage (damage.Damage);
			}
		}
	}
}
