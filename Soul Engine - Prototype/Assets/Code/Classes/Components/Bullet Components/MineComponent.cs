using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Collider2D), typeof (Rigidbody2D), typeof (TagComponent))]
	public class MineComponent : MonoBehaviour, IDamage
	{
		/// <summary>The damage this projectile inflicts.</summary>
		public int Damage { get; }
		
		private const float MaxMagnitude = 1.0f;
		
		[Tooltip ("The radius around the object that will detect the player."), SerializeField]
		private float _DetectionRadius = 2.5f;
		[Tooltip ("The speed of the mine when tracking the player."), SerializeField]
		private float _TrackSpeed = 0.0f;
		[Tooltip ("The tags to look for."), SerializeField]
		private TagController _TagController = null;
		
		private bool _PlayerDetected = false;
		private Transform _Target = null;
		private Transform _Transform = null;
		private Rigidbody2D _Rigidbody2D = null;

		private void Awake ()
		{
			GetComponent<Collider2D> ().isTrigger = true;
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.gravityScale = 0.0f;
			_Rigidbody2D.freezeRotation = false;
			_Transform = GetComponent<Transform> ();
		}

		private void Start ()
		{
			_Target = FindObjectOfType<PlayerController> ().transform;
		}

		private void Update ()
		{
			CheckDistance ();
		}

		private void CheckDistance ()
		{
			_PlayerDetected = ( Vector3.Distance (_Rigidbody2D.position, _Target.position) <= _DetectionRadius );
		}

		private void FixedUpdate ()
		{
			if (_PlayerDetected == false)
				return;

			FaceTarget ();
			Move ();
		}
		
		private void FaceTarget ()
		{
			_Transform.up = _Target.position - _Transform.position;
		}

		private void Move ()
		{
			var velocity = CalculateVelocity ();
			_Rigidbody2D.MovePosition (_Rigidbody2D.position + velocity);
		}

		private Vector2 CalculateVelocity ()
		{
			Vector2 velocity = _Transform.up * ( _TrackSpeed * Time.deltaTime );

			// Clamp max magnitude so that we don't have diagonal speedups.
			if (velocity.magnitude > MaxMagnitude)
				velocity.Normalize ();

			return velocity;
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
			{
				LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
				Cull ();
			}
		}

		private void Cull ()
		{
			this.gameObject.SetActive (false);
		}
	}
}
