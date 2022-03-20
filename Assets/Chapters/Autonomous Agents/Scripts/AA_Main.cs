using System;
using UnityEngine;

namespace Nature
{
    public class AA_Main : MonoBehaviour
    {
		static AA_Main s_Instance;
        [SerializeField] AA_Vehicle m_prefabVehicle;
        [SerializeField] GameObject m_prefabTarget;
		AA_Vehicle m_Vehicle;
		Transform m_Target;
		public static Transform Target { get => s_Instance.m_Target; }

		#region Unity Methods
		void Awake()
		{
			s_Instance = this;
		}
		void OnDestroy()
		{
			s_Instance = null;
		} 
		#endregion

		void Start()
		{
			m_Vehicle = InstantiateVehicle();
			m_Target = Instantiate(m_prefabTarget).transform;
		}
		void Update()
		{
			SetTargetPosition();
		}
		AA_Vehicle InstantiateVehicle()
		{
			Vector2 l_bbMaxVal = BoundingBox.BoundingBoxMaxVal;
			Vector2 l_spawnPos = Utility.GetRandomVector2D(-l_bbMaxVal.x, l_bbMaxVal.x, -l_bbMaxVal.y, l_bbMaxVal.y);
			AA_Vehicle l_Vehicle = Instantiate(m_prefabVehicle, l_spawnPos,Quaternion.identity);
			return l_Vehicle;
		}
		void SetTargetPosition()
		{
			m_Target.position = InputHandler.MouseWorldPosition;
		}
	}
}   
