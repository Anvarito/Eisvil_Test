using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.StaticData;
using JetBrains.Annotations;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class PlayerHitPointView : MonoBehaviour
{
    [SerializeField] private Image _line;
    [SerializeField] private RectTransform _bar;
    [SerializeField] private TextMeshProUGUI _points;
    [SerializeField] private Vector3 _offset;
    private Health _health;
    private int _maxHitPoints;
    private Transform _playerTransform;
    private CancellationTokenSource _cancellationTokenSource;
    private Camera _mainCamera;


    [Inject]
    private void Construct([Inject(Id="Player health")] [NotNull] Health health, IStaticDataService staticDataService, PlayerView playerView)
    {
        _health = health;
        _maxHitPoints = staticDataService.PlayerHitPointsConfig.HitPoints;
        _playerTransform = playerView.transform;
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cancellationTokenSource = new CancellationTokenSource();
        _health.CurrentHitPoints.OnValueChanged += SetHitPointsValue;
        _points.text = _maxHitPoints.ToString();
        _bar.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _health.CurrentHitPoints.OnValueChanged -= SetHitPointsValue;
        _cancellationTokenSource.Cancel();
    }

    private void LateUpdate()
    {
        if(_bar.gameObject.activeSelf)
        {
            Vector3 worldPosition = _playerTransform.position + _offset;
            Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);
            _bar.position = screenPosition;
        }
    }

    private void SetHitPointsValue(int newHitPoints)
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        
        float alpha = (float)newHitPoints / _maxHitPoints;
        _line.fillAmount = alpha;
        _points.text = newHitPoints.ToString();

       ShowBar();
    }

    private async UniTask ShowBar()
    {
        _bar.gameObject.SetActive(true);
        await UniTask.Delay(2000, cancellationToken: _cancellationTokenSource.Token);
        _bar.gameObject.SetActive(false);
    }
}
