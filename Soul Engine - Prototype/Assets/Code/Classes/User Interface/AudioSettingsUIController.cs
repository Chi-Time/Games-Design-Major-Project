using UnityEngine;

namespace SoulEngine.User_Interface
{
	public class AudioSettingsUIController : MonoBehaviour
	{
		public void ChangeMusicVolume (float volume)
		{
			Debug.Log ("Implement music volume Logic");
		}

		public void ChangeSFXVolume (float volume)
		{
			Debug.Log ("Implement sfx volume Logic");
		}

		public void ChangeUIVolume (float volume)
		{
			Debug.Log ("Implement ui volume Logic");
		}

		public void ChangeAmbientVolume (float volume)
		{
			Debug.Log ("Implement ambient volume Logic");
		}
	
		public void DisplayMenu (GameObject menu)
		{
			this.gameObject.SetActive (false);
			menu.SetActive (true);
		}
	}
}
