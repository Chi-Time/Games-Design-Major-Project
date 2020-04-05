using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Code.Classes.Utilities;

namespace SoulEngine
{
	public class ProfilerComponent : MonoBehaviour
	{
		[SerializeField]
		private int _Count = 5;
		[SerializeField]
		private int _AverageCount = 0;
		[SerializeField]
		private GameObject _Other = null;
		
		private Transform _NotNullObject = null;
		private Transform _NullObject = null;
		
		private void Awake ()
		{
			//CirclePointTestComparison ();
			//NullComparisons ();
			//Comparison ();
			//StringAndEnumComparison ();
			Debug.Break ();
		}

		private void CirclePointTestComparison ()
		{
			var stopwatch = new Stopwatch ();
			
			var radius = 2.5f;
			var sqrRadius = radius * radius;
			Vector2 startPosition = new Vector2 (0, 0);
			Vector2[] positions = new Vector2[_Count];
		
			for (int i = 0; i < positions.Length; i++)
			{
				positions[i] = new Vector2 (Random.Range (-25f, 25f), Random.Range (-25f, 25f));
			}
			
			long unityComparison = 0;
			long standardComparison = 0;
			long optimisedComparison = 0;
			long dummyComparison = 0;
				
			stopwatch.Start ();
				
			for (int i = 0; i < _Count; i++)
			{
				if (Vector2.Distance (startPosition, positions[i]) < radius)
					continue;
			}
			
			stopwatch.Stop ();
			unityComparison = stopwatch.ElapsedTicks;
			
			stopwatch.Reset ();
			
			stopwatch.Start ();
				
			for (int i = 0; i < _Count; i++)
			{
				if (Mathy.SqrDistance (startPosition, positions[i]) < sqrRadius)
				{
					continue;
				}
			}
				
			stopwatch.Stop ();
			standardComparison = stopwatch.ElapsedTicks;
			
			stopwatch.Reset ();
			
			stopwatch.Start ();
				
			for (int i = 0; i < _Count; i++)
			{
				if (Mathy.IsInRadiusOptimised (startPosition, positions[i], radius, sqrRadius))
				{
					continue;
				}
			}
				
			stopwatch.Stop ();
			optimisedComparison = stopwatch.ElapsedTicks;
		
			print ("Unity: " + unityComparison + " : " + "Standard: " + standardComparison + " : " + "Optimised: " + optimisedComparison);
		}
		
		// private void NullComparisons ()
		// {
		// 	var stopwatch = new Stopwatch ();
		// 	long opComparison = 0;
		// 	long idComparison = 0;
		// 	
		// 	stopwatch.Start ();
		// 	
		// 	for (int i = 0; i < _Count; i++)
		// 	{
		// 		if (_NullObject)
		// 			continue;
		// 	}
		//
		// 	stopwatch.Stop ();
		// 	opComparison = stopwatch.ElapsedTicks;
		//
		// 	stopwatch.Reset ();
		//
		// 	stopwatch.Start ();
		// 	
		// 	for (int i = 0; i < _Count; i++)
		// 	{
		// 		if (_NullObject is null)
		// 		{
		// 			continue;
		// 		}
		// 	}
		// 	
		// 	stopwatch.Stop ();
		// 	idComparison = stopwatch.ElapsedTicks;
		//
		// 	print ("Unity Null: " + opComparison + " : " + "Object Null: " + idComparison);
		// }
		//
		// private void Comparison ()
		// {
		// 	var stopwatch = new Stopwatch ();
		// 	long opComparison = 0;
		// 	long idComparison = 0;
		//
		// 	stopwatch.Start ();
		// 	
		// 	for (int i = 0; i < _Count; i++)
		// 	{
		// 		if (_Other == gameObject)
		// 			continue;
		// 	}
		//
		// 	stopwatch.Stop ();
		// 	opComparison = stopwatch.ElapsedTicks;
		//
		// 	stopwatch.Reset ();
		//
		// 	stopwatch.Start ();
		// 	
		// 	for (int i = 0; i < _Count; i++)
		// 	{
		// 		if (Equals (gameObject, _Other))
		// 			continue;
		// 	}
		// 	
		// 	stopwatch.Stop ();
		// 	idComparison = stopwatch.ElapsedTicks;
		//
		// 	print ("OPERATOR: " + opComparison + " : " + "ID: " + idComparison);
		// }
		//
		// private void StringAndEnumComparison ()
		// {
		// 	string strTag = "Enemy";
		// 	EditorTags tEditorTag = EditorTags.Enemy;
		// 	var stopwatch = new Stopwatch ();
		// 	int sims = 1;
		// 	long[] stringComparisons = new long[sims];
		// 	long[] enumComparisons = new long[sims];
		//
		// 	var myTag = TagManager.TagsByString[gameObject.tag];
		//
		// 	for (int j = 0; j < sims; j++)
		// 	{
		// 		stopwatch.Reset ();
		// 		//Debug.Log ("=----------=");
		// 		//Debug.Log ("STRING COMPARISON");
		// 		stopwatch.Start ();
		// 		for (int i = 0; i < _Count; i++)
		// 		{
		// 			if (this.HasTags (new string[] {"Magnet", "Player"}))
		// 				continue;
		// 		}
		// 		stopwatch.Stop ();
		// 		stringComparisons[j] = stopwatch.ElapsedTicks;
		// 		//Debug.Log ("TIME: " + stopwatch.ElapsedTicks.ToString ());
		// 		stopwatch.Reset ();
		// 		//Debug.Log ("ENUM COMPARISON");
		// 		stopwatch.Start ();
		// 		for (int i = 0; i < _Count; i++)
		// 		{
		// 			if (this.HasTags (new EditorTags[] {EditorTags.Magnet, EditorTags.Player}))
		// 				continue;
		// 		}
		// 		stopwatch.Stop ();
		// 		enumComparisons[j] = stopwatch.ElapsedTicks;
		// 		//Debug.Log ("TIME: " + stopwatch.ElapsedTicks.ToString ());
		// 	}
		//
		// 	long total = 0;
		// 	foreach (var val in stringComparisons)
		// 	{
		// 		total += val;
		// 	}
		//
		// 	long stringAverage = total / sims;
		// 	
		// 	total = 0;
		// 	foreach (var val in enumComparisons)
		// 	{
		// 		total += val;
		// 	}
		//
		// 	long enumAverage = total / sims;
		//
		// 	Debug.Log ("STRING: " + stringAverage + " : ENUM: " + enumAverage);
		// }
	}
}
