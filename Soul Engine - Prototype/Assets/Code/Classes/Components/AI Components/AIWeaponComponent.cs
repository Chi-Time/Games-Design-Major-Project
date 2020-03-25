using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (WeaponSystemComponent))]
	public class AIWeaponComponent : MonoBehaviour
	{
		private WeaponSystemComponent _WeaponSystem = null;

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
