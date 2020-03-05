using UnityEngine;

namespace SoulEngine
{
	public class SpawnComponent : MonoBehaviour
	{
		private Camera _Camera = null;
		private MonoBehaviour[] _Components = null;
		
		private void Start ()
		{
			_Components = GetComponents<MonoBehaviour> ();
			_Camera = FindObjectOfType<Camera> ();
			ActivateComponents (false);
		}

		private void ActivateComponents (bool shouldActivate)
		{
			if (_Components == null)
				return;
			
			foreach (var component in _Components)
			{
				if (Equals (component, this) == false)
					component.enabled = shouldActivate;
			}
		}

		private void Update ()
		{
			print (_Camera.transform.position.y + 20f + " | " + transform.position.y);
			
			if (_Camera.transform.position.y + 15f >= transform.position.y)
				ActivateComponents (true);
			// print (_Camera.ViewportToWorldPoint (new Vector3 (_Camera.pixelWidth / 2f, _Camera.pixelHeight, 20.0f)));
		}
	}
}
