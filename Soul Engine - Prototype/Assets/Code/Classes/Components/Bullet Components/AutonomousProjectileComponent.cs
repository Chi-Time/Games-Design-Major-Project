using UnityEngine;

namespace SoulEngine
{
	public class AutonomousProjectileComponent : BulletComponent
	{
		[Tooltip ("How often should the AI update it's logic?"), SerializeField]
		private float _TickInterval = 0.0f;
		
		private StateMachine<AutonomousProjectileComponent> _Brain = null;

		protected override void Awake ()
		{
			base.Awake ();
			
			//TODO: Make different AI components for bullet behaviours such as zig zag component for zig zag behaviour, spiral component etc.
			_Brain = new StateMachine<AutonomousProjectileComponent> (this, _TickInterval);
			_Brain.ChangeGlobalState (null);
			_Brain.ChangeState (null);
		}

		private void Update ()
		{
			_Brain.Run ();
		}

		protected override void EnteredCollider (Collider2D other)
		{
			LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		protected override void ExitedCollider (Collider2D other)
		{
		}
	}
}
