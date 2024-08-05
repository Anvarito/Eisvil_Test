using Cysharp.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _speed = 10;
    public int _destroyMilliseconds = 5000;

    private void Awake()
    {
        DestroyByTime();
    }

    private void Update()
    {
        transform.Translate(transform.forward * _speed, Space.World);
    }

    private async UniTask DestroyByTime()
    {
        await UniTask.Delay(_destroyMilliseconds);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageReceiver damageRecivier))
        {
            damageRecivier.ApplyDamage(1);
        }

        Destroy(gameObject);
    }
}