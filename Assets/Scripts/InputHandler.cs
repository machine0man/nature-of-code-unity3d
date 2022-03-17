using UnityEngine;

namespace Nature
{
	public class InputHandler : MonoBehaviour
	{
		static InputHandler s_Instance;
		Vector3 m_mouseScreenPosition;
		Vector3 m_mouseWorldPosition;

		public static Vector3 MouseScreenPosition { get => s_Instance.m_mouseScreenPosition; }
		public static Vector3 MouseWorldPosition { get => s_Instance.m_mouseWorldPosition; }

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

		public static Vector3 GetCursorPosition()
		{
			return s_Instance.GetCursorPosition_internal();
		}
		private void Update()
		{
			SetMouseScreenPosition();
			SetMouseWorldPosition();
		}
		Vector3 GetCursorPosition_internal()
		{
			Vector3 l_pos = Input.mousePosition;
			l_pos.z = 10f;
			return l_pos;
		}
		void SetMouseScreenPosition()
		{
			m_mouseScreenPosition = Input.mousePosition;
			m_mouseScreenPosition.z = 10;
		}
		void SetMouseWorldPosition()
		{
			m_mouseWorldPosition = Camera.main.ScreenToWorldPoint(m_mouseScreenPosition);
		}
	}
}   
