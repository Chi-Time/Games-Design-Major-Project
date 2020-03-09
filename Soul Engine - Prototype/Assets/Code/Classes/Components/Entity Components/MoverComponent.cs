using UnityEngine;

namespace SoulEngine
{
	public class MoverComponent : MonoBehaviour
	{
		[Tooltip ("How fast does the component move across the world?"), SerializeField]
		private float _Speed = 10.0f;

		private Transform _Transform = null;
		private Rigidbody2D _Rigidbody2D = null;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
		}

		private void FixedUpdate ()
		{
			Move ();
		}

		private void Move ()
		{
			Vector2 delta = _Transform.up * (_Speed * Time.fixedDeltaTime);
			_Transform.Translate (delta);
		}
	}
}
