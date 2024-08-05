using UnityEngine;
using Zenject;

public interface IPlayerView
{
    public Transform Transform { get; set; }
    public void SetAnimationSpeed(float speed);

    public void Move(Vector3 direction);

    public void Rotating(Quaternion rotate);
}