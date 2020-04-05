using UnityEngine;

namespace SoulEngine
{
	[RequireComponent (typeof (SpriteRenderer))]
	public class OutlineComponent : MonoBehaviour
	{
		[Tooltip ("The strength of the outline when enabled"), SerializeField, Range (0.0f, 1.0f)]
		private float _EnabledStrength = 1.0f;
		[Tooltip ("The strength of the outline when disabled"), SerializeField, Range (0.0f, 1.0f)]
		private float _DisabledStrength = 0.0f;

		private Transform _Transform = null;
		private SpriteRenderer _Renderer = null;

		private void Awake ()
		{
			_Transform = GetComponent<Transform> ();
			_Renderer = GetComponent<SpriteRenderer> ();
		}

		/// <summary>Resizes the outline to the given size.</summary>
		/// <param name="size">The size of the outline in the world.</param>
		public void Resize (float size)
		{
			_Transform.localScale = new Vector3 (size, size, size);
		}

		/// <summary>Shows/Hides the outline.</summary>
		/// <param name="shouldDisplay">Should the outline be displayed?</param>
		public void Show (bool shouldDisplay)
		{
			if (shouldDisplay)
				_Renderer.color = _Renderer.color.Alpha (_EnabledStrength);
			else
				_Renderer.color = _Renderer.color.Alpha (_DisabledStrength);
		}
	}
}
