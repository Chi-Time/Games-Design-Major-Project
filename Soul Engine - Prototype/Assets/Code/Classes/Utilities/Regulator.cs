using System;
using UnityEngine;

namespace Utilities
{
	[Serializable]
	public class Regulator
	{
		/// <summary>The current tick interval (in seconds) of the regulator.</summary>
		public float Interval { get; private set; }

		/// <summary>The internal time handler.</summary>
		private float _Clock = 0.0f;
		[Tooltip (""), SerializeField]
		private float _Interval = 0.0f;
		
		/// <summary>Creates a new regulator instance to handle time regulation.</summary>
		/// <param name="interval">How often should this regulator handle intervals?</param>
		public Regulator (float interval)
		{
			Interval = interval;
		}
		
		/// <summary>Determines if an interval has elapsed.</summary>
		/// <param name="shouldTick">Should we tick the internal clock while checking?</param>
		/// <returns>True: If the interval has been reached.</returns>
		public bool HasElapsed (bool shouldTick)
		{
			if (shouldTick)
				Tick ();
			
			if (_Clock < Interval)
				return false;

			_Clock = 0.0f;
			return true;
		}

		/// <summary>Update the internal clock's timer.</summary>
		public void Tick ()
		{
			_Clock += Time.deltaTime;
		}
	}
}
