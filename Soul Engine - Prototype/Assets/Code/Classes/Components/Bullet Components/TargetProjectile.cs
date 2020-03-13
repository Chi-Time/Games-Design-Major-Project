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
		private Vector3 _CurrentPosition = Vector3.zero;
		private Vector3 _TargetPosition = Vector3.zero;

		private float _Timer = 0.0f;

		private void Start ()
		{
			_World = FindObjectOfType<MoverComponent> ();
			_Player = FindObjectOfType<PlayerController> ();
			_Target = _Player.gameObject.GetComponent<Transform> ();
			_Input = _Player.gameObject.GetComponent<InputMoveComponent> ();
			
			SelectTarget ();
		}

		protected override void OnEnable ()
		{
			base.OnEnable ();
			SelectTarget ();
		}

		private void SelectTarget ()
		{
			if (_Target == null)
				return;

			_TargetPosition = _Target.position;
			_CurrentPosition = _Transform.position;
			
			var worldVelocity = _World.Direction * _World.Speed;
			var newPosition = _TargetPosition + worldVelocity;
			float time = Vector3.Distance (_CurrentPosition, newPosition) / _Speed;
			
			_AdjustedPosition = _TargetPosition + worldVelocity * time;
			_Transform.up = _AdjustedPosition - _CurrentPosition;
		}
		
		public static Vector3 CalculateInterceptCourse(Vector3 aTargetPos, Vector3 aTargetSpeed, Vector3 aInterceptorPos, float aInterceptorSpeed)
		{
			Vector3 targetDir = aTargetPos - aInterceptorPos;
			float iSpeed2 = aInterceptorSpeed * aInterceptorSpeed;
			float tSpeed2 = aTargetSpeed.sqrMagnitude;
			float fDot1 = Vector3.Dot(targetDir, aTargetSpeed);
			float targetDist2 = targetDir.sqrMagnitude;
			float d = (fDot1 * fDot1) - targetDist2 * (tSpeed2 - iSpeed2);
			if (d < 0.1f) // negative == no possible course because the interceptor isn't fast enough
				return Vector3.zero;
			float sqrt = Mathf.Sqrt(d);
			float S1 = (-fDot1 - sqrt) / targetDist2;
			float S2 = (-fDot1 + sqrt) / targetDist2;
			if (S1 < 0.0001f)
			{
				if (S2 < 0.0001f)
					return Vector3.zero;
				else
					return (S2) * targetDir + aTargetSpeed;
			}
			else if (S2 < 0.0001f)
				return (S1) * targetDir + aTargetSpeed;
			else if (S1 < S2)
				return (S2) * targetDir + aTargetSpeed;
			else
				return (S1) * targetDir + aTargetSpeed;
		}
		
		private void OnDrawGizmos ()
		{
			//Gizmos.DrawLine (_Transform.position, _AdjustedPosition);
		}

		private void FixedUpdate ()
		{
			_Timer += Time.deltaTime;
			
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
