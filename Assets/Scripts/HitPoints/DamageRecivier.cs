using UnityEngine;
using UnityEngine.Events;

public class DamageRecivier : MonoBehaviour, IDamageReceiver
{
    public UnityAction<float> OnApplyDamage { get; set; }

    public void ApplyDamage(float damageValue)
    {
        OnApplyDamage?.Invoke(damageValue);
    }
}
