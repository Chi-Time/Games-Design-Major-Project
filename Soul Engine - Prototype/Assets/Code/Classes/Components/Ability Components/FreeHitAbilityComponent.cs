using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Renderer), typeof (Collider2D), typeof (TagComponent))]
	public class FreeHitAbilityComponent : AbilityComponent
	{
		/// <summary>Reference to the component's Renderer component.</summary>
		private Renderer _Renderer = null;
		/// <summary>Reference to the component's Collider2D component.</summary>
		private Collider2D _Collider2D = null;

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
		
		/// <summary>Enables/Disables the shield depending on the given value.</summary>
		/// <param name="enabled"></param>
		private void SetEnabled (bool enabled)
		{
			_Renderer.enabled = enabled;
			_Collider2D.enabled = enabled;
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			// Has the shield been hit by a bullet or explosion?
			if (other.GetComponent<BulletComponent> () != null || other.GetComponent<ExplosionComponent> () != null)
			{
				// Deactivate the shield and the aggresor.
				Deactivate ();
				other.gameObject.SetActive (false);
			}
		}
	}
}
