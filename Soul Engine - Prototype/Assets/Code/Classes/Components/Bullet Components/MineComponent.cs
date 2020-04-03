using System.Runtime;
using System.Security;
using UnityEngine;

namespace SoulEngine
{
	//TODO: Make additional behaviour so that the mine blows itself up when nearing the blow and spawning and explosion component.
	//This could add variance to the mine's behaviour.
	[RequireComponent (typeof (Collider2D), typeof (Rigidbody2D), typeof (TagComponent))]
	public class MineComponent : MonoBehaviour, IDamage
	{
		/// <summary>The damage this projectile inflicts.</summary>
		public int Damage => _Damage;
		
		private const float MaxMagnitude = 1.0f;

		[Tooltip ("The amount of damage this enemy deals to the player."), SerializeField]
		private int _Damage = 0;
		[Tooltip ("The speed of the mine when tracking the player."), SerializeField]
		private float _TrackSpeed = 7.5f;
		[Tooltip ("The radius around the object that will detect the player."), SerializeField]
		private float _DetectionRadius = 5f;
		[Tooltip ("The tags to look for."), SerializeField]
		private TagController _TagController = null;

		private bool _PlayerDetected = false;
		private Transform _Target = null;
		private Transform _Parent = null;
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
			_Parent = _Transform.parent;
		}

		private void Start ()
		{
			_Target = FindObjectOfType<PlayerController> ().transform;
		}

		private void Update ()
		{
			CheckDistance ();
			SetParent ();
		}

		private void CheckDistance ()
		{
			_PlayerDetected = ( Vector3.Distance (_Rigidbody2D.position, _Target.position) <= _DetectionRadius );
		}

		private void SetParent ()
		{
			if (_PlayerDetected && _Transform.parent != _Target.parent)
			{
				_Transform.SetParent (_Target.parent);
			}
			else if (_PlayerDetected == false && _Transform.parent != _Parent)
			{
				_Transform.SetParent (_Parent);
			}
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
			//TODO: This works currently but is not optimal.
			float step = _TrackSpeed * Time.deltaTime;
			_Transform.position = Vector3.MoveTowards (_Transform.position, _Target.position, step);
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
