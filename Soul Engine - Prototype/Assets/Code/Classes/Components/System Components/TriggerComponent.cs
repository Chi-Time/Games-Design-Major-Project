using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Collider2D), typeof (TagComponent))]
	public abstract class TriggerComponent : MonoBehaviour
	{
		protected TagComponent _TagComponent = null;
		
		private void Awake ()
		{
			_TagComponent = GetComponent<TagComponent> ();
			GetComponent<Collider2D> ().isTrigger = true;
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			Triggered (other);
		}

		protected abstract void Triggered (Collider2D other);
	}
}
