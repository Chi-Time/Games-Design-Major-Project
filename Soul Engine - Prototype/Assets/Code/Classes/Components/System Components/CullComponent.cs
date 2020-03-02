using UnityEngine;

namespace SoulEngine
{
	public class CullComponent : TriggerComponent
	{
		protected override void Triggered (Collider2D other)
		{
			other.gameObject.SetActive (false);
		}
	}
}
