using Player;
using UnityEngine;

public class Ramming : MonoBehaviour
{
    [SerializeField] private DamageRecivier _selfDamageRecivier;
    [SerializeField] private int _selfDamageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerView playerView))
        {
            if (_selfDamageRecivier)
                _selfDamageRecivier.ApplyDamage(_selfDamageAmount);
            playerView.GetComponent<DamageRecivier>().OnApplyDamage(10);
        }
    }
}