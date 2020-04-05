using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SoulEngine
{
	[RequireComponent (typeof (CircleCollider2D),typeof (OutlineComponent), typeof (TagComponent))]
	public class RescueComponent : MonoBehaviour
	{
		public GameObject GameObject => gameObject;

		[Tooltip ("The score awarded upon collection"), SerializeField]
		private int _Score = 0;
		[Tooltip ("The length of time it takes to rescue the character."), SerializeField]
		private float _RescueTime = 0.0f;
		[Tooltip ("The radius of detection for rescuing."), SerializeField]
		private float _RescueRadius = 1.5f;
		[Tooltip ("The various tags to look for."), SerializeField]
		private TagController _TagController = new TagController ();
		
		private float _Counter = 0.0f;
		private bool _IsRescuing = false;
		private GameObject _GameObject = null;
		private OutlineComponent _Outline = null;

		private void Awake ()
		{
			SetupCollider ();

			_GameObject = gameObject;
		}
		
		private void SetupCollider ()
		{
			var collider = GetComponent<CircleCollider2D> ();
			collider.isTrigger = true;
			collider.radius = ( _RescueRadius - 0.5f );
		}

		private void Start ()
		{
			SetupOutline ();
			ResizeOutline ();
		}

		private void SetupOutline ()
		{
			_Outline = GetComponent<OutlineComponent> ();
			_Outline.Show (false);
		}

		private void ResizeOutline ()
		{
			var children = transform.GetComponentsInChildren<Transform> ();
			
			// Remove the children from the parent so that they don't get scaled.
			foreach (Transform child in children)
				child.parent = null;
			
			_Outline.Resize (_RescueRadius);
			
			// Add them back now that the scaling is complete.
			foreach (Transform child in children)
				child.SetParent (transform);
		}

		private void OnEnable ()
		{
			Reset ();
		}

		private void Reset ()
		{
			_Counter = 0.0f;
			_IsRescuing = false;
		}

		private void Update ()
		{
			CalculateTimer ();

			if (TimerHasPassed ())
				Rescue ();
		}

		private void CalculateTimer ()
		{
			if (_IsRescuing)
				_Counter += Time.deltaTime;
			else
				_Counter -= Time.deltaTime;
			
			if (_Counter <= 0.0)
				_Counter = 0.0f;
		}
		
		private bool TimerHasPassed ()
		{
			return _Counter >= _RescueTime;
		}

		private void Rescue ()
		{
			_GameObject.SetActive (false);
			
			LevelSignals.OnEntityRescued?.Invoke (_GameObject);
			LevelSignals.OnScoreIncreased?.Invoke (_Score);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				StartRescue (true);
			}
		}

		private void OnTriggerExit2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				StartRescue (false);
			}
		}
		
		private void StartRescue (bool rescue)
		{
			_IsRescuing = rescue;
			_Outline.Show (rescue);
		}
	}
}
