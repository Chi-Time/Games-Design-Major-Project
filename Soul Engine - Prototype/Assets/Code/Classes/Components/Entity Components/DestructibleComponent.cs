using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Rigidbody2D),typeof (Collider2D), typeof (HealthComponent))]
	[RequireComponent (typeof (ItemDropComponent))]
	public class DestructibleComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		[Tooltip("The score that this object will award upon breaking."), SerializeField]
		private int _Score = 0;
		
		private HealthComponent _HealthComponent = null;
		private ItemDropComponent _ItemDropComponent = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (Rigidbody2D),
				typeof (HealthComponent),
			};
		}

		private void Awake ()
		{
			var rb2D = GetComponent<Rigidbody2D> ();
			rb2D.isKinematic = true;
			rb2D.gravityScale = 0.0f;
			rb2D.freezeRotation = true;
			
			GetComponent<Collider2D> ().isTrigger = true;
			_HealthComponent = GetComponent<HealthComponent> ();
			_ItemDropComponent = GetComponent<ItemDropComponent> ();
		}

		private void Break ()
		{
			//TODO: Play animation, give score, drop something from drop rate chance.
			this.gameObject.SetActive (false);
			LevelSignals.OnScoreIncreased?.Invoke (_Score);
		}

		private void OnEnable ()
		{
			LevelSignals.OnEntityHit += OnEntityHit;
			LevelSignals.OnEntityKilled += OnEntityKilled;
		}

		private void OnEntityHit (IDamage damageComponent, GameObject other)
		{
			if (Equals (gameObject, other))
			{
				_HealthComponent.TakeDamage (damageComponent.Damage);
			}
		}
		
		private void OnEntityKilled (GameObject other)
		{
			if (Equals (gameObject, other))
			{
				Break ();
				_ItemDropComponent.Drop ();
			}
		}
		
		private void OnDisable ()
		{
			LevelSignals.OnEntityHit -= OnEntityHit;
			LevelSignals.OnEntityKilled -= OnEntityKilled;
		}
	}
}
