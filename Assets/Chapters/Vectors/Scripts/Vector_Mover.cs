using UnityEngine;

namespace Nature
{
	public class Vector_Mover : MonoBehaviour,IBounceable
	{
		float m_topSpeed;
		float m_overDriveDistance;
		EAccelerationType m_accelerationType;
		Vector2 m_Acceleration;
		Vector2 m_Velocity;
		float m_PosX, m_PosY;
		bool m_shouldBounce;

		public Vector2 Velocity { get => m_Velocity; set => m_Velocity = value; }
		public Vector2 Acceleration { get => m_Acceleration; set => m_Acceleration = value; }
		public EAccelerationType AccelerationType { get => m_accelerationType; set => m_accelerationType = value; }
		public float OverDriveDistance { get => m_overDriveDistance; set => m_overDriveDistance = value; }
		public float TopSpeed { get => m_topSpeed; set => m_topSpeed = value; }
		public bool ShouldBounce 
		{ 
			get => m_shouldBounce;
			set
			{
				m_shouldBounce = value; 
			}
		}

		#region IBounceable IMPL
		float IBounceable.SpeedX { get => m_Velocity.x; set => m_Velocity.x = value; }
		float IBounceable.SpeedY { get => m_Velocity.y; set => m_Velocity.y = value; }
		float IBounceable.AccelerationX { get => m_Acceleration.x; set => m_Acceleration.x = value; }
		float IBounceable.AccelerationY { get => m_Acceleration.y; set => m_Acceleration.y = value; }
		float IBounceable.ObjPosX => m_PosX;
		float IBounceable.ObjPosY => m_PosY;
		#endregion

		public void Init(Vector2 a_initPosition)
		{
			m_PosX = a_initPosition.x;
			m_PosY = a_initPosition.y;
			m_Velocity = Vector2.zero;
			m_shouldBounce = false;
		}
		public void UpdateMoverPosition()
		{
			if(m_shouldBounce)	BoundingBox.Bounce(this);

			Vector2 l_direction;
			switch (m_accelerationType)
			{
				case EAccelerationType.Zero:
					m_Velocity = m_Acceleration = Vector2.zero;
					break;
				case EAccelerationType.Random:
					m_Acceleration = Utility.GetRandomVector2D() * 0.01f;
					m_Velocity += m_Acceleration;
					break;
				case EAccelerationType.FollowCursor:
					l_direction = (InputHandler.MouseWorldPosition - transform.position);

					if ((Mathf.Abs(l_direction.magnitude) > m_overDriveDistance))//skipping updating velocity to overdrive for nearer objects
					{
						m_Acceleration = (l_direction.normalized * 0.01f) / l_direction.magnitude;
						m_Velocity += m_Acceleration;
					}
					break;
				case EAccelerationType.RepellFromCursor:
					l_direction = (transform.position - InputHandler.MouseWorldPosition);

					if ((Mathf.Abs(l_direction.magnitude) < m_overDriveDistance))
					{
						m_Acceleration = (l_direction.normalized * 0.01f) / l_direction.magnitude;
						m_Velocity += m_Acceleration;
					}
					break;
				default:
					break;
			}

			m_Velocity = LimitSpeed(m_Velocity, m_topSpeed);

			m_PosX += m_Velocity.x;
			m_PosY += m_Velocity.y;

			transform.position = new Vector3(m_PosX, m_PosY,0);   
		}
		Vector2 LimitSpeed(Vector2 a_curVelocity, float a_topSpeed)
		{
			if (a_curVelocity.magnitude > a_topSpeed)
			{
				return  a_curVelocity.normalized * a_topSpeed;
			}
			
			return a_curVelocity;
		}
		public enum EAccelerationType:byte
		{ 
			Zero,
			Random,
			FollowCursor,
			RepellFromCursor
		}
	}

}   
