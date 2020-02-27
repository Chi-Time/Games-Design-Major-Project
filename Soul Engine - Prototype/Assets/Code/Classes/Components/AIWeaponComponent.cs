using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SoulEngine
{
	public class AIWeaponComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		private WeaponSystemComponent _WeaponSystem = null;
		
		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (WeaponSystemComponent)
			};
		}

		private void Awake ()
		{
			_WeaponSystem = GetComponent<WeaponSystemComponent> ();
		}

		private void Update ()
		{
			_WeaponSystem.Fire ();
		}
	}
}
