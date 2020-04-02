using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SoulEngine
{
	public class SettingsSystem : MonoBehaviour
	{
		private Settings _SettingsFile = null;
		private string _SettingsFilePath = @"";

		private void Awake ()
		{
			var json = GetJson ();
			_SettingsFile = Settings.Deserialize (json);
			ApplyLoadedSettings ();
		}

		private string GetJson ()
		{
			using (var streamReader = new StreamReader (_SettingsFilePath, Encoding.UTF8))
			{
				return streamReader.ReadToEnd ();
			}
		}

		public void ApplyLoadedSettings ()
		{
			Application.targetFrameRate = _SettingsFile.TargetFrameRate;
			QualitySettings.antiAliasing = (int)_SettingsFile.AALevel;
			QualitySettings.vSyncCount = (int) _SettingsFile.VSyncSetting;
			QualitySettings.shadowResolution = _SettingsFile.ShadowResolution;
			QualitySettings.masterTextureLimit = (int)_SettingsFile.TextureQuality;
		}

		private void OnDestroy ()
		{
			SaveFile ();
		}
		
		private void SaveFile ()
		{
			var json = _SettingsFile.Serialize ();
			using (var streamWriter = new StreamWriter (_SettingsFilePath, false, Encoding.UTF8))
			{
				streamWriter.Write (json);
			}
		}
	}
	
	[Serializable]
	public class Settings
	{
		public int TargetFrameRate { get; set; }
		public AAQualities AALevel { get; set; }
		public VSyncSettings VSyncSetting { get; set; }
		public ShadowResolution ShadowResolution { get; set; }
		public TextureQualities TextureQuality { get; set; }

		public Settings ()
		{
			TargetFrameRate = 60;
			AALevel = AAQualities.X2;
			VSyncSetting = VSyncSettings.EveryVBlank;
			ShadowResolution = ShadowResolution.Low;
			TextureQuality = TextureQualities.HalfRes;
		}

		public static Settings Deserialize (string json)
		{
			var settings = JsonUtility.FromJson<Settings> (json);

			return settings ?? new Settings ();
		}
		
		public string Serialize ()
		{
			//TODO: Apply exception handling as this could cause crashes.
			var json = JsonUtility.ToJson (this, true);

			return json;
		}
	}
}
