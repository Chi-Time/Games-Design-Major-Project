using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	public class InputMoveComponent : MonoBehaviour, IRequireComponents
	{
		public GameObject GameObject => gameObject;
		
		[Tooltip ("The speed at which the object moves."), SerializeField]
		private float _Speed = 12.0f;
		[Tooltip ("The axis name to use for horizontal input."), SerializeField]
		private string _HorizontalAxis = "Horizontal";
		[Tooltip ("The axis name to use for vertical input."), SerializeField]
		private string _VerticalAxis = "Vertical";

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
		}

		private void SetupRigidbody ()
		{
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.freezeRotation = true;
			_Rigidbody2D.gravityScale = 0.0f;
		}

		private void Update ()
		{
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + CalculateInputVelocity ());
		}

		private Vector2 CalculateInputVelocity ()
		{
			float maxMagnitude = 1.0f;
			
			var inputVelocity = GetInput ();
			
			//Normalise input if we exceed magnitude to stop diagonal speedup.
			if (inputVelocity.magnitude >= maxMagnitude)
				inputVelocity.Normalize ();

			return inputVelocity * (_Speed * Time.fixedDeltaTime);
		}

		private Vector2 GetInput ()
		{
			return new Vector2 (Input.GetAxisRaw (_HorizontalAxis), Input.GetAxisRaw (_VerticalAxis));
		}
	}
}
