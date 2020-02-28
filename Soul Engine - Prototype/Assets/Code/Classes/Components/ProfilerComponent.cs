using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SoulEngine
{
	public class ProfilerComponent : MonoBehaviour
	{
		[SerializeField]
		private int _Count = 5;
		
		private void Awake ()
		{
			StringAndEnumComparison ();
		}

		private void StringAndEnumComparison ()
		{
			string strTag = "Enemy";
			EditorTags tEditorTag = EditorTags.Enemy;
			var stopwatch = new Stopwatch ();
			int sims = 1;
			long[] stringComparisons = new long[sims];
			long[] enumComparisons = new long[sims];

			var myTag = TagManager.TagsByString[gameObject.tag];

			for (int j = 0; j < sims; j++)
			{
				stopwatch.Reset ();
				//Debug.Log ("=----------=");
				//Debug.Log ("STRING COMPARISON");
				stopwatch.Start ();
				for (int i = 0; i < _Count; i++)
				{
					if (this.HasTags (new string[] {"Magnet", "Player"}))
						continue;
				}
				stopwatch.Stop ();
				stringComparisons[j] = stopwatch.ElapsedTicks;
				//Debug.Log ("TIME: " + stopwatch.ElapsedTicks.ToString ());
				stopwatch.Reset ();
				//Debug.Log ("ENUM COMPARISON");
				stopwatch.Start ();
				for (int i = 0; i < _Count; i++)
				{
					if (this.HasTags (new EditorTags[] {EditorTags.Magnet, EditorTags.Player}))
						continue;
				}
				stopwatch.Stop ();
				enumComparisons[j] = stopwatch.ElapsedTicks;
				//Debug.Log ("TIME: " + stopwatch.ElapsedTicks.ToString ());
			}

			long total = 0;
			foreach (var val in stringComparisons)
			{
				total += val;
			}

			long stringAverage = total / sims;
			
			total = 0;
			foreach (var val in enumComparisons)
			{
				total += val;
			}

			long enumAverage = total / sims;

			Debug.Log ("STRING: " + stringAverage + " : ENUM: " + enumAverage);
		}
	}
}
