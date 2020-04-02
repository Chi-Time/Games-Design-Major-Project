using UnityEngine;

namespace SoulEngine.User_Interface
{
	public class CreditsUIController : MonoBehaviour
	{
		public void DisplayMenu (GameObject menu)
		{
			this.gameObject.SetActive (false);
			menu.SetActive (true);
		}
	}
}
