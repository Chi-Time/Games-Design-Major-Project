using UnityEngine;
using Utilities;

namespace SoulEngine
{
	[RequireComponent(typeof (Collider2D), typeof (TagComponent))]
	public class LaserBulletComponent : MonoBehaviour, IDamage
	{
		public int Damage => _Damage;
		
		[Tooltip ("How much damage should the laser deal per hit?"), SerializeField]
		private int _Damage = 0;
		[Tooltip ("How long is the laser's reach?"), SerializeField]
		private float _Range = 0.0f;
		[Tooltip ("How often should the laser damage the player while they're being hit?"), SerializeField]
		private Regulator _DamageRegulator = null;
		[Tooltip ("The tag manager for this component."), SerializeField]
		private TagController _TagController = null;

		private void Awake ()
		{
			var localScale = transform.localScale;
			transform.localScale = new Vector3 (localScale.x, _Range,  localScale.z);

			var collider2D = GetComponent<BoxCollider2D> ();
			collider2D.isTrigger = true;
			collider2D.offset = new Vector2 (collider2D.offset.x, 0.5f);
		}

		private void Update ()
		{
			_DamageRegulator.Tick ();
		}

		private void OnTriggerStay2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				Hit (other.gameObject);
			}
		}

		private void OnTriggerExit2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
				_DamageRegulator.Reset (true);
		}

		private void Hit (GameObject other)
		{
			if (_DamageRegulator.HasElapsed (false))
				LevelSignals.OnEntityHit?.Invoke (this, other);
		}
	}
}
