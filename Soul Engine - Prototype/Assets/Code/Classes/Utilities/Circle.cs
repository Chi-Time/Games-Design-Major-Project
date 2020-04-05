using System;
using UnityEngine;

namespace Code.Classes.Utilities
{
	[Serializable]
	public struct Circle
	{
		/// <summary>The circles current radius.</summary>
		public float Radius
		{
			get => _Radius;
			set => Resize (value);
		}

		/// <summary>The center position of the circle.</summary>
		public Vector2 Center => _Transform.position;

		/// <summary>The circles current radius squared.</summary>
		public float SqrRadius { get; private set; }
		/// <summary>The current diameter of the circle.</summary>
		public float Diameter { get; private set; }
		/// <summary>The circles current circumference.</summary>
		public float Circumference { get; private set; }

		private float _Radius;
		/// <summary>The transform component that this circle relies on.</summary>
		private Transform _Transform;
		

		/// <summary>Constructs a new circle.</summary>
		/// <param name="radius">The radius of the circle. (All other parameters are calculated from this.)</param>
		/// <param name="transform">The transform this circle will rely on.</param>
		public Circle (float radius, Transform transform) : this ()
		{
			Resize (radius);
			_Transform = transform;
		}

		/// <summary>Determines if this circle is intersecting with a position in space.</summary>
		/// <param name="target">The target position to test against.</param>
		/// <returns>True if intersecting, false if not.</returns>
		public bool IsIntersecting (Transform target)
		{
			return Mathy.IsInRadius (this, target.position);
		}

		/// <summary>Determines if this circle is intersecting with another.</summary>
		/// <param name="target">The target circle to test against.</param>
		/// <returns>True if intersecting, false if not.</returns>
		public bool IsIntersecting (Circle target)
		{
			return Mathy.IntersectsCircle (this, target);
		}

		/// <summary>Resize the circle from a new radius.</summary>
		/// <param name="radius">The new radius to resize to.</param>
		public void Resize (float radius)
		{
			_Radius = radius;
			SqrRadius = radius * radius;
			Diameter = radius * 2f;
			Circumference = Mathf.PI * Diameter;
		}
	}
}
