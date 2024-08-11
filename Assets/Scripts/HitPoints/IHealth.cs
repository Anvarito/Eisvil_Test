using UnityEngine.Events;

public interface IHealth
{
    public UnityAction<IHealth> OnDead { get; set; }
    int CurrentHitPoints { get;}
    void TakeDamage(int damageAmount);
}