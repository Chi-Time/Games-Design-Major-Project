﻿using System;
using UnityEngine;

namespace SoulEngine
{
	public static class LevelSignals
	{
		public static Action OnGamePaused;
		public static Action OnLevelFailed;
		public static Action OnLevelComplete;
		public static Action<GameObject> OnEntityEMP;
		public static Action<GameObject> OnEntityKilled;
		public static Action<GameObject> OnEntityRescued;
		public static Action<bool, float> OnEntityBubbled;
		public static Action<GameObject> OnResourceCollected;
		public static Action<IDamage, GameObject> OnEntityHit;
		//TODO: Consider implementing custom collision callback which is called when any ontriggerenter is and passes an extended collider with better enum based tags instead.
	}
}
