using UnityEngine;

namespace SoulEngine.User_Interface
{
	public class MainMenuUIController : MonoBehaviour
	{
		public void DisplayMenu (GameObject menu)
		{
			this.gameObject.SetActive (false);
			menu.SetActive (true);
		}

		public void EndGame ()
		{
			Application.Quit ();
		}
	}
}
