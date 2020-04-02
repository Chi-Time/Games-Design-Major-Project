using UnityEngine;

namespace SoulEngine.User_Interface
{
	public class GameplaySettingsUIController : MonoBehaviour
	{
		public void EnableAutoFire (bool enable)
		{}
		
		public void DisplayMenu (GameObject menu)
		{
			this.gameObject.SetActive (false);
			menu.SetActive (true);
		}
	}
}
