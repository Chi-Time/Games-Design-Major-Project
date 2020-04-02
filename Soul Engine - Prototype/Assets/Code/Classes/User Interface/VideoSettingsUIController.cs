using System;
using UnityEngine;
using UnityEngine.UI;

namespace SoulEngine.User_Interface
{
	//TODO: Link this into the Settings System so that it saves.
	public class VideoSettingsUIController : MonoBehaviour
	{
		private Text _TargetFrameRateLabel = null;
		private Dropdown _ShadowResolutionDropdown = null;
		private Dropdown _TextureQualityDropdown = null;
		private Dropdown _AAQualityDropdown = null;
		private Dropdown _VSyncDropdown = null;
		
		private void Awake ()
		{
			UpdateUserInterface ();
		}

		private void UpdateUserInterface ()
		{
			_TargetFrameRateLabel.text = Application.targetFrameRate.ToString ();
			_ShadowResolutionDropdown.value = (int) QualitySettings.shadowResolution;
			_TextureQualityDropdown.value = QualitySettings.masterTextureLimit;
			_AAQualityDropdown.value = QualitySettings.antiAliasing;
			_VSyncDropdown.value = QualitySettings.vSyncCount;
		}

		public void ChangeFrameRateTarget (int targetFrameRate)
		{
			Application.targetFrameRate = targetFrameRate;
			_TargetFrameRateLabel.text = targetFrameRate.ToString ();
		}
		
		public void ChangeShadowResolution (ShadowResolution resolution)
		{
			QualitySettings.shadowResolution = resolution;
			_ShadowResolutionDropdown.value = (int) resolution;
		}

		public void ChangeTextureQuality (TextureQualities textureQuality)
		{
			QualitySettings.masterTextureLimit = (int) textureQuality;
			_TextureQualityDropdown.value = (int) textureQuality;
		}
		
		public void ChangeAAQuality (AAQualities qualityLevel)
		{
			QualitySettings.antiAliasing = (int) qualityLevel;
			_AAQualityDropdown.value = (int) qualityLevel;
		}
		
		public void ChangeVSync (VSyncSettings vsyncSetting)
		{
			QualitySettings.vSyncCount = (int) vsyncSetting;
			_VSyncDropdown.value = (int) vsyncSetting;
		}

		public void SetDefaults ()
		{
			Debug.Log ("Implement Set Defaults Logic");
		}
		
		public void DisplayMenu (GameObject menu)
		{
			this.gameObject.SetActive (false);
			menu.SetActive (true);
		}
	}
}
