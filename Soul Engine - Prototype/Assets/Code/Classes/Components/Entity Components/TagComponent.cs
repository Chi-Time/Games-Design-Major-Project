using System;
using UnityEngine;

namespace SoulEngine
{
	public class TagComponent : MonoBehaviour
	{
		/// <summary>The cached tag of this gameobject.</summary>
		public EditorTags Tag => _Tag;
		/// <summary>The various tags this component is looking for.</summary>
		public EditorTags[] Tags 
		{
			get => _Tags;
			set => _Tags = value;
		}

		//TODO: Profile how long it takes to loop through single value tag arrays when doing a HasTags check as we use single tags most of the time and don't need the overhead of a loop if it exists.
		[Tooltip ("The various tags to look for."), SerializeField]
		private EditorTags[] _Tags = null;

		private EditorTags _Tag;

		protected virtual void Awake ()
		{
			_Tag = TagManager.TagsByString[gameObject.tag];
		}
	}
}
