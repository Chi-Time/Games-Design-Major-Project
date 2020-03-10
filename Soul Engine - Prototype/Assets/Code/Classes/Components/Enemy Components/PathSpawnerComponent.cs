using UnityEngine;
using Pixelplacement;
using Utilities;

namespace SoulEngine
{
	[RequireComponent (typeof(Spline), typeof (SpawnComponent))]
	public class PathSpawnerComponent : MonoBehaviour
	{
		[Header ("Path Settings")]
		[Tooltip ("What is the name of the current path?"), SerializeField]
		private string _Name = "";
		[Tooltip ("The speed at which the followers move across the path. (In Seconds)"), SerializeField]
		private float _Speed = 5f;
		[Tooltip ("The animation type for the follower's movement."), SerializeField]
		private LerpCurves.LerpType _CurveType = LerpCurves.LerpType.SmoothStep;
		
		[Header ("Spawn Settings")]
		[Tooltip ("How many enemies should be spawned on the current path."), SerializeField]
		private int _SpawnCount = 1;
		[Tooltip ("The delay between each enemy spawn on this path."), SerializeField]
		private float _SpawnDelay = 0.5f;
		[Tooltip ("The follower to spawn beneath this instance."), SerializeField]
		private PathFollowerComponent _FollowerPrefab = null;

		private Spline _Path = null;
		private int _SpawnedCounter = 0;
		private ObjectPool<PathFollowerComponent> _FollowerPool = new ObjectPool<PathFollowerComponent> ();

		private void Awake ()
		{
			_Path = GetComponent<Spline> ();
			_FollowerPool.Construct (this, _FollowerPrefab, _SpawnCount, _Name);
		}

		private void OnEnable ()
		{
			_SpawnedCounter = 0;
			InvokeRepeating (nameof(SpawnFollower), _SpawnDelay, _SpawnDelay);
		}

		private void OnDisable ()
		{
			CancelInvoke ();
		}

		private void SpawnFollower ()
		{
			var follower = _FollowerPool.Get ();
			if (follower != null)
			{
				_SpawnedCounter++;
				follower.Setup (_Speed, _Path, _CurveType);
				follower.transform.position = _Path.GetPosition (0.0f);
				follower.gameObject.SetActive (true);
			}

			if (_SpawnedCounter >= _SpawnCount)
			{
				CancelInvoke ();
			}
		}
	}
}
