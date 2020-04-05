using System;
using UnityEngine;

namespace Code.Classes.Utilities
{
	[Serializable]
	public struct Circle
	{
		public float Radius 
		{
			get => Radius;
			set => Recalculate (value);
		}
		
		public float SqrRadius { get; private set; }
		public float Diameter { get; private set; }
		public float Circumference { get; private set; }
		public Vector2 Center { get; set; }

		public static Circle One => new Circle (1.0f, new Vector2 (1f, 1f));

		public Circle (float radius, Vector2 center) : this ()
		{
			Recalculate (radius);
			Center = center;
		}

		public void Recalculate (float radius)
		{
			Radius = radius;
			SqrRadius = radius * radius;
			Diameter = radius * 2f;
			Circumference = Mathf.PI * Diameter;
		}
	}
}
