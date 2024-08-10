using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _muzzlePoint;


    private float _shootDistance = 30;
    private bool _isDetected = false;
    private float _cooldwnTimer;
    private float _fireRate;

    public void Init(float fireRate)
    {
        _fireRate = fireRate;
    }


    public void Shoot()
    {
        _cooldwnTimer += Time.deltaTime;
        if (_cooldwnTimer >= _fireRate)
        {
            Instantiate(_bulletPrefab, _muzzlePoint.position, _muzzlePoint.rotation);
            _cooldwnTimer = 0;
        }
    }
}