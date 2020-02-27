namespace SoulEngine
{
	public interface IProjectile : IDamage, IPoolable
	{
		/// <summary>The speed at which the projectile travels.</summary>
		float Speed { get; }
		/// <summary>The lifespan of the projectile before being culled.</summary>
		float LifeTime { get; }

		/// <summary>Culls the object allowing it to be re-pooled.</summary>
		void Cull ();
	}
}
