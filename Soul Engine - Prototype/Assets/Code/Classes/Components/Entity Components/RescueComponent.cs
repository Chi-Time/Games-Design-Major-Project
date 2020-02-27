using System;
using UnityEngine;
using System.Collections.Generic;

namespace SoulEngine
{
	public class RescueComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => _GameObject;

		[Tooltip ("The length of time it takes to rescue the character."), SerializeField]
		private float _RescueTime = 0.0f;
		//TODO: Consider only using a single string rather than checking an array as you're doing a foreach for what is probably only 1 player tag needing to be checkd.
		[Tooltip ("The tags this object should register with."), SerializeField]
		private string[] _Tags = null;
		
		private float _Counter = 0.0f;
		private bool _IsRescuing = false;
		private GameObject _GameObject = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				null,
			};
		}

		private void Awake ()
		{
			_GameObject = gameObject;
		}

		private void Update ()
		{
			CalculateTimer ();

			if (_Counter >= _RescueTime)
				Rescue ();
		}

		private void CalculateTimer ()
		{
			if (_IsRescuing == false)
				_Counter -= Time.deltaTime;
			else
				_Counter += Time.deltaTime;

			//TODO: Consider moving this out into the ontriggerenter method to prevent branch misprediction issues and the overhead of constantly calling an if statement.
			if (_Counter <= 0.0f)
				_Counter = 0.0f;
		}

		private void Rescue ()
		{
			_GameObject.SetActive (false);
			LevelSignals.OnEntityRescued?.Invoke (_GameObject);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.HasTags (_Tags))
			{
				_IsRescuing = true;
			}
		}

		private void OnTriggerExit2D (Collider2D other)
		{
			if (other.gameObject.HasTags (_Tags))
			{
				_IsRescuing = false;
			}
		}
	}
}
