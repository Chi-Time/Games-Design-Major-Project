using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Collider2D))]
	public class CullComponent : MonoBehaviour
	{
		private void Awake ()
		{
			GetComponent<Collider2D> ().isTrigger = true;
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			other.gameObject.SetActive (false);
		}
	}
}
