using System;
using UnityEngine;

namespace Code.Classes.Utilities
{
	[Serializable]
	public class Time
	{
		public int Seconds => _Seconds;
		public int Minutes => _Minutes;
		public int Hours => _Hours;
		
		private const int HourCap = 99;
		private const int SenaryCap = 60;

		[SerializeField]
		private int _Seconds = 0;
		[SerializeField]
		private int _Minutes = 0;
		[SerializeField]
		private int _Hours = 0;
		
		private float _Tick = 0.0f;
		[SerializeField]
		private float _Clock = 0.0f;
		private bool _CanTick = true;
		
		/// <summary>Shorthand for writing new Time (0,0,0);</summary>
		public static Time Zero => new Time (0, 0, 0);

		public Time (int seconds, int minutes, int hours)
		{
			_Seconds = seconds;
			_Minutes = minutes;
			_Hours = hours;
			_CanTick = true;
		}

		public TimeSpan GetTime ()
		{
			return TimeSpan.FromSeconds (_Clock);
		}
		
		public TimeSpan GetTime (float seconds)
		{
			return TimeSpan.FromSeconds (seconds);
		}

		public string GetFormattedTime ()
		{
			var time = GetTime ();

			return $"{time.Hours.ToString ()}:{time.Minutes.ToString ()}:{time.Seconds.ToString ()}:{time.Milliseconds.ToString ()}";
		}
		
		public string GetFormattedTime (float seconds)
		{
			var time = GetTime (seconds);

			return $"{time.Hours.ToString ()}:{time.Minutes.ToString ()}:{time.Seconds.ToString ()}:{time.Milliseconds.ToString ()}";
		}
		
		public string GetFormattedTime (TimeSpan time)
		{
			return $"{time.Hours.ToString ()}:{time.Minutes.ToString ()}:{time.Seconds.ToString ()}:{time.Milliseconds.ToString ()}";
		}

		public void SetSeconds (int value)
		{ }

		public void SetMinutes (int value)
		{ }

		public void SetHours (int value)
		{ }
		
		public void Tick ()
		{
			if (_CanTick == false)
				return;

			_Clock += UnityEngine.Time.deltaTime;

			_Tick += UnityEngine.Time.deltaTime;

			if (_Tick >= 1)
			{
				TickSeconds ();
				_Tick = 0.0f;
			}
		}

		private void TickSeconds ()
		{
			_Seconds++;

			if (_Seconds == 60)
			{
				_Seconds = 0;
				TickMinutes ();
			}
		}

		private void TickMinutes ()
		{
			_Minutes++;

			if (_Minutes == 60)
			{
				_Minutes = 0;
				TickHours ();
			}
		}

		private void TickHours ()
		{
			_Hours++;

			if (_Hours == 99)
				_CanTick = false;
		}
	}
}
