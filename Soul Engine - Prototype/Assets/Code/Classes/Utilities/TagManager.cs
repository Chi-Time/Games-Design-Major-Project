using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
	public static class TagManager
	{
		/// <summary>Table to convert enum tags to their string equivalent.</summary>
		public static Dictionary<EditorTags, string> TagsByEnum { get; private set; }
		/// <summary>Table to convert string tags to their enum equivalent.</summary>
		public static Dictionary<string, EditorTags> TagsByString { get; private set; }

		static TagManager ()
		{
			TagsByEnum = new Dictionary<EditorTags, string> ();
			TagsByString = new Dictionary<string, EditorTags> ();
			var values = Enum.GetValues (typeof(EditorTags));

			foreach (var value in values)
			{
				TagsByEnum.Add ((EditorTags)value, Enum.GetName (typeof(EditorTags), value));
				TagsByString.Add (Enum.GetName (typeof(EditorTags), value), (EditorTags)value);
			}
		}
	}
}
