using UnityEngine;

namespace SoulEngine
{
	public class PerspectiveSwitchTrigger : TriggerComponent
	{
		protected override void Triggered (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				LevelSignals.OnPerspectiveSwitched?.Invoke ();
				gameObject.SetActive (false);
			}
		}
	}
}
