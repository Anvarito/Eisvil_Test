using UnityEngine;
using UnityEngine.Events;

public class DamageRecivier : MonoBehaviour, IDamageReceiver
{
    public UnityAction<int> OnApplyDamage { get; set; }

    public void ApplyDamage(int damageValue)
    {
        OnApplyDamage?.Invoke(damageValue);
    }
}
