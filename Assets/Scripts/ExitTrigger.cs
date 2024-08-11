using Player;
using UnityEngine;
using UnityEngine.Events;

public class ExitTrigger : MonoBehaviour
{
    public UnityAction OnPlayerReachExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerView playerView))
        {
            OnPlayerReachExit?.Invoke();
        }
    }
}


