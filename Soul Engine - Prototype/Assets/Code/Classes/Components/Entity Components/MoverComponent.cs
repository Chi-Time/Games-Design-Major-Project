using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	public class MoverComponent : MonoBehaviour
	{
		public Vector3 Direction { get; private set; }
		public Vector3 Velocity { get; private set; }
		public float Speed => _Speed;
		public Transform Trans => _Transform;
		
		[Tooltip ("How fast does the component move across the world?"), SerializeField]
		private float _Speed = 10.0f;

		private Vector3 prevPos = Vector3.zero;
		private Transform _Transform = null;
		public Rigidbody2D _Rigidbody2D = null;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.freezeRotation = true;
			_Rigidbody2D.isKinematic = true;
		}

		private void Start ()
		{
			prevPos = _Transform.position;
			//StartCoroutine (CalculateVelocity ());
		}

		private void FixedUpdate ()
		{
			Move ();
			Direction = (_Transform.position - prevPos).normalized;
			prevPos = _Transform.position;
		}

		private void Move ()
		{
			Vector2 delta = _Transform.up * (_Speed * Time.fixedDeltaTime);
			//_Transform.Translate (delta);
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + delta);
		}

		private IEnumerator CalculateVelocity ()
		{
			while( Application.isPlaying )
			{
				// Position at frame start
				// Wait till it the end of the frame
				yield return new WaitForEndOfFrame();
				// Calculate velocity: Velocity = DeltaPosition / DeltaTime
				Velocity = (prevPos - _Transform.position) / Time.fixedDeltaTime;
				prevPos = _Transform.position;
			}
		}
	}
}
