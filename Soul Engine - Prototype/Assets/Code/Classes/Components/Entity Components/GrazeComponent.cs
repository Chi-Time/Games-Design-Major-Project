using Code.Classes.Utilities;
using UnityEngine;
using Utilities;

namespace SoulEngine
{
	public class GrazeComponent : MonoBehaviour
	{
		[Header ("Graze Settings")]
		[Tooltip ("The score to award the player whilst grazing."), SerializeField]
		private int _Score = 0;
		[Tooltip ("The interval for each amount of score to be awarded."), SerializeField]
		private float _ScoreInterval = 0.0f;
		[Tooltip ("The radius around the enemy for when to award score."), SerializeField]
		private float _GrazeRadius = 2f;
		
		[Header ("Outline Settings")]
		[Tooltip ("The object to use to display an outline"), SerializeField]
		private SpriteRenderer _OutlinePrefab = null;
		[Tooltip ("How strong the alpha is when visible."), SerializeField]
		private float _OutlineVisible = 0.5f;
		[Tooltip ("How low the alpha is when invisible"), SerializeField]
		private float _OutlineInvisible = 0.0f;
		
		private Circle _GrazeCircle;
		/// <summary>Reference to the player position.</summary>
		private Transform _Target = null;
		/// <summary>Reference to this object's transform component.</summary>
		private Transform _Transform = null;
		/// <summary>Regulator for awarding score at intervals.</summary>
		private Regulator _ScoringRegulator = null;
		/// <summary>Transform component of graze outline.</summary>
		private Transform _OutlineTransform = null;
		/// <summary>Renderer component of graze outline.</summary>
		private SpriteRenderer _OutlineRenderer = null;

		private void Awake ()
		{
			_Transform = GetComponent <Transform> ();
			_ScoringRegulator = new Regulator (_ScoreInterval);
			_GrazeCircle = new Circle (_GrazeRadius, _Transform);
			
			SetupOutline ();
		}

		private void SetupOutline ()
		{
			_OutlineRenderer = Instantiate (_OutlinePrefab, _Transform, false);
			_OutlineTransform = _OutlineRenderer.GetComponent<Transform> ();
			
			_OutlineTransform.localScale = new Vector3 (_GrazeRadius, _GrazeRadius, 1.0f);
			_OutlineRenderer.color = _OutlineRenderer.color.Alpha (0.0f);
		}

		private void Start ()
		{
			_Target = FindObjectOfType<PlayerController> ().transform;	
		}

		private void Update ()
		{
			// Are we intersecting with the target?
			if (_GrazeCircle.IsIntersecting (_Target))
			{
				// Show the graze outline.
				_OutlineRenderer.color = _OutlineRenderer.color.Alpha (_OutlineVisible);
				
				// If we can increase the score, then do so.
				if (_ScoringRegulator.HasElapsed (true))
					LevelSignals.OnScoreIncreased?.Invoke (_Score);
			}
			else
			{
				// Hide the graze outline.
				_OutlineRenderer.color = _OutlineRenderer.color.Alpha (_OutlineInvisible);
			}
		}
	}
}
