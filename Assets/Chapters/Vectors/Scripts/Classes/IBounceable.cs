namespace Nature
{
	public interface IBounceable
	{
		float AccelerationX { get; set; }
		float AccelerationY { get; set; }
		float SpeedX { get; set; }
		float SpeedY { get; set; }
		float ObjPosX { get; }
		float ObjPosY { get; }
	}
}   
