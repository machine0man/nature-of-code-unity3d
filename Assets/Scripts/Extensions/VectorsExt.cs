using UnityEngine;

namespace Nature
{
    public static class VectorsExt 
    {
		public static void LimitMagnitudeUpper(this Vector2 a_vec, float a_maxMagnitude)
		{
			if (a_vec.magnitude > a_maxMagnitude)
			{
				a_vec.Normalize();
				a_vec.x *= a_maxMagnitude;
				a_vec.y *= a_maxMagnitude;
			}
		}
	}
}   
