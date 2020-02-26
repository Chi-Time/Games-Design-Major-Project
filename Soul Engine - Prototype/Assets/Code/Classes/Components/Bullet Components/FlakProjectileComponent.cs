using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulEngine
{
	//TODO: Find a better way to handle the logic. Currently turning off the renderer and tracking the parent is stupid and messy, RECONSIDER THIS WITH FREETIME.
	//TODO: Consider making bullet movement logic it's own component so that each bullet type simply adds the component with the relevant logic it needs.
	public class FlakProjectileComponent : BulletComponent
	{
		[Header ("Flak Settings")]
		[Space (2)]
		[Tooltip ("The min and max distance a flak explosion can occur from the point of firing."), SerializeField]
		private Boundary _ExplosionDistances = Boundary.Zero;
		[Tooltip ("The image to display to the player where the explosion will happen."), SerializeField]
		private Transform _Marker = null;
		[Tooltip ("The explosion to spawn upon detonation."), SerializeField]
		private ExplosionComponent _Explosion = null;
		
		private Renderer _Renderer = null;
		private CircleCollider2D _Collider2D = null;

		protected override void Awake ()
		{
			base.Awake ();
			_Renderer = GetComponent<Renderer> ();
			_Explosion.gameObject.SetActive (false);
			_Marker.gameObject.SetActive (false);
			_Collider2D = GetComponent<CircleCollider2D> ();
		}

		protected override void OnEnable ()
		{
			var position = SelectDetonationTarget ();
			_Renderer.enabled = true;
			_Collider2D.enabled = true;
			_Explosion.Construct (_Damage, _Tags);
			_Explosion.transform.position = position;
			_Transform.position = _Transform.parent.position;
			
			_Marker.position = position;
			_Marker.gameObject.SetActive (true);
			
			_Transform.up = position - _Transform.position;
		}

		//TODO: Consider making it so that it always selects the current position of the player at the time so that they have to move away and it provides a timed denial of the area from the player.
		private Vector3 SelectDetonationTarget ()
		{
			return Random.insideUnitCircle * _ExplosionDistances.Max;
		}

		protected override void OnDisable ()
		{
			_Marker.gameObject.SetActive (false);
		}

		private void Update ()
		{
			if (_Renderer.enabled)
			{
				if (Approximation.Vector3 (_Marker.position, _Transform.position, 0.1f))
				{
					Explode ();
				}
			
				_Rigidbody2D.MovePosition (_Rigidbody2D.position + (Vector2)_Transform.up * (_Speed * Time.deltaTime));
			}
			//If we're currently disabled.
			else
			{
				// And the explosion has ended.
				if (_Explosion.isActiveAndEnabled == false)
				{
					// Then deactivate ourselves for later use.
					_Transform.parent.gameObject.SetActive (false);
				}
			}
		}

		private void Explode ()
		{
			_Renderer.enabled = false;
			_Collider2D.enabled = false;
			_Marker.gameObject.SetActive (false);

			_Explosion.gameObject.SetActive (true);
		}

		protected override void EnteredCollider (Collider2D other)
		{
			Explode ();
			LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
		}

		protected override void ExitedCollider (Collider2D other)
		{
		}
	}
}
