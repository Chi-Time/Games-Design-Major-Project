using UnityEngine;

namespace SoulEngine
{
	public class FaceTargetComponent : MonoBehaviour
	{	
		[Tooltip ("Should the object look at the player by default?"), SerializeField]
		private bool _UsePlayer = true;

		private Transform _Target = null;
		private Transform _Transform = null;

		private void Awake ()
		{
			FindTarget ();
			_Transform = GetComponent<Transform> ();
		}

		private void FindTarget ()
		{
			if (_UsePlayer)
			{
				_Target = FindObjectOfType<PlayerController> ()?.transform;
			}
		}

		private void Update ()
		{
			LookAt ();
		}

		private void LookAt ()
		{
			//BUG: Find a way to use a boolean for the null check instead as Unity's own overload is terrible.
			if (_Target == null)
				return;

			_Transform.up = _Target.position - _Transform.position;
		}
	}
}
