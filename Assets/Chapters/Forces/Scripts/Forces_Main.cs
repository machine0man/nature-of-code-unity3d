using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Nature
{
	public class Forces_Main : MonoBehaviour
	{
		[SerializeField] Forces_Mover m_prefabMover;
		[SerializeField] GameObject m_prefabMisterRed;
		[SerializeField] int m_moversCount;
		[SerializeField] float m_overDriveDistance;
		[SerializeField] float m_topSpeed;
		[SerializeField] Vector2 m_windForce;
		[SerializeField] float m_graviatationalConstant;

		List<Forces_Mover> m_lstMovers = new List<Forces_Mover>();
		GameObject m_MisterRed;
		private void Start()
		{
			Vector2 l_bbMaxVal = BoundingBox.BoundingBoxMaxVal;

			for (int l_moversIndex = 0; l_moversIndex < m_moversCount; l_moversIndex++)
			{
				Vector2 l_moverPos = Utility.GetRandomVector2D(-l_bbMaxVal.x, l_bbMaxVal.x, -l_bbMaxVal.y, l_bbMaxVal.y);
				Forces_Mover l_mover = Instantiate(m_prefabMover);
				l_mover.Init(l_moverPos,Random.Range(0f,5f));
				l_mover.ShouldBounce = true;
				m_lstMovers.Add(l_mover);
			}
		}
		private void FixedUpdate()
		{
			foreach (Forces_Mover l_mover in m_lstMovers)
			{
				l_mover.OverDriveDistance = m_overDriveDistance;
				l_mover.TopSpeed = m_topSpeed;


				foreach (Forces_Mover l_attracter in m_lstMovers)
				{
					if (l_attracter == l_mover) continue;

					Vector2 l_attractionForce = l_attracter.Attract(l_mover);
					l_mover.AddForce(l_attractionForce * m_graviatationalConstant);
				}

				l_mover.UpdateMoverPosition();
				l_mover.Acceleration = Vector2.zero;
			}
		}
	}
}
