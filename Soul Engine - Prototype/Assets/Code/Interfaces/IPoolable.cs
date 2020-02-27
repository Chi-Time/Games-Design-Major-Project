using UnityEngine;

namespace SoulEngine
{
	public interface IPoolable
	{
		/// <summary>Accessor to the object's cached transform component.</summary>
		Transform Transform { get; set; }

		/// <summary>Allows the object to set itself up for use.</summary>
		void Activate ();
	}
}
