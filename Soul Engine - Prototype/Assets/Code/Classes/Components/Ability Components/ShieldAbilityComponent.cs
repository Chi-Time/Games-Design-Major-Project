using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Renderer), typeof (Collider2D), typeof (HealthComponent))]
	[RequireComponent (typeof (TagComponent))]
	public class ShieldAbilityComponent : AbilityComponent
	{
		/// <summary>Reference to the component's Renderer component.</summary>
		private Renderer _Renderer = null;
		/// <summary>Reference to the component's Collider2D component.</summary>
		private Collider2D _Collider2D = null;
		/// <summary>Reference to the shields strength.</summary>
		private HealthComponent _HealthComponent = null;

		private void Awake ()
		{
			CacheComponents ();
			SetEnabled (false);
			_Collider2D.isTrigger = true;
		}

		private void CacheComponents ()
		{
			_Renderer = GetComponent<Renderer> ();
			_Collider2D = GetComponent<Collider2D> ();
			_HealthComponent = GetComponent<HealthComponent> ();
		}

		/// <summary>Activate the shield on the player.</summary>
		protected override void Activate ()
		{
			SetEnabled (true);
		}

		/// <summary>Deactivate the shield on the player.</summary>
		protected override void Deactivate ()
		{
			SetEnabled (false);
			_IsInUse = false;
		}
		
		/// <summary>Enable or Disable the shield on the player.</summary>
		/// <param name="enabled"></param>
		private void SetEnabled (bool enabled)
		{
			_Renderer.enabled = enabled;
			_Collider2D.enabled = enabled;
		}

		private void OnEnable ()
		{
			LevelSignals.OnEntityHit += OnEntityHit;			
		}

		private void OnDisable ()
		{
			LevelSignals.OnEntityHit -= OnEntityHit;
		}

		private void OnEntityHit (IDamage damage, GameObject other)
		{
			if (Equals (this.gameObject, other))
			{
				_HealthComponent.TakeDamage (damage.Damage);
			}
		}
	}
}
