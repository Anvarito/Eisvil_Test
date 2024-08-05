public class PlayerDataModel : IPlayerDataModel
{
    public PlayerDataModel(float speed, float angularSpeed)
    {
        MoveSpeed = speed;
        AngularSpeed = angularSpeed;
    }

    public float MoveSpeed { get; }
    public float AngularSpeed { get; }
}