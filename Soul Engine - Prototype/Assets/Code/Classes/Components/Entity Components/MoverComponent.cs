using UnityEngine;

namespace SoulEngine
{
	//[RequireComponent (typeof (Rigidbody2D))]
	public class MoverComponent : MonoBehaviour
	{
		[Tooltip ("How fast does the component move across the world?"), SerializeField]
		private float _Speed = 10.0f;

		private Transform _Transform = null;
		private Rigidbody2D _Rigidbody2D = null;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			// _Rigidbody2D = GetComponent<Rigidbody2D> ();
			// _Rigidbody2D.isKinematic = true;
			// _Rigidbody2D.gravityScale = 0.0f;
			// _Rigidbody2D.freezeRotation = true;
		}

		private void FixedUpdate ()
		{
			Move ();
		}

		private void Move ()
		{
			Vector2 delta = _Transform.up * _Speed * Time.fixedDeltaTime;
			//_Rigidbody2D.MovePosition (_Rigidbody2D.position + delta);	
			_Transform.Translate (delta);
		}
	}
}
