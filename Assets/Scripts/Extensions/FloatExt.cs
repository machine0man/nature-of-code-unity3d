using UnityEngine;

namespace Nature
{
	public static class FloatExt
	{
		public static float Map(this float a_valueToMap, float a_minFrom, float a_maxFrom, float a_minTo, float a_maxTo)
		{
			float l_T = Mathf.InverseLerp(a_minFrom, a_maxFrom, a_valueToMap);
			return Mathf.Lerp(a_minTo, a_maxTo, l_T);
		}
	}
}
