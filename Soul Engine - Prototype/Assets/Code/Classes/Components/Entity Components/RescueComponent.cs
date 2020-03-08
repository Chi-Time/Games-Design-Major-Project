using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SoulEngine
{
	[RequireComponent (typeof (TagComponent))]
	public class RescueComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;

		[Tooltip ("The score awarded upon collection"), SerializeField]
		private int _Score = 0;
		[Tooltip ("The length of time it takes to rescue the character."), SerializeField]
		private float _RescueTime = 0.0f;
		
		private float _Counter = 0.0f;
		private bool _IsRescuing = false;
		private GameObject _GameObject = null;
		private TagController _TagController = new TagController ();

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (TagComponent),
			};
		}

		private void Awake ()
		{
			_GameObject = gameObject;
			_TagController.Construct (this);
			GetComponent<Collider2D> ().isTrigger = true;
		}

		private void OnEnable ()
		{
			_Counter = 0.0f;
			_IsRescuing = false;
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
		}

		private void OnDisable ()
		{
			LevelSignals.OnEntityRescued?.Invoke (_GameObject);
			LevelSignals.OnScoreIncreased?.Invoke (_Score);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				_IsRescuing = true;
			}
		}

		private void OnTriggerExit2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				_IsRescuing = false;
			}
		}
	}
}
