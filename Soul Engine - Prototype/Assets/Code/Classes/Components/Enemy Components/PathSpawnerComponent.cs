using UnityEngine;
using Pixelplacement;
using Utilities;

namespace SoulEngine
{
	[RequireComponent (typeof(Spline), typeof (SpawnComponent))]
	public class PathSpawnerComponent : MonoBehaviour
	{
		[Tooltip ("What is the name of the current path?"), SerializeField]
		private string _Name = "";
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
				follower.Path = _Path;
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
