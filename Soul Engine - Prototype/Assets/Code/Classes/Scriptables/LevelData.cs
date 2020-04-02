using UnityEngine;
using System.Collections.Generic;

namespace SoulEngine
{
	[CreateAssetMenu (fileName = "Level Data", menuName = "Level Data", order = 0)]
	public class LevelData : ScriptableObject
	{
		public string LevelName = "Level-Name-Here";
		public TextAsset Description = null;

		public Difficulties SelectedDifficultyLevel { get; set; } = Difficulties.Easy;
		public Dictionary<Difficulties, int> DifficultyScores { get; set; } = new Dictionary<Difficulties, int> ();
		public Dictionary<Difficulties, Time> DifficultyTimes { get; set; } = new Dictionary<Difficulties, Time> ();
		public Dictionary<Difficulties, bool> DifficultiesCompleted { get; set; } = new Dictionary<Difficulties, bool> ();
		public Dictionary<Challenges, bool> ChallengesCompleted { get; set; } = new Dictionary<Challenges, bool> ();
	}
}
