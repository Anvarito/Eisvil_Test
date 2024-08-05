using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public void Place(IPlayerView playerView)
    {
        playerView.Transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
