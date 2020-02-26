using UnityEngine;

namespace SoulEngine
{
	public class BubbleProjectileComponent : BulletComponent
	{
		[Header ("Bubble Settings")]
		[Space (2)]
		[Tooltip ("How much should the affected entity be slowed?"), SerializeField]
		private float _Slowdown = 0.0f;

		private void Update ()
		{
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + (Vector2)_Transform.up * (_Speed * Time.deltaTime));
		}

		protected override void EnteredCollider (Collider2D other)
		{
			LevelSignals.OnEntityBubbled?.Invoke (true, _Slowdown);
		}

		protected override void ExitedCollider (Collider2D other)
		{
			LevelSignals.OnEntityBubbled?.Invoke (false, _Slowdown);
		}
	}
}
