﻿using UnityEngine;

namespace SoulEngine
{
	public class FaceTargetComponent : MonoBehaviour
	{	
		[Tooltip ("Should the object look at the player by default?"), SerializeField]
		private bool _UsePlayer = true;
		[Tooltip ("The target that we should turn to face."), SerializeField]
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
				_Target = FindObjectOfType<PlayerController> ().transform;
			}
		}

		private void Update ()
		{
			LookAt ();
		}

		private void LookAt ()
		{
			_Transform.up = _Target.position - _Transform.position;
		}
	}
}