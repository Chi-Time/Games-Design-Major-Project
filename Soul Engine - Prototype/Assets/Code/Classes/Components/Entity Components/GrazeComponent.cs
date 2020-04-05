using Code.Classes.Utilities;
using UnityEngine;
using Utilities;

namespace SoulEngine
{
	[RequireComponent (typeof (OutlineComponent))]
	public class GrazeComponent : MonoBehaviour
	{
		[Header ("Graze Settings")]
		[Tooltip ("The score to award the player whilst grazing."), SerializeField]
		private int _Score = 0;
		[Tooltip ("The interval for each amount of score to be awarded."), SerializeField]
		private float _ScoreInterval = 0.0f;
		[Tooltip ("The radius around the enemy for when to award score."), SerializeField]
		private float _GrazeRadius = 2f;

		/// <summary>Detection circle around this object.</summary>
		private Circle _GrazeCircle;
		/// <summary>Reference to the player position.</summary>
		private Transform _Target = null;
		/// <summary>Reference to this object's transform component.</summary>
		private Transform _Transform = null;
		/// <summary>Reference to the outline component on this object.</summary>
		private OutlineComponent _Outline = null;
		/// <summary>Regulator for awarding score at intervals.</summary>
		private Regulator _ScoringRegulator = null;

		private void Awake ()
		{
			_Transform = GetComponent <Transform> ();
			_ScoringRegulator = new Regulator (_ScoreInterval);
			_GrazeCircle = new Circle (_GrazeRadius, _Transform);
		}

		private void Start ()
		{
			SetupOutline ();
			_Target = FindObjectOfType<PlayerController> ().transform;	
		}
		
		private void SetupOutline ()
		{
			_Outline = GetComponent<OutlineComponent> ();
			_Outline.Resize (_GrazeRadius);
		}

		private void Update ()
		{
			CheckIntersections ();
		}

		private void CheckIntersections ()
		{
			// Are we intersecting with the target?
			if (_GrazeCircle.IsIntersecting (_Target))
			{
				// Show the graze outline.
				_Outline.Show (true);
				UpdateScore ();
			}
			else
			{
				// Hide the graze outline.
				_Outline.Show (false);
			}
		}

		private void UpdateScore ()
		{
			// If we can increase the score, then do so.
			if (_ScoringRegulator.HasElapsed (true))
				LevelSignals.OnScoreIncreased?.Invoke (_Score);
		}
	}
}
