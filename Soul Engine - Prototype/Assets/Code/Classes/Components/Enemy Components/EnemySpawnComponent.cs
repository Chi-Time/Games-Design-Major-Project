using Pixelplacement;
using UnityEngine;

namespace SoulEngine
{
	public class EnemySpawnComponent : MonoBehaviour
	{
		[Tooltip ("The offset from the player that this path will spawn at."), SerializeField]
		private float _SpawnOffset = 5.0f;

		private bool _HasSpawned = false;
		private Transform _Camera = null;
		private Transform _Transform = null;
		private MonoBehaviour[] _Components = null;
		
		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Camera = FindObjectOfType<Camera> ().GetComponent<Transform> ();
			_Components = GetComponents<MonoBehaviour> ();
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
			if (_Camera.position.y + _SpawnOffset >= _Transform.position.y && false == _HasSpawned)
			{
				ActivateComponents(true);
				_HasSpawned = true;
			}
			
			if (_Camera.position.y - _SpawnOffset >= _Transform.position.y && _HasSpawned == true)
			{
				this.gameObject.SetActive (false);
			}
		}

		private void OnDisable ()
		{
			_HasSpawned = false;
		}
	}
}
