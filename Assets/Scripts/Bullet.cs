using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private int _destroyMilliseconds = 5000;
    [SerializeField] private int _damageValue = 3;
    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        DestroyByTime();
    }

    private void Update()
    {
        transform.Translate(transform.forward * _speed, Space.World);
    }

    private async UniTask DestroyByTime()
    {
        await UniTask.Delay(_destroyMilliseconds, cancellationToken: _cancellationTokenSource.Token);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageReceiver damageRecivier))
        {
            damageRecivier.ApplyDamage(_damageValue);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }
}