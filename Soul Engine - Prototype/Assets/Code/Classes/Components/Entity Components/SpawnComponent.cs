using UnityEngine;

namespace SoulEngine
{
	public class SpawnComponent : MonoBehaviour
	{
		[Tooltip ("The offset from the player that this path will spawn at."), SerializeField]
		private float _SpawnOffset = 5.0f;

		private Camera _Camera = null;
		private Transform _Mover = null;
		private Transform _Transform = null;
		private MonoBehaviour[] _Components = null;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
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

		private void Start ()
		{
			_Mover = FindObjectOfType<MoverComponent> ().transform;
		}

		private void Update ()
		{
			if (_Camera.transform.position.y + _SpawnOffset >= transform.position.y)
			{
				_Transform.SetParent (_Mover);
                ActivateComponents(true);
                this.enabled = false;
            }
		}
	}
}
