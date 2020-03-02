using System;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

namespace SoulEngine
{
	public class MagnetComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		[Tooltip ("How fast does the magnetised object move toward the player."), SerializeField]
		private float _Speed = 0.0f;

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

		//TODO: Consider moving the resource pieces in their own component.
		private void OnTriggerStay2D (Collider2D other)
		{
			// Grab the other transform.
			var target = other.transform;

			// Make sure we're not already tweening the other transform.
			if (other.HasTags (_TagComponent.Tags) && IsActive (target.GetInstanceID ()) == false)
			{
				Tween.Position (target,
				                _Transform.position,
				                _Speed,
				                0,
				                Tween.EaseIn,
				                Tween.LoopType.None,
				                null);
			}
		}

		private bool IsActive (int instanceID)
		{
			// Check the active tweens list to determine if we're already active.
			foreach (var tween in Tween.activeTweens)
			{
				if (tween.targetInstanceID == instanceID)
					return true;
			}

			return false;
		}

		private void OnTriggerExit2D (Collider2D other)
		{
			if (other.HasTags (_TagComponent.Tags))
			{
				Tween.Stop (other.transform.GetInstanceID ());
			}
		}
	}
}
