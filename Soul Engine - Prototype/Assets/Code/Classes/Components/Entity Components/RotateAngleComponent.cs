using System.Collections;
using UnityEngine;
using Utilities;

namespace SoulEngine
{
	public class RotateAngleComponent : MonoBehaviour
	{
		[Tooltip ("The speed at which the object rotates to it's next target."), SerializeField]
		private float _Speed = 0.0f;
		[Tooltip ("The range of motion for the object to express."), SerializeField]
		private float _AngleRange = 45.0f;
		
		private void OnEnable ()
		{
			StartCoroutine (RotateTo (_AngleRange, _Speed));
		}

		private IEnumerator RotateTo (float nextAngle, float speed)
		{
			float time = 0.0f;
			Vector3 eulerAngle = transform.rotation.eulerAngles;
			float startAngle = eulerAngle.z;

			while (time <= speed)
			{
				time += Time.deltaTime;
				
				float x = eulerAngle.x;
				float y = eulerAngle.y;
				float z = Mathf.Lerp (startAngle, nextAngle, LerpCurves.Curve (time / speed, LerpCurves.LerpType.EaseOut));
				
				transform.rotation = Quaternion.Euler (new Vector3 (x, y, z));

				yield return new WaitForEndOfFrame ();
			}
			
			transform.rotation = Quaternion.Euler (new Vector3 (eulerAngle.x,
			                                                    eulerAngle.y,
			                                                    nextAngle));
			StopAllCoroutines ();
			StartCoroutine (RotateTo (-_AngleRange, _Speed));
		}
	}
}
