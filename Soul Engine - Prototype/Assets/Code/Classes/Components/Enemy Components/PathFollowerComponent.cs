using System.Collections;
using Pixelplacement;
using UnityEngine;
using Utilities;

namespace SoulEngine
{
	public class PathFollowerComponent : MonoBehaviour
	{
		public Spline Path { get; set; }

		[Tooltip ("How long (in seconds) should this object take to complete their path?"), SerializeField]
		private float _Speed = 3f;
		[Tooltip ("Should the follower face toward the path?"), SerializeField]
		private bool _ShouldFacePath = false;
		[Tooltip ("Should the follower disable at the end of it's path?"), SerializeField]
		private bool _ShouldDisableWhenDone = true;
		[Tooltip ("The animation type for the follower's movement."), SerializeField]
		private LerpCurves.LerpType _CurveType = LerpCurves.LerpType.SmoothStep;
		
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

			StartCoroutine (MoveTo (1f, _Speed));
		}

		private IEnumerator MoveTo (float endValue, float length)
		{
			float time = 0.0f;
			float startValue = 0.0f;

			while (time <= length)
			{
				time += Time.fixedDeltaTime;
				float t = LerpCurves.Curve (time / length, _CurveType);
				OnValueUpdated (Mathf.Lerp (startValue, endValue, t));

				yield return new WaitForFixedUpdate ();  
			}

			OnValueUpdated (endValue);
			OnPathComplete ();
		}

		private void OnValueUpdated(float value)
		{
			_Rigidbody2D.MovePosition (Path.GetPosition (value));
			
			if (_ShouldFacePath)
				LookAhead (value);
		}

		private void OnPathComplete ()
		{
			if (_ShouldDisableWhenDone)
				gameObject.SetActive (false);
		}
		
		private void OnDisable ()
		{
			LookAhead (0.0f);
			StopAllCoroutines ();
			
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
