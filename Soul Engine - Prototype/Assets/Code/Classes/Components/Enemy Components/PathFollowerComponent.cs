using Pixelplacement;
using UnityEngine;

namespace SoulEngine
{
	public class PathFollowerComponent : MonoBehaviour
	{
		public Spline Path { get; set; }
		
		[Tooltip ("How long (in seconds) should this object take to complete their path?"), SerializeField]
		private float _Speed;
		
		private Transform _Transform;
		private Rigidbody2D _Rigidbody2D;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.gravityScale = 0.0f;
			_Rigidbody2D.freezeRotation = true;

			GetComponent<Collider2D> ().isTrigger = true;
		}

		private void OnEnable ()
		{
			if (Path == null)
				return;
			
			Tween.Value (0f,
			             1f,
			             OnValueUpdated,
			             6,
			             0,
			             null,
			             Tween.LoopType.PingPong,
			             null,
			             OnPathComplete);
		}
		
		private void OnValueUpdated(float value)
		{
			_Rigidbody2D.MovePosition (Path.GetPosition (value));
			LookAhead (value);
		}

		private void OnPathComplete ()
		{
			gameObject.SetActive (false);
		}
		
		private void OnDisable ()
		{
			LookAhead (0.0f);
			//TODO: Figure out how to stop tweens if the player has killed and disabled the follower early.
			
			if (Path != null)
				_Rigidbody2D.MovePosition (Path.GetPosition (0.0f));
		}
		
		private void LookAhead (float value)
		{
			//Add an offset because we want to look AHEAD of we're currently at to get the correct facing vector.
			float lookOffset = 0.01f;
			value += lookOffset;
			
			if (value > 0.99f || Path == null)
				return; 
			
			_Transform.up = Path.GetPosition(value) - _Transform.position;
		}
	}
}
