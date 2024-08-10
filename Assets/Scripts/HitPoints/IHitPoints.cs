using UnityEngine.Events;

public interface IHitPoints
{
    int CurrentHitPoints { get;}
    int DecreaseHitPoints(int damageAmount);
}