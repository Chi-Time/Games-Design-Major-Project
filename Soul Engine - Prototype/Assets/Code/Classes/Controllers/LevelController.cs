using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulEngine
{
	public class LevelController : MonoBehaviour
	{
		private void Update ()
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
}
