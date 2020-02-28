namespace SoulEngine
{
	public interface IDamage
	{
		//TODO: Add a difficulty enum later for additional handling of difficulty curve logic.
		/// <summary>The damage this projectile inflicts.</summary>
		int Damage { get; }
	}
}
