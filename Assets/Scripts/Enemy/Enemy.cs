using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityAction<Enemy> OnDead;
    private IHitPoints _hitPoints;

    public void Init(IHitPoints hitPoints)
    {
        _hitPoints = hitPoints;
        _hitPoints.OnHealthOver += OnHealthOver;
    }

    private void OnHealthOver()
    {
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }
}