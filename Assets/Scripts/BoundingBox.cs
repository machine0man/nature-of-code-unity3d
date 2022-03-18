using UnityEngine;

namespace Nature
{
	public class BoundingBox : MonoBehaviour
	{
		static BoundingBox s_Instance;

		[SerializeField] Vector2 m_BoundingBoxMaxVal;
		public static Vector2 BoundingBoxMaxVal { get => s_Instance.m_BoundingBoxMaxVal;}

		#region Unity Methods
		private void Awake()
		{
			s_Instance = this;
		}
		private void OnDestroy()
		{
			s_Instance = null;
		}
		#endregion

		public static void Bounce(IBounceable a_objToBounce)
		{
			s_Instance.Bounce_internal(a_objToBounce);
		}
		void Bounce_internal(IBounceable a_objToBounce)
		{
			if (Mathf.Abs(a_objToBounce.ObjPosX) > (m_BoundingBoxMaxVal.x))
			{
				a_objToBounce.SpeedX *= -1; 
				a_objToBounce.AccelerationX *= -1; 
			}
			if (Mathf.Abs(a_objToBounce.ObjPosY) > (m_BoundingBoxMaxVal.y))
			{
				a_objToBounce.SpeedY *= -1;
				a_objToBounce.AccelerationY *= -1;
			}
		}
	}
}
