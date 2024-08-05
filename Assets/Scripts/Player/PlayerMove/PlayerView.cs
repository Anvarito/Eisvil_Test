using System;
using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    public Transform Transform
    {
        get => transform;
        set => throw new NotImplementedException();
    }

    public void SetAnimationSpeed(float speed)
    {
        _animator.SetFloat("Speed_f", speed);
    }

    public void Move(Vector3 direction)
    {
        //print((direction));
        _controller.Move(direction);
    }

    public void Rotating(Quaternion rotate)
    {
        transform.rotation = rotate;
    }

    private void Update()
    {
        if(!_controller.isGrounded)
        _controller.Move(Vector3.down * (-Physics.gravity.y * Time.deltaTime));
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("WAAWAWAW");
    }
}