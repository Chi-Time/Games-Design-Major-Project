	using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (Rigidbody2D), typeof (Collider2D))]
	public class MagneticComponent : MonoBehaviour
	{
		[Tooltip ("The speed of the object when being magnetised."), SerializeField]
		private float _Speed = 5.0f;
		[Tooltip ("The various tags this component should look for."), SerializeField]
		private TagController _TagController = new TagController ();

		private Transform _Target = null;
		private bool _IsMagnetised = false;
		private Transform _Transform = null;
		private Rigidbody2D _Rigidbody2D = null;

		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Rigidbody2D = GetComponent<Rigidbody2D> ();
			_TagController.Construct (this);
			_Rigidbody2D.isKinematic = true;
			_Rigidbody2D.gravityScale = 0.0f;
			_Rigidbody2D.freezeRotation = true;
			GetComponent<Collider2D> ().isTrigger = true;
		}

		private void Start ()
		{
			GetTarget ();
		}

		private void GetTarget ()
		{
			_Target = FindObjectOfType<PlayerController> ()?.transform;
		}

		private void FixedUpdate ()
		{
			if (_IsMagnetised)
				Move ();
		}

		private void Move ()
		{
			float speed = _Speed * Time.deltaTime;
			Vector2 position = _Transform.position;
			Vector2 velocity = ( (Vector2)_Target.position - position ).normalized;

			_Rigidbody2D.MovePosition (_Rigidbody2D.position + velocity * speed);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
				_IsMagnetised = true;
		}

		private void OnTriggerExit2D (Collider2D other)
		{
			if (other.HasTags (_TagController.Tags))
				_IsMagnetised = false;
		}
	}
}
