using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulEngine
{
	public enum LevelStates { Play, Paused, Failed, Complete }
	
	public class LevelController : MonoBehaviour
	{
		[SerializeField]
		private int _Score = 0;
		[SerializeField]
		private int _RescuedEntities = 0;
		[SerializeField]
		private int _ResourcesCollected = 0;
		[SerializeField]
		private int _EnemiesKilled = 0;
		[SerializeField]
		private bool _HitTaken = false;
		[SerializeField]
		private LevelStates _CurrentState = LevelStates.Play;

		private GameObject _Player = null;

		private void Awake ()
		{
		}

		private void Start ()
		{
			_Player = FindObjectOfType<PlayerController> ().gameObject;
			
			LevelSignals.OnEntityHit += OnEntityHit;
			LevelSignals.OnEntityKilled += OnEntityKilled;
			LevelSignals.OnEntityRescued += OnEntityRescued;
			LevelSignals.OnScoreIncreased += OnScoreIncreased;
			LevelSignals.OnResourceCollected += OnResourcesCollected;
		}

		private void OnScoreIncreased (int score)
		{
			if (_Score > 0)
				_Score += score;
		}

		private void OnEntityRescued (GameObject entity)
		{
			_RescuedEntities++;
			//TODO: Find out which character was saved and store the information for the log.
		}

		private void OnResourcesCollected (int resources)
		{
			if (resources > 0)
				_RescuedEntities += resources;
		}

		private void OnEntityHit (IDamage damage, GameObject other)
		{
			if (Equals (other, _Player))
				_HitTaken = true;
			
			//TODO: Find a way to negate this if the player has a perk enabled.
		}

		private void OnEntityKilled (GameObject other)
		{
			if (Equals (other, _Player))
				LevelSignals.OnStateChanged?.Invoke (LevelStates.Failed);
		}

		private void OnStateChanged (LevelStates state)
		{
			switch (state)
			{
				case LevelStates.Play:
					break;
				case LevelStates.Paused:
					break;
				case LevelStates.Failed:
					break;
				case LevelStates.Complete:
					break;
			}
		}

		private void OnDestroy ()
		{
			LevelSignals.OnEntityHit -= OnEntityHit;
			LevelSignals.OnEntityKilled -= OnEntityKilled;
			LevelSignals.OnEntityRescued -= OnEntityRescued;
			LevelSignals.OnScoreIncreased -= OnScoreIncreased;
			LevelSignals.OnResourceCollected -= OnResourcesCollected;
		}

		private void Update ()
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
}
