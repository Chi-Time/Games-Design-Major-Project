using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Collider2D))]
	public abstract class TriggerComponent : MonoBehaviour
	{
		[SerializeField]
		protected TagController _TagController = new TagController ();

		private void Awake ()
		{
			_TagController.Construct (this);
			GetComponent<Collider2D> ().isTrigger = true;
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			Triggered (other);
		}

		protected abstract void Triggered (Collider2D other);
	}
}
