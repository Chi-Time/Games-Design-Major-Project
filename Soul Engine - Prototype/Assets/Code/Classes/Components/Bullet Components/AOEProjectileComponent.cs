using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulEngine
{
	public class AOEProjectileComponent : BulletComponent
	{
		[Header ("AOE Settings")]
		[Space (2)]
		[Tooltip ("The min and max distance a flak explosion can occur from the point of firing."), SerializeField]
		private Boundary _ExplosionDistances = Boundary.Zero;
		[Tooltip ("The marker to display the current explosion."), SerializeField]
		private Transform _Marker = null;
		[Tooltip ("The explosion to spawn on detonation"), SerializeField]
		private ExplosionComponent _Explosion = null;

		private bool _WasFired = false;
		private Collider2D _Collider2D = null;

		protected override void Awake ()
		{
			base.Awake ();
			
			_Rigidbody2D.isKinematic = false;
			_Collider2D = GetComponent<Collider2D> ();
			_Explosion.gameObject.SetActive (false);
			_Marker.gameObject.SetActive (false);
		}

		//TODO: Flak and AOE projectiles share many similarities consider sharing the code between the two or abstracting them into a seperate component.
		protected override void OnEnable ()
		{
			var position = SelectTargetPosition ();

			_WasFired = false;
			_Collider2D.enabled = true;
			_Explosion.Construct (_Damage, _Tags);
			_Explosion.transform.position = position;
			_Marker.gameObject.SetActive (true);
			_Marker.position = position;
			_Transform.position = _Transform.parent.position;

			Invoke (nameof(Explode), _Lifetime);
		}

		protected override void OnDisable ()
		{
			base.OnDisable ();
			
			CancelInvoke (nameof(Explode));
			_Marker.gameObject.SetActive (false);
		}

		private Vector3 SelectTargetPosition ()
		{
			return Random.insideUnitCircle * _ExplosionDistances.Max;
		}

		private void Update ()
		{
			if (_WasFired && _Explosion.isActiveAndEnabled == false)
			{
				// Then deactivate ourselves for later use.
				_Transform.parent.gameObject.SetActive (false);
			}
		}

		private void Explode ()
		{
			_WasFired = true;
			_Collider2D.enabled = false;
			_Explosion.gameObject.SetActive (true);
			_Marker.gameObject.SetActive (false);
		}

		protected override void EnteredCollider (Collider2D other)
		{ }

		protected override void ExitedCollider (Collider2D other)
		{ }
	}
}
