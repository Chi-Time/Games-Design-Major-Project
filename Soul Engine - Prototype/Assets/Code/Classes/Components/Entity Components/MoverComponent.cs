using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class MoverComponent : MonoBehaviour
	{
		public float Speed => _Speed;
		public Transform Transform => _Transform;
		public Vector3 Velocity { get; private set; }
		public Vector3 Direction { get; private set; }
		
		[Tooltip ("How fast does the component move across the world?"), SerializeField]
		private float _Speed = 10.0f;
		
		private Transform _Transform = null;
		private Rigidbody2D _Rigidbody2D = null;
		private Vector3 _PreviousPosition = Vector3.zero;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.freezeRotation = true;
			_Rigidbody2D.isKinematic = true;
		}

		private void Start ()
		{
			_PreviousPosition = _Transform.position;
		}

		private void FixedUpdate ()
		{
			Move ();
			CalculateDirection ();
		}

		private void CalculateDirection ()
		{
			var currentPosition = _Transform.position;
			Direction = ( currentPosition - _PreviousPosition ).normalized;
			_PreviousPosition = currentPosition;
		}

		private void Move ()
		{
			Vector2 delta = _Transform.up * (_Speed * Time.fixedDeltaTime);
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + delta);
		}
	}
}
