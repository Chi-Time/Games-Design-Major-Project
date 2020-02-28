using System;
using UnityEngine;
using System.Collections.Generic;

namespace SoulEngine
{
	public class EnemyComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;
		
		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (HealthComponent),
				typeof (Collider2D),
				typeof (Rigidbody2D)
			};
		}
	}
}
