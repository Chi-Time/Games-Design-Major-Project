using UnityEngine;
using Utilities;

namespace SoulEngine
{
	public class ItemDropComponent : MonoBehaviour
	{
		[Tooltip ("The amount to drop of the item."), SerializeField]
		private int _Amount = 1;
		[Tooltip ("The variance in position when dropping the item after death."), SerializeField]
		private float _Variance = 2.0f;
		[Tooltip ("The item type to spawn upon dropping."), SerializeField]
		private Transform _ItemPrefab = null;

		private Transform _Transform = null;
		private ObjectPool<Transform> _ItemPool = new ObjectPool<Transform> ();

		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			//TODO: Consider using global resource pool and grabbing stuff when it's available rather than having seperate pools per item component and wasting memory.
			_ItemPool.Construct (this, _ItemPrefab, _Amount, $"POOL: {gameObject.name} - Item Pool");
		}
		
		public void Drop ()
		{
			for (int i = 0; i <= _Amount; i++)
			{
				print ("spawning");
				var item = _ItemPool.Get ();

				if (item != null)
				{
					print ("Dropping it");
					SpawnDrop (item);
				}
			}
		}

		private void SpawnDrop (Transform item)
		{
			item.gameObject.SetActive (true);
			item.position = (UnityEngine.Random.insideUnitCircle * _Variance) + (Vector2)_Transform.position;
		}
	}
}
