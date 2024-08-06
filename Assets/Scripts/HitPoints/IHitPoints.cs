using UnityEngine.Events;

public interface IHitPoints
{
    UnityAction OnHealthOver { get; set; }
    float CurrentHitPoints { get;}
}