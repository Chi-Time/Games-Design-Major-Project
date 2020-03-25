using UnityEngine;

namespace SoulEngine
{
	public abstract class AbilityComponent : MonoBehaviour
	{
		[Tooltip ("The number of times this ability can be used."), SerializeField]
		protected int _UseCount = 0;

		private int _CurrentUses = 0;
		protected bool _IsInUse = false;

		public void Use ()
		{
			if (CanActivate ())
			{
				_IsInUse = true;
				_CurrentUses++;
				Activate ();
			}
		}

		private bool CanActivate ()
		{
			return _CurrentUses < _UseCount && !_IsInUse;
		}
		
		protected abstract void Activate ();
		protected abstract void Deactivate ();
	}
}
