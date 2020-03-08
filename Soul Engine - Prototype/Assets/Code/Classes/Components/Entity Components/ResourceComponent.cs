using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (TagComponent), typeof (Collider2D))]
	public class ResourceComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		[Tooltip("The score that this object will award upon breaking."), SerializeField]
		private int _Score = 0;
		[Tooltip ("The value of this resource upon being collected."), SerializeField]
		private int _Value = 0;
		[Tooltip ("The distance away from the player before being collected."), SerializeField]
		private float _Distance = 0.25f;
		[Tooltip ("The various tags this component should look for."), SerializeField]
		private TagController _TagController = new TagController ();

		private Transform _Target = null;
		private Transform _Transform = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				null,
			};
		}

		private void Awake ()
		{
			_TagController.Construct (this);
			_Transform = GetComponent<Transform> ();
			GetComponent<Collider2D> ().isTrigger = true;
		}

		private void Start ()
		{
			_Target = FindObjectOfType<PlayerController> ()?.transform;
		}

		private void Collect ()
		{
			this.gameObject.SetActive (false);
			LevelSignals.OnScoreIncreased?.Invoke (_Score);
			LevelSignals.OnResourceCollected?.Invoke (_Value);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				Collect ();
			}
		}
	}
}
