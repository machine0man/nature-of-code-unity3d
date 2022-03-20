using UnityEngine;

namespace Nature
{
    public class AA_Vehicle : MonoBehaviour
    {
		[SerializeField] float m_Mass;
		[SerializeField] float m_maxSpeed;
		[SerializeField] float m_maxForce;
		[SerializeField] bool m_followTarget;
		[SerializeField] ESteerForceType m_steerForceType;
		[SerializeField] float m_ArriveRadius;

		Vector2 m_Acceleration;
		Vector2 m_Velocity=Vector2.zero;
		float m_PosX,m_PosY;

		Transform m_Target;
		Vector2 m_targetDir;

		void Start()
		{
			m_Target = AA_Main.Target;
		}
		void FixedUpdate()
		{
			if (m_followTarget) FollowTarget();
		}
		void FollowTarget()
		{
			Vector2 l_steer = GetSteerForce();
			AddForce(l_steer);

			m_Velocity += m_Acceleration;
			m_PosX += m_Velocity.x;
			m_PosY += m_Velocity.y;

			transform.position = new Vector2(m_PosX, m_PosY);
			transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, m_targetDir));
			m_Acceleration = Vector2.zero;
		}
		Vector2 GetSteerForce()
		{
			switch (m_steerForceType)
			{
				case ESteerForceType.Seek: return GetSeekForce();
				case ESteerForceType.Flee: return GetFleeForce();

				case ESteerForceType.None: 
				default:
					return Vector2.zero;
			}
		}
		Vector2 GetSeekForce()
		{
			m_targetDir = (m_Target.position - transform.position);
			Vector2 m_desiredForce = m_targetDir.normalized;

			m_desiredForce *= (m_targetDir.magnitude < m_ArriveRadius)?
				m_targetDir.magnitude.Map(0, m_ArriveRadius, 0,m_maxSpeed) //arrive behaviour
				: m_maxSpeed;
			
			//Reynolds Steering Formula
			Vector2 l_steer = m_desiredForce - m_Velocity;
			
			return (l_steer.magnitude > m_maxForce) ? l_steer.normalized * m_maxForce : l_steer; 

			/* check why it doesn't work later
			l_steer.LimitMagnitudeUpper(m_maxForce);
			return l_steer;
			*/
		}
		Vector2 GetFleeForce()
		{
			return GetSeekForce() * -1;
		}
		void AddForce(Vector2 l_forceToAdd)
		{
			m_Acceleration = l_forceToAdd; //considering mass1
		}
		public enum ESteerForceType : byte
		{
			None,
			Seek,
			Flee
		}
	}
}   
