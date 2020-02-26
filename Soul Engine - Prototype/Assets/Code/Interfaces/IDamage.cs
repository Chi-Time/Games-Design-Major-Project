namespace SoulEngine
{
	public interface IDamage : ITaggable
	{
		//TODO: Add a difficulty enum later for additional handling of difficulty curve logic.
		int Damage { get; }
	}
}
