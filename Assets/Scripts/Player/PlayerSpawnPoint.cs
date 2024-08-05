using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.States;
using UnityEngine;
using Zenject;

public class PlayerSpawnPoint : MonoBehaviour
{
    public void Place(IPlayerView playerView)
    {
        playerView.Transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
