using System;
using UnityEngine;

namespace SoulEngine
{
	public class TagComponent : MonoBehaviour
	{
		/// <summary>The cached tag of this gameobject.</summary>
		public EditorTags Tag { get; private set; }

		private void Awake ()
		{
			Tag = TagManager.TagsByString[gameObject.tag];
		}
	}
}
