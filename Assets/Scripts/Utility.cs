using UnityEngine;

namespace Nature
{
	public static class Utility 
	{
		public static Vector2 GetRandomVector2D(float minX, float maxX, float minY, float maxY)
		{
			return new Vector2
				(
				Random.Range(minX, maxX),
				Random.Range(minY, maxY)
				);
		}
		public static Vector2 GetRandomVector2D()
		{
			return GetRandomVector2D(-0.5f, 0.5f, -0.5f, 0.5f).normalized;
		}
	}
}   
