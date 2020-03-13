using UnityEngine;

namespace SoulEngine
{
	public class RotaterComponent : MonoBehaviour
	{
		[Tooltip ("The speed at which the object rotates in the world."), SerializeField]
		private float _Speed = 0.0f;
		[Tooltip (""), SerializeField]
		private bool _ShouldStep = false;
		[Tooltip (""), SerializeField]
		private float _StepTimes = 0.0f;
		[Tooltip (""), SerializeField]
		private float _StepAngle = 0.0f;

		private float _Counter = 0.0f;
		private Transform _Transform = null;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
		}

		private void Update ()
		{
			if (_ShouldStep)
			{
				Step ();
			}
			else
			{
				Rotate ();
			}
		}
		
		//TODO: Add in lerp so that we slowly move to the next position.
		private void Step ()
		{
			_Counter += Time.deltaTime;

			if (_Counter >= _StepTimes)
			{
				_Counter = 0.0f;
				var rotation = new Vector3 (0.0f, 0.0f, _Transform.rotation.eulerAngles.z + _StepAngle);
				_Transform.rotation = Quaternion.Euler (rotation);
			}
		}

		private void Rotate ()
		{
			var speed = _Speed * Time.deltaTime;
			var rotation = new Vector3 (0.0f, 0.0f, _Transform.rotation.eulerAngles.z + speed);
			_Transform.rotation = Quaternion.Euler (rotation);
		}
	}
}
