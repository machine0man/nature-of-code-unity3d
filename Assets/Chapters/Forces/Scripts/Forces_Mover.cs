using UnityEngine;

namespace Nature
{
	public class Forces_Mover : MonoBehaviour,IBounceable
	{
		float m_Mass;
		float m_topSpeed;
		float m_overDriveDistance;
		Vector2 m_Acceleration;
		Vector2 m_Velocity;
		float m_PosX, m_PosY;
		bool m_shouldBounce;

		public Vector2 Position { get => new Vector2(m_PosX,m_PosY); }
		public Vector2 Velocity { get => m_Velocity; set => m_Velocity = value; }
		public Vector2 Acceleration { get => m_Acceleration; set => m_Acceleration = value; }
		public float OverDriveDistance { get => m_overDriveDistance; set => m_overDriveDistance = value; }
		public float TopSpeed { get => m_topSpeed; set => m_topSpeed = value; }
		public bool ShouldBounce { get => m_shouldBounce; set => m_shouldBounce = value; }
		public float Mass { get => m_Mass; set => m_Mass = value; }

		#region IBounceable IMPL
		float IBounceable.SpeedX { get => m_Velocity.x; set => m_Velocity.x = value; }
		float IBounceable.SpeedY { get => m_Velocity.y; set => m_Velocity.y = value; }
		float IBounceable.AccelerationX { get => m_Acceleration.x; set => m_Acceleration.x = value; }
		float IBounceable.AccelerationY { get => m_Acceleration.y; set => m_Acceleration.y = value; }
		float IBounceable.ObjPosX => m_PosX;
		float IBounceable.ObjPosY => m_PosY;
		#endregion

		public void Init(Vector2 a_initPosition, float a_mass)
		{
			m_PosX = a_initPosition.x;
			m_PosY = a_initPosition.y;
			m_Velocity = Vector2.zero;
			m_shouldBounce = false;
			m_Mass = a_mass;
			transform.localScale = new Vector2(GetRadiusFromMass(a_mass)*2, GetRadiusFromMass(a_mass) * 2);
		}
		public void UpdateMoverPosition()
		{
			if(m_shouldBounce)	BoundingBox.Bounce(this);

			m_Velocity += m_Acceleration;
		//	m_Velocity = LimitSpeed(m_Velocity, m_topSpeed);

			m_PosX += m_Velocity.x;
			m_PosY += m_Velocity.y;

			transform.position = new Vector3(m_PosX, m_PosY,0);
		}
		public void AddForce(Vector2 l_forceToAdd)
		{
			m_Acceleration += l_forceToAdd / m_Mass; //f = m*a
		}
		public Vector2 Attract(Forces_Mover a_mover)
		{
			Vector2 l_direction = Position -a_mover.Position;
			if (l_direction.magnitude < OverDriveDistance)
				return Vector2.zero;
			else
			{
				Vector2 l_force = ((m_Mass * a_mover.m_Mass) / l_direction.sqrMagnitude) * l_direction.normalized;
				return l_force;
			}
		}
		Vector2 LimitSpeed(Vector2 a_curVelocity, float a_topSpeed)
		{
			if (a_curVelocity.magnitude > a_topSpeed)
			{
				return  a_curVelocity.normalized * a_topSpeed;
			}
			
			return a_curVelocity;
		}
		float GetRadiusFromMass(float a_mass)
		{
			return Mathf.Sqrt(2.5f*2.5f *a_mass);
		}
	}

}   
