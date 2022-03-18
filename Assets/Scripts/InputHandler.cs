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
		private void Update()
		{
			SetMousePosition();
		}
		void SetMousePosition()
		{
			m_mouseScreenPosition = Input.mousePosition;
			m_mouseScreenPosition.z = 10f;

			m_mouseWorldPosition = Camera.main.ScreenToWorldPoint(m_mouseScreenPosition);
		}
	}
}   
