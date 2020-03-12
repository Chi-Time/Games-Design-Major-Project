using UnityEngine;

namespace SoulEngine
{
	public class FaceTargetComponent : MonoBehaviour
	{	
		[Tooltip ("Should the object look at the player by default?"), SerializeField]
		private bool _UsePlayer = true;
		[Tooltip ("The target that we should turn to face."), SerializeField]

		private MoverComponent _World = null;
		private Transform _Target = null;
		private Transform _Transform = null;
		private InputMoveComponent _InputMoveCompoenent = null;
		private Vector3 _AdjustedPosition = Vector3.zero;

		private void Awake ()
		{
			FindTarget ();
			_Transform = GetComponent<Transform> ();
			_InputMoveCompoenent = FindObjectOfType<InputMoveComponent> ();
		}

		private void Start ()
		{
			_World = FindObjectOfType<MoverComponent> ();
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

			//var position = _Target.position + ( _World.Direction * ( _World.Speed ) );
			
			_Transform.up = _Target.position - _Transform.position;
		}

		// private void OnDrawGizmos ()
		// {
		// 	_AdjustedPosition = _Target.position + ( _World.Direction * ( _World.Speed ) );
		// 	Gizmos.DrawLine (_Transform.position, _AdjustedPosition);
		// }
	}
}
