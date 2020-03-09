using System;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace SoulEngine
{
	[Serializable]
	public class Item
	{
		public int Amount => _Amount;
		public float DropRate => _DropRate;
		public ObjectPool<Transform> Pool { get; private set; }

		[Tooltip ("The name of the item to be dropped."), SerializeField]
		private string _Name = "Item";
		[Tooltip ("The amount to drop of the item."), SerializeField]
		private int _Amount = 1;
		[Tooltip ("The chance out of 10 which the item has of being spawned."), Range (0f, 1.0f), SerializeField]
		private float _DropRate = 1.0f;
		[Tooltip ("The item type to spawn upon dropping."), SerializeField]
		private Transform _ItemPrefab = null;

		public void Construct (MonoBehaviour owner)
		{
			Pool = new ObjectPool<Transform> ();
			Pool.Construct (owner, _ItemPrefab, _Amount, $"POOL: {_Name}");
		}
	}

	public class ItemDropComponent : MonoBehaviour
	{
		[Tooltip ("The various items that have a chance to be dropped."), SerializeField]
		private Item[] _Items = null;
		[Tooltip ("The variance in position when dropping the item after death."), SerializeField]
		private float _Variance = 2.0f;

		private Transform _Transform = null;

		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();

			foreach (var item in _Items)
			{
				item.Construct (this);
			}
		}
		
		public void Drop ()
		{
			// Loop through every item in the weight table.
			foreach (var item in _Items)
			{
				// If the chance to drop has been met.
				if (Random.Range (0f, 1f) <= item.DropRate)
				{
					// Drop the specified number of items if they exist in the pool.
					for (int i = 0; i < item.Amount; i++)
					{
						var itemGO = item.Pool.Get ();

						if (itemGO != null)
						{
							SpawnDrop (itemGO);
						}
					}
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
