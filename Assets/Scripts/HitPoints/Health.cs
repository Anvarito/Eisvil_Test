using UnityEngine;
using UnityEngine.Events;

public class Health : IHealth
{
    public UnityAction<IHealth> OnDead { get; set; }
    public int CurrentHitPoints { get; private set; }
    
    public Health(int hp)
    {
        CurrentHitPoints = hp;
    }
    public void TakeDamage(int damageAmount)
    {
        CurrentHitPoints -= Mathf.Clamp(damageAmount,0, CurrentHitPoints);
        if (CurrentHitPoints <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        OnDead?.Invoke(this);
    }
}