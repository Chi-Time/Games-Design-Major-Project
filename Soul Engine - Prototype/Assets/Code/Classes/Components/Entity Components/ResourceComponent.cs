using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	public class ResourceComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		[Tooltip("The score that this object will award upon breaking."), SerializeField]
		private int _Score = 0;
		[Tooltip ("The value of this resource upon being collected."), SerializeField]
		private int _Value = 0;
		[Tooltip ("The tag to look for when colliding with the player."), SerializeField]
		private string _PlayerTag = "Player";

		private TagComponent _TagComponent = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				null,
			};
		}

		private void Awake ()
		{
			GetComponent<Collider2D> ().isTrigger = true;
			_TagComponent = GetComponent<TagComponent> ();
		}

		private void Collect ()
		{
			this.gameObject.SetActive (false);
			LevelSignals.OnScoreIncreased?.Invoke (_Score);
			LevelSignals.OnResourceCollected?.Invoke (_Value);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagComponent.Tags))
			{
				Collect ();
			}
		}
	}
}
