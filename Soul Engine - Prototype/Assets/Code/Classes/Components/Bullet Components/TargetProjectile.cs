using UnityEngine;

namespace SoulEngine
{
	public class TargetProjectile : BulletComponent
	{
		private PlayerController _Player;
		private Transform _Target = null;
		private InputMoveComponent _Input;
		private MoverComponent _World;
		private Vector3 _AdjustedPosition = Vector3.zero;
		private Vector3 _TimeAdjustedPosition = Vector3.zero;

		protected override void OnEnable ()
		{
			base.OnEnable ();
			
			_Player = FindObjectOfType<PlayerController> ();
			_Target = _Player.gameObject.GetComponent<Transform> ();
			_Input = _Player.gameObject.GetComponent<InputMoveComponent> ();
			_World = FindObjectOfType<MoverComponent> ();
			// _Rigidbody2D.isKinematic = false;
			// _Rigidbody2D.gravityScale = 0.0f;
			// _Rigidbody2D.freezeRotation = true;
			
			_Transform.rotation = Quaternion.identity;
			SelectTarget ();

			//_Rigidbody2D.AddForce (_Transform.up * _Speed, ForceMode2D.Force);
		}

		private void SelectTarget ()
		{
			if (_Target == null)
			{
				return;
			}
			
			var worldVelocity = _World.Direction * ( _World.Speed );
			var newPosition = _Target.position + worldVelocity;
			var time = Vector3.Distance (_Transform.position, newPosition) / _Speed;
			
			_AdjustedPosition = _Target.position + worldVelocity * time;
			
			_Transform.up = _AdjustedPosition - _Transform.position;
		}
		
		private void OnDrawGizmos ()
		{
			//Gizmos.DrawLine (_Transform.position, _AdjustedPosition);
		}

		private void FixedUpdate ()
		{
			Vector2 velocity = _Transform.up * ( _Speed * Time.deltaTime );
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + velocity);
		}
	
		protected override void EnteredCollider (Collider2D other)
		{
			Cull ();
			LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		protected override void ExitedCollider (Collider2D other)
		{
		}
	}
}
