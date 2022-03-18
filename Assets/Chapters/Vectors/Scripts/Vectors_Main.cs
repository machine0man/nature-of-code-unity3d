using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Nature
{
	public class Vectors_Main : MonoBehaviour
	{
		[SerializeField] Vector_Mover m_prefabMover;
		[SerializeField] GameObject m_prefabMisterRed;
		[SerializeField] int m_moversCount;
		[SerializeField] Vector_Mover.EAccelerationType m_accelerationType;
		[SerializeField] float m_overDriveDistance;
		[SerializeField] float m_topSpeed;

		List<Vector_Mover> m_lstMovers = new List<Vector_Mover>();
		GameObject m_MisterRed;
		private void Start()
		{
			Vector2 l_bbMaxVal = BoundingBox.BoundingBoxMaxVal;

			for (int l_moversIndex = 0; l_moversIndex < m_moversCount; l_moversIndex++)
			{
				Vector2 l_moverPos = Utility.GetRandomVector2D(-l_bbMaxVal.x, l_bbMaxVal.x, -l_bbMaxVal.y, l_bbMaxVal.y);
				Vector_Mover l_mover = Instantiate(m_prefabMover);

				l_mover.Init(l_moverPos);
				//	l_mover.Velocity = Utility.GetRandomVector2D(-0.5f,0.5f,-0.5f,0.5f);
				l_mover.Acceleration = Utility.GetRandomVector2D(-0.05f, 0.05f, -0.05f, 0.05f);

				m_lstMovers.Add(l_mover);
			}
		}
		private void FixedUpdate()
		{
			foreach (Vector_Mover l_mover in m_lstMovers)
			{
				if (l_mover.AccelerationType == Vector_Mover.EAccelerationType.RepellFromCursor)
				{
					if (m_MisterRed == null) m_MisterRed = Instantiate(m_prefabMisterRed);
					m_MisterRed.transform.position = InputHandler.MouseWorldPosition;
					l_mover.ShouldBounce = true;
				}
				else
				{
					if (m_MisterRed != null) Destroy(m_MisterRed); 
				}
				l_mover.AccelerationType = m_accelerationType;
				l_mover.OverDriveDistance = m_overDriveDistance;
				l_mover.TopSpeed = m_topSpeed;
				l_mover.UpdateMoverPosition();
			}
		}
	}
}
