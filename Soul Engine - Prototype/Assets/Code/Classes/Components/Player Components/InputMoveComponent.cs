using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	public class InputMoveComponent : MonoBehaviour, IRequireComponents
	{
		public Vector2 Direction { get; private set; }

		public float Speed 
		{
			get => _Speed;
			set => _Speed = value;
		}
		
		public GameObject GameObject => gameObject;
		
		[Tooltip ("The speed at which the object moves."), SerializeField]
		private float _Speed = 12.0f;
		[Tooltip ("The axis name to use for vertical input."), SerializeField]
		private string _VerticalAxis = "Vertical";
		[Tooltip ("The axis name to use for horizontal input."), SerializeField]
		private string _HorizontalAxis = "Horizontal";
		
		private bool _SwapAxes = false;
		private Vector3 prevPos = Vector3.zero;
		private Transform _Transform = null;
		/// <summary>Reference to the object's Rigidbody component.</summary>
		private Rigidbody2D _Rigidbody2D = null;

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (Rigidbody2D)
			};
		}

		private void Awake ()
		{
			SetupRigidbody ();
			_Transform = GetComponent<Transform> ();
		}

		private void SetupRigidbody ()
		{
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.freezeRotation = true;
			_Rigidbody2D.gravityScale = 0.0f;
		}

		private void FixedUpdate ()
		{
			_Transform.Translate (CalculateInputVelocity ());
			Direction = (_Transform.position - prevPos).normalized;
			prevPos = _Transform.position;
			//_Rigidbody2D.MovePosition ((Vector2)_Transform.localPosition + CalculateInputVelocity ());
		}

		private Vector2 CalculateInputVelocity ()
		{
			float maxMagnitude = 1.0f;
			
			var inputVelocity = GetInput ();
		
			// https://forum.unity.com/threads/character-moving-too-fast-in-diagonal-movement.476179/
			//Normalise input if we exceed magnitude to stop diagonal speedup.
			if (inputVelocity.magnitude >= maxMagnitude)
				inputVelocity.Normalize ();

			return inputVelocity * (_Speed * Time.fixedDeltaTime);
		}

		private Vector2 GetInput ()
		{
			if (_SwapAxes)
				return new Vector2 (-Input.GetAxisRaw (_VerticalAxis), Input.GetAxisRaw (_HorizontalAxis));
			
			return new Vector2 (Input.GetAxisRaw (_HorizontalAxis), Input.GetAxisRaw (_VerticalAxis));
		}
		
		private void OnEnable ()
		{
			LevelSignals.OnPerspectiveSwitched += OnPerspectiveSwitched;
		}

		private void OnPerspectiveSwitched ()
		{
			_SwapAxes = !_SwapAxes;
		}

		private void OnDisable ()
		{
			LevelSignals.OnPerspectiveSwitched -= OnPerspectiveSwitched;
		}
	}
}
