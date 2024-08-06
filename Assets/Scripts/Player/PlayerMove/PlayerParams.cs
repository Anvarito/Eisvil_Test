public class PlayerParams : IPlayerParams
{
    public PlayerParams(float speed, float angularSpeed)
    {
        MoveSpeed = speed;
        AngularSpeed = angularSpeed;
    }

    public float MoveSpeed { get; }
    public float AngularSpeed { get; }
}