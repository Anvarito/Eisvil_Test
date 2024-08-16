using Infrastructure.Extras;
using UnityEngine.Events;

public interface IHealth
{
    public UnityAction<IHealth> OnDead { get; set; }
    ReactiveVariable<int> CurrentHitPoints { get;}
    void TakeDamage(int damageAmount);
}