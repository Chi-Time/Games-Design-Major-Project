using Boo.Lang;

namespace SoulEngine.Level_States
{
	public class LevelStatePlay : IState<LevelController>
	{
		private List<ITickable> _Updateables = new List<ITickable> ();
		
		public void Enter (LevelController agent)
		{
		}

		public void Execute (LevelController agent)
		{
		}

		public void PhysicsExecute (LevelController agent)
		{
		}

		public void Exit (LevelController agent)
		{
		}
	}
}
