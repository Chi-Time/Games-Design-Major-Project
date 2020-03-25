using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SoulEngine
{
	//TODO: Consider doing a system whereby you scale the COLLIDER instead of the ACTUAL object and instead make use of an animation which is purely visual to display the damage. The problem with the current system is that you need to scale the object itself for the effect, but with a visual sprite and simple scale of the collider instead it could work better.
	//TODO: Find a way to not have to copy data from the AOE bullet _into_ this one as it's not very good desing.
	//Maybe consider making it so that each bullet has a behaviour component that defines what they _do_ so in this case it would define the bullet as blowing up at the spot.
	//And this could then be easily applied to the flak bullet with the same logic.
	//TODO: Consider abstracting collision logic into seperate component so that we can simply copy the collision checks into this rather than the tags.
	//TODO: Consider making a seperate component just for damage data storage rather than an interface.
	public class ExplosionComponent : MonoBehaviour, IRequireComponents, IDamage
	{
		public int Damage { get; private set; }
		public GameObject GameObject => gameObject;

		[Tooltip ("Should this object be destroyed when being culled?"), SerializeField]
		private bool _ShouldDestroy = false;
		[Tooltip ("How long does the explosion take to disappear?"), SerializeField]
		private float _ExplosionLength = 0.0f;
		[Tooltip ("The max size of the explosion radius"), SerializeField]
		private float _ExplosionSize = 0.0f;

		private bool _CanDamage = false;
		private float _StepLength = 0.0f;
		private Transform _Transform = null;
		private TagComponent _TagComponent = null;
		private Vector2 _CachedScale = Vector2.zero;
		private CircleCollider2D _Collider2D = null;
		private TagController _TagController = new TagController ();

		public IEnumerable<Type> RequiredComponents ()
		{
			return new Type[]
			{
				typeof (TagComponent),
			};
		}

		public void Construct (int damage, EditorTags[] tags)
		{
			Damage = damage;
			_TagController.Tags = tags;
		}

		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Collider2D = GetComponent<CircleCollider2D> ();
			_CachedScale = _Transform.localScale;
			_TagComponent = GetComponent<TagComponent> ();
			
			LevelSignals.OnBombExploded += OnBombExploded;
		}

		private void OnEnable ()
		{
			_CanDamage = true;
			_StepLength = _ExplosionLength * 0.5f;
			Invoke (nameof(Cull), _ExplosionLength);
			StartCoroutine (ScaleTo (Vector2.one * _ExplosionSize));
		}

		private void OnDisable ()
		{
			CancelInvoke ();
			StopAllCoroutines ();
			_Transform.localScale = _CachedScale;
		}

		private void OnDestroy ()
		{
			LevelSignals.OnBombExploded -= OnBombExploded;
		}

		private void OnBombExploded ()
		{
			if (gameObject.activeInHierarchy)
				Cull ();
		}

		IEnumerator ScaleTo (float endRadius)
		{
			var time = 0.0f;
			var startRadius = _Collider2D.radius;
			
			while (time <= _StepLength)
			{
				time += Time.deltaTime;
				_Collider2D.radius = Mathf.Lerp (startRadius, endRadius, time / _StepLength);
				
				yield return new WaitForEndOfFrame ();
			}

			_Collider2D.radius = endRadius;

			StartCoroutine (ScaleTo (startRadius));
		}

		IEnumerator ScaleTo (Vector2 endScale)
		{
			var time = 0.0f;
			var startScale = _Transform.localScale;
			
			while (time <= _StepLength)
			{
				time += Time.deltaTime;
				_Transform.localScale = Vector3.Lerp (startScale, endScale, time / _StepLength);
				
				yield return new WaitForEndOfFrame ();
			}

			_Transform.localScale = endScale;

			StartCoroutine (ScaleTo (startScale));
		}

		private void Cull ()
		{
			if (_ShouldDestroy)
				Destroy (gameObject);

			gameObject.SetActive (false);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags) && _CanDamage)
			{
				//TODO: Figure out how to damage player when not using a bullet component to deal the damage.
				LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
				_CanDamage = false;
			}
		}
	}
}
