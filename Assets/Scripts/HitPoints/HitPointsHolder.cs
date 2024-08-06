using UnityEngine;
using UnityEngine.Events;

public class HitPointsHolder : IHitPoints
{
    public UnityAction OnHealthOver { get; set; }
    public float CurrentHitPoints { get; private set; }

    private IDamageReceiver _damageReceiver;

    public HitPointsHolder(IDamageReceiver damageReceiver, int maxHitPoint)
    {
        CurrentHitPoints = maxHitPoint;
        _damageReceiver = damageReceiver;
        _damageReceiver.OnApplyDamage += ApplyDamage;
    }

    private void ApplyDamage(float damageAmount)
    {
        CurrentHitPoints -= Mathf.Clamp(damageAmount,0, CurrentHitPoints);

        if (CurrentHitPoints <= 0)
        {
            OnHealthOver?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _damageReceiver.OnApplyDamage -= ApplyDamage;
    }

}