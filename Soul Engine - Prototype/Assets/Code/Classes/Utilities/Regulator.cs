using System;
using UnityEngine;

namespace Utilities
{
	[Serializable]
	public class Regulator
	{
		/// <summary>The current tick interval (in seconds) of the regulator.</summary>
		public float Interval => _Interval;

		[Tooltip ("The set interval to wait for."), SerializeField]
		private float _Interval = 0.0f;

		/// <summary>The internal time handler.</summary>
		private float _Clock = 0.0f;

		/// <summary>Creates a new regulator instance to handle time regulation.</summary>
		/// <param name="interval">How often should this regulator handle intervals?</param>
		public Regulator (float interval)
		{
			_Interval = interval;
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

		/// <summary>Reset the internal clock counter so that it begins again.</summary>
		/// <param name="resetToInterval">Should the clock start from 0 or start at the interval?</param>
		public void Reset (bool resetToInterval)
		{
			_Clock = resetToInterval ? _Interval : 0.0f;
		}

		/// <summary>Update the internal clock's timer.</summary>
		public void Tick ()
		{
			_Clock += Time.deltaTime;
		}
	}
}
