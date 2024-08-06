using UnityEngine;
using Zenject;

public class PlayerSpawnPoint : MonoBehaviour
{
    private PlayerView _playerView;

    [Inject]
    private void Construct(PlayerView playerView)
    {
        _playerView = playerView;
    }

    private void Awake()
    {
        Place();
    }

    public void Place()
    {
        _playerView.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}