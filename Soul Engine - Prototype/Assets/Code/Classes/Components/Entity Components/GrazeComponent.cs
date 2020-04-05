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
		
		/// <summary>Reference to the player position.</summary>
		private Transform _Target = null;
		/// <summary>Reference to this object's transform component.</summary>
		private Transform _Transform = null;
		/// <summary>Cached value for the graze radius</summary>
		private float _GrazeRadiusSquared = 0.0f;
		/// <summary>Regulator for awarding score at intervals.</summary>
		private Regulator _ScoringRegulator = null;
		/// <summary>Transform component of graze outline.</summary>
		private Transform _OutlineTransform = null;
		/// <summary>Renderer component of graze outline.</summary>
		private SpriteRenderer _OutlineRenderer = null;
		/// <summary>The current distance the player is from the bullet.</summary>
		private float _CurrentDistance = float.MaxValue;

		private void Awake ()
		{
			_Transform = GetComponent <Transform> ();
			_ScoringRegulator = new Regulator (_ScoreInterval);
			
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
			// Cache the squared radius of this object.
			_GrazeRadiusSquared = _GrazeRadius * _GrazeRadius;
			_Target = FindObjectOfType<PlayerController> ().transform;	
		}

		private void Update ()
		{
			SetCurrentDistance ();
			SetOutlineAlpha ();
			UpdateScore ();
		}

		private void SetCurrentDistance ()
		{
			_CurrentDistance = Mathy.SqrDistance (_Transform.position, _Target.position);
		}

		private void SetOutlineAlpha ()
		{
			//TODO: Remove this additional if branch as we're doing the same check twice when we could save performance and do it once.
			_OutlineRenderer.color = _OutlineRenderer.color.Alpha (IsGrazing () ? _OutlineVisible : _OutlineInvisible);
		}

		private void UpdateScore ()
		{
			if (IsGrazing () && _ScoringRegulator.HasElapsed (true))
			{
				LevelSignals.OnScoreIncreased?.Invoke (_Score);
			}
		}

		private bool IsGrazing ()
		{
			return _CurrentDistance < _GrazeRadiusSquared;
		}
	}
}
