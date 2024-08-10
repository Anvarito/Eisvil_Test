using UnityEngine;

public class HitPointsHolder : IHitPoints
{
    public int CurrentHitPoints { get; private set; }
    public HitPointsHolder(int maxHitPoint)
    {
        CurrentHitPoints = maxHitPoint;
    }
    public int DecreaseHitPoints(int damageAmount)
    {
        CurrentHitPoints -= Mathf.Clamp(damageAmount,0, CurrentHitPoints);
        return CurrentHitPoints;
    }
}