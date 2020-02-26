using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	public class InputMoveComponent : MonoBehaviour, IRequireComponents
	{
		[Tooltip ("The speed at which the object moves.")]
		[SerializeField] private float _Speed = 0.0f;

		/// <summary>Reference to the object's Rigidbody component.</summary>
		private Rigidbody2D _Rigidbody2D = null;

		public GameObject GameObject => gameObject;

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
			var h = Input.GetAxis ("Horizontal");
			var v = Input.GetAxis ("Vertical");
			var inputVelocity = new Vector3 (h, v, 0.0f);
			inputVelocity.Normalize ();

			inputVelocity *= _Speed * Time.fixedDeltaTime;

			return inputVelocity;
		}

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (Rigidbody2D)
			};
		}
	}
}
