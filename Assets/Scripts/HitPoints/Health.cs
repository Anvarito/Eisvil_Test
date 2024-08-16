using Infrastructure.Extras;
using UnityEngine;
using UnityEngine.Events;

public class Health : IHealth
{
    public UnityAction<IHealth> OnDead { get; set; }
    public ReactiveVariable<int> CurrentHitPoints { get; private set; }
    
    public Health(int hp)
    {
        CurrentHitPoints = new ReactiveVariable<int>
        {
            Value = hp
        };
    }
    public void TakeDamage(int damageAmount)
    {
        CurrentHitPoints.Value -= Mathf.Clamp(damageAmount,0, CurrentHitPoints.Value);
        if (CurrentHitPoints.Value <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        OnDead?.Invoke(this);
    }
}