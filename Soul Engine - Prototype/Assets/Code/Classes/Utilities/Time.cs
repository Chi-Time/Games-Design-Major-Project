using System;

namespace Code.Classes.Utilities
{
	[Serializable]
	public class Time
	{
		public int Seconds { get; private set; }
		public int Minutes { get; private set; }
		public int Hours { get; private set; }

		private const int Cap = 60;
		private const int Max = 99;
		private float _Tick = 0.0f;
		private bool _CanTick = true;
		
		/// <summary>Shorthand for writing new Time (0,0,0);</summary>
		public static Time Zero => new Time (0, 0, 0);

		public Time (int seconds, int minutes, int hours)
		{
			Seconds = seconds;
			Minutes = minutes;
			Hours = hours;
			_CanTick = true;
		}

		public void SetSeconds (int value)
		{ }

		public void SetMinutes (int value)
		{ }

		public void SetHours (int value)
		{ }
		
		public void UpdateTime (float deltaTime)
		{
			if (_CanTick == false)
				return;

			_Tick += deltaTime;

			if (_Tick >= 1)
			{
				TickSeconds ();
				_Tick = 0.0f;
			}
		}

		private void TickSeconds ()
		{
			Seconds++;

			if (Seconds == 60)
			{
				Seconds = 0;
				TickMinutes ();
			}
		}

		private void TickMinutes ()
		{
			Minutes++;

			if (Minutes == 60)
			{
				Minutes = 0;
				TickHours ();
			}
		}

		private void TickHours ()
		{
			Hours++;

			if (Hours == 99)
				_CanTick = false;
		}
	}
}
