using System.Collections;
using Pixelplacement;
using UnityEngine;
using Utilities;

namespace SoulEngine
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class PathFollowerComponent : MonoBehaviour
	{
		public float Speed => _Speed;
		public Spline Path => _Path;
		public LerpCurves.LerpType Curve => _Curve;
		
		[Tooltip ("Should the follower face toward the path?"), SerializeField]
		private bool _ShouldFacePath = false;
		[Tooltip ("Should the follower disable at the end of it's path?"), SerializeField]
		private bool _ShouldDisableWhenDone = true;
		
		private float _Speed = 0.0f;
		private Spline _Path = null;
		private LerpCurves.LerpType _Curve = LerpCurves.LerpType.Linear;
		private Transform _Transform;
		private Rigidbody2D _Rigidbody2D;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.gravityScale = 0.0f;
			_Rigidbody2D.freezeRotation = true;
		}

		public void Setup (float speed, Spline path, LerpCurves.LerpType curve)
		{
			_Speed = speed;
			_Path = path;
			_Curve = curve;
		}

		private void OnEnable ()
		{
			if (Path == null)
				return;

			StartCoroutine (MoveTo (1f, Speed));
		}

		private IEnumerator MoveTo (float endValue, float length)
		{
			float time = 0.0f;
			float startValue = 0.0f;

			while (time <= length)
			{
				time += Time.fixedDeltaTime;
				float t = LerpCurves.Curve (time / length, Curve);
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
			
			//BUG: Comparison to null, find a way to remove it.
			if (value > 0.99f || Path == null)
				return;
			
			_Transform.up = Path.GetPosition(value) - _Transform.position;
		}
	}
}
