using UnityEngine;

namespace Code.Classes.Utilities
{
	/// <summary>Custom Math Extensions for Unity.</summary>
	public static class Mathy
	{
		/// <summary>Calculates the distance between two positions.</summary>
		/// <param name="a">The position of the caller.</param>
		/// <param name="b">The position of the target.</param>
		/// <returns>The distance between the two positions.</returns>
		public static float SqrDistance (Vector2 a, Vector2 b)
		{
			var dx = b.x - a.x;
			var dy = b.y - a.y;

			return ( dx * dx ) + ( dy * dy );
		}

		/// <summary>Calculates the distance between two positions.</summary>
		/// <param name="a">The position of the caller.</param>
		/// <param name="b">The position of the target.</param>
		/// <returns>The distance between the two positions.</returns>
		public static float SqrDistance (Vector3 a, Vector3 b)
		{
			var dx = b.x - a.x;
			var dy = b.y - a.y;
			var dz = b.z - a.z;

			return ( dx * dx ) + ( dy * dy ) + ( dz * dz );
		}
		
		/// <summary>Compares the given position of a circle and a target to determine if they intersect.</summary>
		/// <param name="a">The position of the caller.</param>
		/// <param name="b">The position of the target.</param>
		/// <param name="sqrRadius">The (squared) radius of the circle to compare.</param>
		/// <returns>True if the point is in the radius, false if not.</returns>
		public static bool IsInRadius (Vector2 a, Vector2 b, float sqrRadius)
		{
			var sqrDist = SqrDistance (a, b);

			return sqrDist <= sqrRadius;
		}
		
		/// <summary>Compares the given position of a circle and a target to determine if they intersect.</summary>
		/// <param name="a">The position of the caller.</param>
		/// <param name="b">The position of the target.</param>
		/// <param name="sqrRadius">The (squared) radius of the circle to compare.</param>
		/// <returns>True if the point is in the radius, false if not.</returns>
		public static bool IsInRadius (Vector3 a, Vector3 b, float sqrRadius)
		{
			var sqrDist = SqrDistance (a, b);

			return sqrDist <= sqrRadius;
		}

		/// <summary>Compares the given position of a circle and a point to determine if they intersect.</summary>
		/// <param name="a">The caller.</param>
		/// <param name="b">The target point.</param>
		/// <returns>True if point is in circle radius, false if not.</returns>
		public static bool IsInRadius (Circle a, Vector2 b)
		{
			var sqrDist = SqrDistance (a.Center, b);

			return sqrDist < a.SqrRadius;
		}

		/// <summary>Compares the given position of a circle and a point to determine if they intersect.</summary>
		/// <param name="a">The caller.</param>
		/// <param name="b">The target point.</param>
		/// <returns>True if point is in circle radius, false if not.</returns>
		public static bool IsInRadius (Circle a, Vector3 b)
		{
			var sqrDist = SqrDistance ((Vector3)a.Center, b);

			return sqrDist < a.SqrRadius;
		}

		/// <summary>Determines if two circles are intersecting one another.</summary>
		/// <param name="a">The caller's center position.</param>
		/// <param name="b">The targets center position.</param>
		/// <param name="sqrRadiusA">The squared radius of the caller.</param>
		/// <param name="sqrRadiusB">The squared radius of the target.</param>
		/// <returns>True if an intersection occurs, false if not.</returns>
		public static bool IntersectsCircle (Vector2 a, Vector2 b, float sqrRadiusA, float sqrRadiusB)
		{
			var sqrDist = SqrDistance (a, b);

			return sqrDist < sqrRadiusA + sqrRadiusB;
		}

		/// <summary>Determines if two circles are intersecting one another.</summary>
		/// <param name="a">The caller's center position.</param>
		/// <param name="b">The targets center position.</param>
		/// <param name="sqrRadiusA">The squared radius of the caller.</param>
		/// <param name="sqrRadiusB">The squared radius of the target.</param>
		/// <returns>True if an intersection occurs, false if not.</returns>
		public static bool IntersectsCircle (Vector3 a, Vector3 b, float sqrRadiusA, float sqrRadiusB)
		{
			var sqrDist = SqrDistance (a, b);

			return sqrDist < sqrRadiusA + sqrRadiusB;
		}

		/// <summary>Determines if two circles are intersecting one another.</summary>
		/// <param name="a">The caller.</param>
		/// <param name="b">The target.</param>
		/// <returns>True if the circles are intersecting, false if not.</returns>
		public static bool IntersectsCircle (Circle a, Circle b)
		{
			var sqrDist = SqrDistance (a.Center, b.Center);

			return sqrDist < a.SqrRadius + b.SqrRadius;
		}

		public static bool IsInRadiusOptimised (Vector2 a, Vector2 b, float r, float sqrRadius)
		{
			var dx = b.x - a.x;
			var dy = b.y - a.y;

			if (dx > r || dy > r)
				return false;

			if (dx + dy <= r)
				return true;
			
			var sqrDist = SqrDistance (a, b);
			return sqrDist <= sqrRadius;
		}
	}
}
