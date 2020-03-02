using Pixelplacement;
using UnityEngine;

namespace SoulEngine
{
	public class CameraController : MonoBehaviour
	{
		[Tooltip ("How long should the perspective change animation take?"), SerializeField]
		private float _PerspectiveSwitchLength = 1.0f;

		private float _ZoomedFOV = 55f;
		private float _CurrentFOV = 0.0f;
		private Camera _Camera = null;
		private Transform _Transform = null;

		private void Awake ()
		{
			_Camera = GetComponent<Camera> ();
			_Transform = GetComponent<Transform> ();

			_CurrentFOV = _Camera.fieldOfView;
		}

		private void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Q))
				LevelSignals.OnPerspectiveSwitched?.Invoke ();
		}

		private void OnEnable ()
		{
			LevelSignals.OnPerspectiveSwitched += OnPerspectiveSwitched;
		}

		private void OnPerspectiveSwitched ()
		{
			if (IsHorizontal ())
			{
				StartRotate (-449f);
			}
			
			if (IsVertical ())
			{
				StartRotate (449f);
			}
			
			StartZoomTween (_ZoomedFOV);
		}

		//TODO: Find a better way to do the zoom in and out, there must be a way to use ping pong properly.
		private void StartZoomTween (float fov)
		{
			Tween.Value (_Camera.fieldOfView,
			             fov,
			             ZoomValueUpdated,
			             _PerspectiveSwitchLength * 0.5f,
			             0.0f,
			             Tween.EaseInOut,
			             Tween.LoopType.None,
			             null,
			             OnZoomComplete,
			             false);
		}

		private void ZoomValueUpdated (float value)
		{
			_Camera.fieldOfView = value;
		}

		private void OnZoomComplete ()
		{
			if (_Camera.fieldOfView < _CurrentFOV)
				StartZoomTween (_CurrentFOV);
			else
				_Camera.fieldOfView = _CurrentFOV;
		}

		private bool IsHorizontal ()
		{
			if (_Transform.rotation.eulerAngles.z >= 46f)
				return true;

			return false;
		}

		private bool IsVertical ()
		{
			if (_Transform.rotation.eulerAngles.z <= 45f)
				return true;

			return false;
		}
		
		private void StartRotate (float angle)
		{
			Tween.Rotate (_Transform,
			              new Vector3 (0, 0, angle),
			              Space.Self,
			              _PerspectiveSwitchLength,
			              0,
			              Tween.EaseInOut,
			              Tween.LoopType.None,
			              null,
			              OnRotateComplete,
			              obeyTimescale: false);
		}

		private void OnRotateComplete ()
		{
			var currentRotation = _Transform.rotation.eulerAngles;

			if (IsHorizontal ())
			{
				_Transform.rotation = Quaternion.Euler (new Vector3 (currentRotation.x,currentRotation.x, 90f));
			}

			if (IsVertical ())
			{
				_Transform.rotation = Quaternion.Euler (new Vector3 (currentRotation.x,currentRotation.x, 0.0f));
			}
		}

		private void OnDisable ()
		{
			LevelSignals.OnPerspectiveSwitched -= OnPerspectiveSwitched;
		}
	}
}
