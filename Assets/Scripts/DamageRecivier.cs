using UnityEngine;

public class DamageRecivier : MonoBehaviour, IDamageReceiver
{
    public void ApplyDamage(float damageValue)
    {
        print(damageValue);
    }
}
