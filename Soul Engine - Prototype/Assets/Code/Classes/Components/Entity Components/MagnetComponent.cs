using System;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Rigidbody2D), typeof (Collider2D), typeof (TagComponent))]
	public class MagnetComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		private Transform _Transform = null;
		private Rigidbody2D _Rigidbody2D = null;
		private TagComponent _TagComponent = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (Rigidbody2D),
				typeof (TagComponent)
			};
		}

		protected void Awake ()
		{
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.gravityScale = 0.0f;
			_Rigidbody2D.freezeRotation = true;

			_Transform = GetComponent<Transform> ();
			GetComponent<Collider2D> ().isTrigger = true;
			_TagComponent = GetComponent<TagComponent> ();
		}
	}
}
