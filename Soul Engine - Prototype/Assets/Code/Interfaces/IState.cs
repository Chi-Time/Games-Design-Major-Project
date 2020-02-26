namespace SoulEngine
{
	public interface IState<T> where T : class
	{
		void Enter (T agent);
		void Execute (T agent);
		void Exit (T agent);
	}
}
