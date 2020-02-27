using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulEngine
{
	public class AOEProjectileComponent : BulletComponent
	{
		[Header ("AOE Settings")]
		[Space (2)]
		[Tooltip ("The random distance the explosion can be from it's target."), SerializeField]
		private float _Distance = 1.0f;
		[Tooltip ("The marker to display the current explosion."), SerializeField]
		private Transform _Marker = null;
		[Tooltip ("The explosion to spawn on detonation"), SerializeField]
		private ExplosionComponent _Explosion = null;

		private bool _WasFired = false; 
		private Transform _Target = null;
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
			CacheTarget ();
			var position = SelectTargetPosition ();
			SetupBullet ();
			SetupMarker (ref position);
			SetupExplosion (ref position);

			Invoke (nameof(Explode), _Lifetime);
		}
		
		private void CacheTarget ()
		{
			_Target = FindObjectOfType<PlayerController> ().transform; 
		}

		private void SetupBullet ()
		{
			_WasFired = false;
			_Collider2D.enabled = true;
			_Transform.position = _Transform.parent.position;
		}

		private void SetupMarker (ref Vector3 position)
		{
			_Marker.gameObject.SetActive (true);
			_Marker.position = position;
		}

		private void SetupExplosion (ref Vector3 position)
		{
			_Explosion.Construct (_Damage, _Tags);
			_Explosion.transform.position = position;
		}

		protected override void OnDisable ()
		{
			base.OnDisable ();
			
			CancelInvoke (nameof(Explode));
			_Marker.gameObject.SetActive (false);
		}

		private Vector3 SelectTargetPosition ()
		{
			return _Target.position + (Vector3)Random.insideUnitCircle * _Distance;
		}

		//TODO: Improve this as using a WasFired is ugly and needless.
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
